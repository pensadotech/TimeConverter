using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using MahApps.Metro.Controls;
using TimeConverter.Service;
using WpfClient.Utilities;

namespace WpfClient.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        // Private members .............................................
        private ITimeConverter _timeConverter;  // time functions service
        private MetroWindow _currentWindow;     // parent window property
        private float _secondsToConvert;
        private string _convertedHours;
        private DateTime _hoursToConvert;
        private string _convertedSeconds;
        private bool _convertSecondsToHoursAction;
        private bool _convertHoursToSecondsAction;

        // INotifyPropertyChanged event ............................
        public event PropertyChangedEventHandler PropertyChanged;

        // On-Property-Changed 
        private void OnPropertyChanged([CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }

        // Properties .........................................
        public MetroWindow CurrentWindow
        {
            get { return _currentWindow; }
            set { _currentWindow = value; }
        }
       
        public float SecondsToConvert
        {
            get { return _secondsToConvert; }
            set
            {
                if (value != _secondsToConvert)
                {
                    _secondsToConvert = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ConvertedHours
        {
            get { return _convertedHours; }
            set
            {
                if (value != _convertedHours)
                {
                    _convertedHours = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime HoursToConvert
        {
            get { return _hoursToConvert; }
            set
            {
                if (value != _hoursToConvert)
                {
                    _hoursToConvert = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ConvertedSeconds
        {
            get { return _convertedSeconds; }
            set
            {
                if (value != _convertedSeconds)
                {
                    _convertedSeconds = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool ConvertSecondsToHoursAction
        {
            get { return _convertSecondsToHoursAction; }
            set
            {
                if (value != _convertSecondsToHoursAction)
                {
                    _convertSecondsToHoursAction = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool ConvertHoursToSecondsAction
        {
            get { return _convertHoursToSecondsAction; }
            set
            {
                if (value != _convertHoursToSecondsAction)
                {
                    _convertHoursToSecondsAction = value;
                    OnPropertyChanged();
                }
            }
        }

        // Commands 
        public ICommand ConvertCommand { get; set; }

        // Constructors .......................................
        public MainWindowViewModel(ITimeConverter timeConverter)
        {
            // inject timeConverter library
           _timeConverter = timeConverter;

            // Prepare commands 
            LoadCommands();

            // Iitialize values
            _secondsToConvert = 0;
            _convertedHours = "";
            _hoursToConvert = DateTime.Now;
            _convertedSeconds = "";
            // Selection Box
            _convertSecondsToHoursAction = true;
            _convertHoursToSecondsAction = false;
        }

        private void LoadCommands()
        {
            ConvertCommand = new CustomCommand(ConvertTime, CanConvertTime);
        }

        private void ConvertTime(object obj)
        {

            float second = _secondsToConvert;
            string convertedHours = _convertedHours;
            DateTime hoursToConvert = _hoursToConvert;
            string convertedSeconds = _convertedSeconds;
            bool convertSecondsToHoursAction = _convertSecondsToHoursAction;
            bool convertHoursToSecondsAction = _convertHoursToSecondsAction;

        }

        private bool CanConvertTime(object obj)
        {
            return true;
        }


    }
}
