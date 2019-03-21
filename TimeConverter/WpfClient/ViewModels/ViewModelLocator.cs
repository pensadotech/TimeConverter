using TimeConverter.Service;

namespace WpfClient.ViewModels
{
    public class ViewModelLocator
    {
        // Private members
        private static ITimeConverter _timeConverter = new TimeConverter.Service.TimeConverter();

        // IMPORTANT: The ViewModelLocator has to know about all ViewsModels in the application!
        private static MainWindowViewModel _mainWindowViewModel = new MainWindowViewModel(_timeConverter);

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

    }
}
