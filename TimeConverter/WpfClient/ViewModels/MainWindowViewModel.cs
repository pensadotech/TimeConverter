using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using MahApps.Metro.Controls;
using TimeConverter.Service;
using WpfClient.Extensions;
using WpfClient.Messages;
using WpfClient.Utilities;

namespace WpfClient.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        // Private members .............................................
        // Services
        private ITimeConverter _timeConverter;  
        // parent window property
        private MetroWindow _currentWindow;     
        // Properties
        private double _secondsToConvert;
        private string _convertedHours;
        private DateTime _hoursToConvert;
        private string _convertedSeconds;
        private bool _convertSecondsToHoursAction;
        private bool _convertHoursToSecondsAction;
        // Flyout
        private bool _isSettingsFlyoutOpen;

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
       
        public double SecondsToConvert
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

        public bool IsSettingsFlyoutOpen
        {
            get { return _isSettingsFlyoutOpen; }
            set
            {
                if (value != _isSettingsFlyoutOpen)
                {
                    _isSettingsFlyoutOpen = value;
                    OnPropertyChanged();
                }
            }
        }


        // Commands 
        public ICommand ConvertCommand { get; set; }
        public ICommand RefreshTimeCommand { get; set; }
        public ICommand SettingsCommand { get; set; }

        // Constructors .......................................
        public MainWindowViewModel(ITimeConverter timeConverter)
        {
            // inject timeConverter library
           _timeConverter = timeConverter;

            // Prepare commands 
            LoadCommands();

            // Initialize Screen
            InitializeScreen(true);

            // Register to messanger
            // Note: For multi message, use context in order to have a unique identifier
            Messenger.Default.Register<SettingMessage>(this, OnSettingMessageReceived, "Flyout");
        }

        // Methods .............................................

        /// <summary>
        /// Initialize screen 
        /// </summary>
        /// <param name="initSelecionBox"></param>
        private void InitializeScreen(bool initSelecionBox)
        {
            // Iitialize values
            DateTime curentTime = DateTime.Now;
            SecondsToConvert = _timeConverter.ConvertString24HrTimeToSeconds(curentTime.ToString("HH:mm:ss"));
            ConvertedHours = "";
            HoursToConvert = curentTime;
            ConvertedSeconds = "";

            if (initSelecionBox)
            {
                // Initialize Selection Box
                ConvertSecondsToHoursAction = true;
                ConvertHoursToSecondsAction = false;
            }
        }

        // Messages ..................................
        private void OnSettingMessageReceived(SettingMessage obj)
        {
            var settingMessage = obj as SettingMessage;
            IsSettingsFlyoutOpen = settingMessage.IsOpen;
        }

        // commands .........................................
        private void LoadCommands()
        {
            ConvertCommand = new CustomCommand(ConvertTime, CanConvertTime);
            RefreshTimeCommand = new CustomCommand(RefreshTime, CanRefreshTime);
            SettingsCommand = new CustomCommand(OpenSettings, CanOpenSettings);
        }

        private void ConvertTime(object obj)
        {
            if (_convertSecondsToHoursAction)
            {
                // Convert entered seconds into DateTime and retunr time into screen
                DateTime resultDateTime = _timeConverter.ConvertSecondsToCurrentDateTime(_secondsToConvert);
                ConvertedHours = resultDateTime.ToString("HH:mm:ss");

            } else if (_convertHoursToSecondsAction)
            {
                // Convert time in 24 hrs into seconds and display result in screen 
                double resultSecs = _timeConverter.ConvertString24HrTimeToSeconds(_hoursToConvert.ToString("HH:mm:ss"));
                ConvertedSeconds = $"{resultSecs:#,#0.00}"; // to String
            }
        }

        private bool CanConvertTime(object obj)
        {
            return true;
        }

        private void RefreshTime(object obj)
        {
            // Initialize Screen
            InitializeScreen(false);
        }

        private bool CanRefreshTime(object obj)
        {
            return true;
        }

        public void OpenSettings(object obj)
        {
            IsSettingsFlyoutOpen = true;

            // Option #1 : Traditional look (no Flyouts)
            // The settings need to be a MetroContentControl
            // and it is emebeded into a new window
            //var w = new Window();
            //w.Title = "Settings";
            //w.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            //w.Content=new SettingsView();
            //w.SizeToContent = SizeToContent.WidthAndHeight;
            //w.Owner = Application.Current.MainWindow;
            //w.ShowDialog();

        }

        public bool CanOpenSettings(object obj)
        {
            return true;
        }

    }
}
