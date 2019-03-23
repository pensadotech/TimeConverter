using System;
using System.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using MahApps.Metro;
using WpfClient.ColorsAndTheme;
using WpfClient.Extensions;
using WpfClient.Messages;
using WpfClient.Utilities;

namespace WpfClient.ViewModels
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        // INotifyPropertyChanged event handler ............................
        public event PropertyChangedEventHandler PropertyChanged;

        // On-Property-Changed 
        private void OnPropertyChanged([CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }

        // Private memebers .....................................
        private ObservableCollection<AccentColorData> _accentColorDataList;
        private ObservableCollection<AppThemeData> _appThemeDataList;
        private AccentColorData _selectedAccentColorData;
        private AppThemeData _SelectedAppThemeData;

        // Properties .......................................... 
        public ObservableCollection<AccentColorData> AccentColors
        {
            get { return _accentColorDataList; }
            set
            {
                if (value != _accentColorDataList)
                {
                    _accentColorDataList = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<AppThemeData> AppThemes
        {
            get { return _appThemeDataList; }
            set
            {
                if (value != _appThemeDataList)
                {
                    _appThemeDataList = value;
                    OnPropertyChanged();
                }
            }
        }

        public AccentColorData SelectedAccentColor
        {
            get { return _selectedAccentColorData; }
            set
            {
                if (value != _selectedAccentColorData)
                {
                    _selectedAccentColorData = value;
                    OnPropertyChanged();
                }

            }
        }

        public AppThemeData SelectedThemeData
        {
            get { return _SelectedAppThemeData; }
            set
            {
                if (value != _SelectedAppThemeData)
                {
                    _SelectedAppThemeData = value;
                    OnPropertyChanged();
                }
            }
        }

        // Commands can be added here for specific actions
        // example: public ICommand SaveCommand { get; set; } 
        public ICommand SaveSettingsCommad { get; set; }

        // Constructors .......................................
        public SettingsViewModel()
        {
            // Prepare commands 
            LoadAccentColorsAndThemes();
            LoadCommands();
            
        }

        // Colors and themes ...................................................
        public void LoadAccentColorsAndThemes()
        {
            // create accent color menu items for the demo
            _accentColorDataList = ThemeManager.Accents
                .Select(a => new AccentColorData()
                    {
                        Name = a.Name,
                        ColorBrush = a.Resources["AccentColorBrush"] as Brush
                    }
                ).ToList().ToObservableCollection();

            // create metro theme color menu items for the demo
            _appThemeDataList = ThemeManager.AppThemes
                .Select(a => new AppThemeData()
                    {
                        Name = a.Name,
                        BorderColorBrush = a.Resources["BlackColorBrush"] as Brush,
                        ColorBrush = a.Resources["WhiteColorBrush"] as Brush
                    }
                ).ToList().ToObservableCollection();

            // Detect current Accent Color
            Tuple<AppTheme, Accent> appStyle = ThemeManager.DetectAppStyle(Application.Current);
            _selectedAccentColorData = _accentColorDataList.FirstOrDefault(a => a.Name == appStyle.Item2.Name);

            // Detect current Aplicaiton Theme 
            Tuple<AppTheme, Accent> theme = ThemeManager.DetectAppStyle(Application.Current);
            _SelectedAppThemeData = _appThemeDataList.FirstOrDefault(t => t.Name == theme.Item1.Name);

        }

        // Commands ............................................................
        private void LoadCommands()
        {
            SaveSettingsCommad = new CustomCommand(SaveSettings, CanSaveSettings);
        }

        private void SaveSettings(object obj)
        {
            SetAccentColor();
            SetAppTheme();

            var settingMsg = new SettingMessage()
            {
                IsOpen = false
            };

            // Message out 
            // Note: For multi message, use context in order to have a unique identifier
            Messenger.Default.Send<SettingMessage>(settingMsg, "Flyout");
        }

        private bool CanSaveSettings(object obj)
        {
            return true;
        }

        // Saving Colors and Theme ................................................
        private void SetAccentColor()
        {
            Tuple<AppTheme, Accent> theme = ThemeManager.DetectAppStyle(Application.Current);
            Accent accent = ThemeManager.GetAccent(SelectedAccentColor.Name);
            ThemeManager.ChangeAppStyle(Application.Current, accent, theme.Item1);
        }

        private void SetAppTheme()
        {
            Tuple<AppTheme, Accent> theme = ThemeManager.DetectAppStyle(Application.Current);
            AppTheme appTheme = ThemeManager.GetAppTheme(SelectedThemeData.Name);
            ThemeManager.ChangeAppStyle(Application.Current, theme.Item2, appTheme);
        }
    }
}
