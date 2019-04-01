
namespace WpfClient.ViewModels
{
    public class ViewModelLocator
    {
        // Private members..................................
        // Time Conversion Logic
        private static readonly TimeConverter.Service.ITimeConverter TimeConverter = 
            new TimeConverter.Service.TimeConverter();
        // Configuration service
        private static TimeConverter.Service.IAppConfigHandler _applicationConfig = 
             new TimeConverter.Service.AppConfigHandler("TimeConverter.xml");

        // The ViewModelLocator has to know about all ViewsModels in the application!
        private static MainWindowViewModel _mainWindowViewModel = new MainWindowViewModel(TimeConverter);
        // Flyouts ViewModels
        private static SettingsViewModel _settingsViewModel = new SettingsViewModel(_applicationConfig);

        // Constructors .....................................
        public ViewModelLocator()
        {
            // Nothing here
        }

        // Properties ........................................
        public static MainWindowViewModel MainWindowViewModel
        {
            get { return _mainWindowViewModel; }
        }

        public static SettingsViewModel SettingsViewModel
        {
            get { return _settingsViewModel; }
        }

    }
}
