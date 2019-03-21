using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using MahApps.Metro.Controls;
using WpfClient.ViewModels;

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {   
        // Private members 
        private MainWindowViewModel _mainWindowViewModel;

        public MainWindow()
        {
            InitializeComponent();

            // action to do when this screen is loaded
            this.Loaded += new RoutedEventHandler(thisWindow_Loaded);
        }

        private void thisWindow_Loaded(object sender, RoutedEventArgs args)
        {    
            // Get the view model for this window use teh locator
            _mainWindowViewModel = ViewModelLocator.MainWindowViewModel;
            // assign currentWindow propery for this window
            _mainWindowViewModel.CurrentWindow = (MetroWindow)this;
        }

    }
}
