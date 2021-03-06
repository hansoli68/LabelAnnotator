using COCOAnnotator.Services;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace COCOAnnotator.ViewModels.Commons {
    public abstract class ViewModel : BindableBase {
        private string _Title;
        public string Title {
            get => _Title;
            set => SetProperty(ref _Title, value);
        }

        protected IEventAggregator EventAggregator;
        protected IDialogService UserDialogSerivce;
        protected SettingService SettingService;

        protected ViewModel() {
            _Title = "";

            EventAggregator = ContainerLocator.Current.Resolve<IEventAggregator>();
            UserDialogSerivce = ContainerLocator.Current.Resolve<IDialogService>();
            SettingService = ContainerLocator.Current.Resolve<SettingService>();
        }
    }
}
