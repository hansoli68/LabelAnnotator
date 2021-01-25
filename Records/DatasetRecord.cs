using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace COCOAnnotator.Records {
    public class DatasetRecord : BindableBase {
        private string _BasePath;
        public string BasePath {
            get => _BasePath;
            set => SetProperty(ref _BasePath, value);
        }

        public FastObservableCollection<ImageRecord> Images { get; }
        public ObservableCollection<CategoryRecord> Categories { get; }

        public void Deconstruct(out ICollection<ImageRecord> Images, out ICollection<CategoryRecord> Categories) {
            Images = this.Images;
            Categories = this.Categories;
        }

        public DatasetRecord() {
            _BasePath = "";
            Images = new FastObservableCollection<ImageRecord>();
            Categories = new ObservableCollection<CategoryRecord>();
        }

        public DatasetRecord(string BasePath, ICollection<ImageRecord> Images, ICollection<CategoryRecord> Categories) {
            _BasePath = BasePath;
            this.Images = new FastObservableCollection<ImageRecord>(Images);
            this.Categories = new ObservableCollection<CategoryRecord>(Categories);
        }
    }
}
