using DataAccess = TimeConverter.DataAccess;
using Domain = TimeConverter.Domain;

namespace WpfClient.ViewModels
{
    public class ViewModelLocator
    {
        // Private members..................................
        // Repositories
        private static Domain.Interfaces.Repositories.IUserConfigRepository _userConfigRepository;
        private static Domain.Interfaces.Repositories.ITimeConverterRepository _timeConverterRepository;

        // Services (represent the Seams)
        private static Domain.Interfaces.Services.ITimeConverterService _timeConverterService;
        private static Domain.Interfaces.Services.IUserConfigService _userConfigService;

        // The ViewModelLocator has to know about all ViewsModels in the application!
        private static MainWindowViewModel _mainWindowViewModel;
        private static SettingsViewModel _settingsViewModel;

        private const string ConfigFilename = "TimeConverterConfig.xml";
        
        // Constructors .....................................
        public ViewModelLocator()
        {
            // INJECTION: Root composition 
            // Option 1: Initialize Repositories and Services by code
            RootCompositionByCode();

            // Option 2: Initialize Repositories and Services using configuration settings
            RootCompositionByConfiguration(); // TODO - add configuration and activate

            // Option 3: Initialize Repositories and Services using DI container
            RootCompositionByDIContainer();  // Use UUNity DI container

            // Initialize ViewModels 
            // The ViewModelLocator has to know about all ViewsModels in the application!
            _mainWindowViewModel = new MainWindowViewModel(_timeConverterService);
            // Flyouts ViewModels
            _settingsViewModel = new SettingsViewModel(_userConfigService);
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
        
        // Methods .................................................
        private void RootCompositionByCode()
        {
            // Repositories
            _userConfigRepository = new DataAccess.ConfigurationRepository();
            _timeConverterRepository = new Domain.Functionality.TimeConvertionFunctions();

            // Services (represent the seam in the architecture)
            _timeConverterService = new Domain.Services.TimeConverterService(_timeConverterRepository);
            _userConfigService = new Domain.Services.UserConfigService(ConfigFilename, _userConfigRepository);
        }

        private void RootCompositionByConfiguration()
        {
            
        }

        private void RootCompositionByDIContainer()
        {
            
        }

    }
}
