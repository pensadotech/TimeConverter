using DataAccess = TimeConverter.DataAccess;
using Domain = TimeConverter.Domain;

namespace WpfClient.ViewModels
{
    public class ViewModelLocator
    {
        // Private members..................................
        
        // INJECTION: Root composition 
        // Repositories
        private static readonly Domain.Interfaces.Repositories.IUserConfigRepository UserConfigRepository = 
            new DataAccess.ConfigurationRepository();
        private static readonly Domain.Interfaces.Repositories.ITimeConverterRepository TimeConverterRepository = 
            new Domain.Functionality.TimeConvertionFunctions();
        
        // Services (represent teh Seam in teh architecture)
        private static readonly Domain.Interfaces.Services.ITimeConverterService TimeConverterService = 
            new Domain.Services.TimeConverterService(TimeConverterRepository);
        private static readonly Domain.Interfaces.Services.IUserConfigService UserConfigService = 
            new Domain.Services.UserConfigService("TimeConverter.xml", UserConfigRepository);
        

        // The ViewModelLocator has to know about all ViewsModels in the application!
        private static MainWindowViewModel _mainWindowViewModel = new MainWindowViewModel(TimeConverterService);
        // Flyouts ViewModels
        private static SettingsViewModel _settingsViewModel = new SettingsViewModel(UserConfigService);

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
