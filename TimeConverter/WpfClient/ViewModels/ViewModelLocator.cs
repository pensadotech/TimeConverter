namespace WpfClient.ViewModels
{
    public class ViewModelLocator
    {
        // Private members
        private static readonly TimeConverter.Service.ITimeConverter TimeConverter = 
            new TimeConverter.Service.TimeConverter();

        // IMPORTANT: The ViewModelLocator has to know about all ViewsModels in the application!
        private static MainWindowViewModel _mainWindowViewModel = new MainWindowViewModel(TimeConverter);

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
