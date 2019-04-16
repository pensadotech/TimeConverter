using TimeConverter.Domain.Functionality;
using TimeConverter.Domain.Interfaces.Repositories;
using TimeConverter.Domain.Interfaces.Services;
using TimeConverter.DataAccess;
using TimeConverter.Domain.Services;

namespace WpfClient.ViewModels
{
    public class ViewModelLocator
    {
        // Private members..................................
        
        // INJECTION: Root composition 
        // Repositories
        private static readonly IUserConfigRepository _userConfigRepository = new ConfigurationRepository();
        private static readonly ITimeConverterRepository _timeConverterRepository = new TimeConvertionFunctions();
        // Services 
        private static readonly ITimeConverterService _timeConverterService = new TimeConverterService(_timeConverterRepository);
        private static readonly IUserConfigService _userConfigService = new UserConfigService("TimeConverter.xml", _userConfigRepository);
        
        // TODO Make sure that View Moldels use services properly

        // The ViewModelLocator has to know about all ViewsModels in the application!
        private static MainWindowViewModel _mainWindowViewModel = new MainWindowViewModel(_timeConverterService);
        // Flyouts ViewModels
        private static SettingsViewModel _settingsViewModel = new SettingsViewModel(_userConfigService);

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
