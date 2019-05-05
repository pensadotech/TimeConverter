using System;
using System.Configuration;
using Unity;
using Unity.Injection;
using DataAccess = TimeConverter.DataAccess;
using Domain = TimeConverter.Domain;

namespace WpfClient.ViewModels
{
    public class ViewModelLocator
    {
        // Private members...............................................................
        // Repositories
        private static Domain.Interfaces.Repositories.IUserConfigRepository _userConfigRepository;
        private static Domain.Interfaces.Repositories.ITimeConverterRepository _timeConverterRepository;

        // Services (represent the Seams)
        private static Domain.Interfaces.Services.IUserConfigService _userConfigService;
        private static Domain.Interfaces.Services.ITimeConverterService _timeConverterService;
        
        // The ViewModelLocator has to know about all ViewsModels in the application!
        private static MainWindowViewModel _mainWindowViewModel;
        private static SettingsViewModel _settingsViewModel;

        private const string ConfigFilename = "TimeConverterConfig.xml";

        // DI container
        private IUnityContainer _container;

        // Constructors ...................................................................
        public ViewModelLocator()
        {
            // INJECTION: Root composition 
            // The following options demostrated posible ways
            // to inject the dependencies for the solution.
            
            // Option 1: Initialize Repositories and Services with code
            //           This option instantiate programatically the dependencies andd
            //           inject into the services used by this client application.
            //           this option is the least flexible. A change in repositories
            //           forces a manual change for referencing appropiate repositories
            //           and recompilaiton.
            // RootCompositionByCode();

            // Option 2: Initialize Repositories and Services using configuration settings
            //           This option reads from the App.Config file the repositoriy assemblies
            //           refrences, and activate using reflection, an dinjet them to the
            //           services. 
            // RootCompositionByConfiguration();

            // Option 3: Initialize Repositories and Services using DI container
            //           This option uses a DI container. In this case MS Unity. 
            //           Repositories assembly types will be read from App.Config
            //           and both Repositories and Services types will be register in
            //           the container. Finally services will be resovled using the container.
            RootCompositionByDIContainer();

            // Initialize Client ViewModels 
            // Flyouts configuration ViewModels
            _settingsViewModel = new SettingsViewModel(_userConfigService);
            // The ViewModelLocator has to know about all ViewsModels in the application!
            _mainWindowViewModel = new MainWindowViewModel(_timeConverterService);
        }

        // Properties ...................................................................
        public static MainWindowViewModel MainWindowViewModel
        {
            get { return _mainWindowViewModel; }
        }

        public static SettingsViewModel SettingsViewModel
        {
            get { return _settingsViewModel; }
        }

        // Methods ......................................................................
        private void RootCompositionByCode()
        {
            // Repositories
            _userConfigRepository = new DataAccess.ConfigurationRepository();
            _timeConverterRepository = new Domain.Functionality.TimeConvertionFunctions();

            // Services (represent the seam in the architecture)
            _userConfigService = new Domain.Services.UserConfigService(ConfigFilename, _userConfigRepository);
            _timeConverterService = new Domain.Services.TimeConverterService(_timeConverterRepository);
           
        }

        private void RootCompositionByConfiguration()
        {
            // How to refrence an assembly in teh config file by assembly qualified name
            // "{namespace}.{class name}, "{assembly name}"
            // Ref: https://jeremylindsayni.wordpress.com/2019/02/11/instantiating-a-c-object-from-a-string-using-activator-createinstance-in-net/
            
            // Repositories
            var confiRepoTypeName = ConfigurationManager.AppSettings["configurationRepository"];
            var configRepoType = Type.GetType(confiRepoTypeName, true);
            _userConfigRepository =
                (Domain.Interfaces.Repositories.IUserConfigRepository)Activator.CreateInstance(configRepoType);

            var timeConvertionTypeName = ConfigurationManager.AppSettings["timeConvertionFunction"];
            var timeConvertionType = Type.GetType(timeConvertionTypeName, true);
            _timeConverterRepository = (Domain.Interfaces.Repositories.ITimeConverterRepository)Activator.CreateInstance(timeConvertionType);

            // Services (represent the seam in the architecture)
            _userConfigService = new Domain.Services.UserConfigService(ConfigFilename, _userConfigRepository);
            _timeConverterService = new Domain.Services.TimeConverterService(_timeConverterRepository);
        }

        private void RootCompositionByDIContainer()
        {
            // Repositories - Retreive repositories from App.Config
            var confiRepoTypeName = ConfigurationManager.AppSettings["configurationRepository"];
            var configRepoType = Type.GetType(confiRepoTypeName, true);
            _userConfigRepository =
                (Domain.Interfaces.Repositories.IUserConfigRepository)Activator.CreateInstance(configRepoType);

            var timeConvertionTypeName = ConfigurationManager.AppSettings["timeConvertionFunction"];
            var timeConvertionType = Type.GetType(timeConvertionTypeName, true);
            _timeConverterRepository = 
                (Domain.Interfaces.Repositories.ITimeConverterRepository)Activator.CreateInstance(timeConvertionType);


            // Define container and register ITimeConverterRepository
            _container = new UnityContainer();

            // REGISTER
            // Register the repositories that are needed
            _container.RegisterInstance<Domain.Interfaces.Repositories.IUserConfigRepository>(_userConfigRepository);
            _container.RegisterInstance<Domain.Interfaces.Repositories.ITimeConverterRepository> (_timeConverterRepository);

            // Services (represent the seam in the architecture)
            // For the service help the container understand the repository type that will be passed
            // as parameter for the constructor.
            _container.RegisterType<Domain.Interfaces.Services.IUserConfigService, Domain.Services.UserConfigService>
                (new InjectionConstructor(ConfigFilename, _userConfigRepository));
            _container.RegisterType<Domain.Interfaces.Services.ITimeConverterService, Domain.Services.TimeConverterService>
                (new InjectionConstructor(_timeConverterRepository));

            // RESOLVE
            // Instantiate a ITimeConverterService 
            _userConfigService = _container.Resolve<Domain.Interfaces.Services.IUserConfigService>();
            _timeConverterService = _container.Resolve<Domain.Interfaces.Services.ITimeConverterService>();

        }

    }
}
