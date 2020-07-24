﻿using System;
using System.IO;
using YamlDotNet.RepresentationModel;

namespace LabelAnnotator {
    public static class SettingManager {
        private static YamlStream? _YamlStream;
        private static string SettingPath => AppDomain.CurrentDomain.BaseDirectory is null ? "setting.yaml" : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "setting.yaml");
        private static YamlStream YamlStream {
            get {
                if (_YamlStream is YamlStream) return _YamlStream;

                if (File.Exists(SettingPath)) {
                    using StreamReader file = new StreamReader(SettingPath);
                    _YamlStream = new YamlStream();
                    _YamlStream.Load(file);
                    if (_YamlStream.Documents.Count == 0 || !(_YamlStream.Documents[0].RootNode is YamlMappingNode)) {
                        _YamlStream.Documents.Insert(0, new YamlDocument(new YamlMappingNode()));
                    }
                } else {
                    _YamlStream = new YamlStream { new YamlDocument(new YamlMappingNode()) };
                }
                return _YamlStream;
            }
        }
        private static string GetItem(string Key, string DefaultValue) {
            YamlMappingNode root = (YamlMappingNode)YamlStream.Documents[0].RootNode;
            YamlScalarNode keyNode = new YamlScalarNode(Key);
            if (!root.Children.ContainsKey(keyNode)) {
                root.Add(Key, DefaultValue);
                return DefaultValue;
            } else {
                return ((YamlScalarNode)root.Children[keyNode]).Value ?? string.Empty;
            }
        }
        private static void SetItem(string Key, string Value) {
            YamlMappingNode root = (YamlMappingNode)YamlStream.Documents[0].RootNode;
            YamlScalarNode keyNode = new YamlScalarNode(Key);
            bool NeedSave = false;
            if (!root.Children.ContainsKey(keyNode)) {
                root.Add(Key, Value);
                NeedSave = true;
            } else {
                YamlScalarNode node = (YamlScalarNode)root.Children[keyNode];
                if (node.Value != Value) {
                    node.Value = Value;
                    NeedSave = true;
                }
            }
            if (NeedSave) using (StreamWriter file = new StreamWriter(SettingPath)) YamlStream.Save(file);
        }

        public static string Format {
            get => GetItem("Format", "LTRB");
            set => SetItem("Format", value);
        }
    }
}
