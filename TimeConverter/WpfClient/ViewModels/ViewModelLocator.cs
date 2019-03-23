namespace WpfClient.ViewModels
{
    public class ViewModelLocator
    {
        // Private members..................................
        // Connect Busines Logic layer, in this case time conversion logic
        private static readonly TimeConverter.Service.ITimeConverter TimeConverter = 
            new TimeConverter.Service.TimeConverter();

        // The ViewModelLocator has to know about all ViewsModels in the application!
        private static MainWindowViewModel _mainWindowViewModel = new MainWindowViewModel(TimeConverter);

        // Flyouts ViewModels
        private static SettingsViewModel _settingsViewModel = new SettingsViewModel();

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
