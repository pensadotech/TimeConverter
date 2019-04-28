using System;
using System.Configuration;
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
           // RootCompositionByCode();

            // Option 2: Initialize Repositories and Services using configuration settings
            RootCompositionByConfiguration(); 

            // Option 3: Initialize Repositories and Services using DI container
           // RootCompositionByDIContainer();  // TODO: Use Unity DI container

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
            // Ref: https://jeremylindsayni.wordpress.com/2019/02/11/instantiating-a-c-object-from-a-string-using-activator-createinstance-in-net/
            // assembly qualified name
            // "{namespace}.{class name}, "{assembly name}"

            // Repositories
            var confiRepoTypeName = ConfigurationManager.AppSettings["configurationRepository"];
            var configRepoType = Type.GetType(confiRepoTypeName, true);
            _userConfigRepository =
                (Domain.Interfaces.Repositories.IUserConfigRepository) Activator.CreateInstance(configRepoType);

            var timeConvertionTypeName = ConfigurationManager.AppSettings["timeConvertionFunction"];
            var timeConvertionType = Type.GetType(timeConvertionTypeName, true);
            _timeConverterRepository = (Domain.Interfaces.Repositories.ITimeConverterRepository)Activator.CreateInstance(timeConvertionType);

            // Services (represent the seam in the architecture)
            _timeConverterService = new Domain.Services.TimeConverterService(_timeConverterRepository);
            _userConfigService = new Domain.Services.UserConfigService(ConfigFilename, _userConfigRepository);

        }

        private void RootCompositionByDIContainer()
        {
            
        }

    }
}
