using System;
using System.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Media;
using MahApps.Metro;
using TimeConverter.Domain.Interfaces.Services;
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
        // Keys for operation
        private const string Color_Config_Key = "Color";
        private const string Theme_Config_Key = "Theme";
        private const string Flyout_msg_key = "Flyout";
        // Config service
        private IUserConfigService _userConfigService;
        // Parameters
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
        public SettingsViewModel(IUserConfigService userConfigService)
        {
            // configuration service to use
            _userConfigService = userConfigService;
            // Prepare commands 
            LoadCommands();
            // Load collors and themes
            LoadAccentColorsAndThemes();
            // Load colors and themes from settings
            LoadColrAndThemeFromSettings();
            // apply colors and them to screen
            InitializeColorAndTheme();
        }

        // Methods .........................................................
      
        // Commands 
        private void LoadCommands()
        {
            SaveSettingsCommad = new CustomCommand(SaveSettings, CanSaveSettings);
        }

        private void SaveSettings(object obj)
        {
            // Apply color and theme
            ApplyAccentColor(_selectedAccentColorData);
            ApplyAppTheme(_SelectedAppThemeData);

            // SAVE: set color and theme in config file, and save the file
            _userConfigService.SetConfigItem(Color_Config_Key, _selectedAccentColorData.Name);
            _userConfigService.SetConfigItem(Theme_Config_Key, _SelectedAppThemeData.Name);
            _userConfigService.SaveUserConfiguration();

            // Prepare message for parent window
            var settingMsg = new SettingMessage()
            {
                IsOpen = false
            };

            // Send message 
            // Note: For multi message, use context in order to have a unique identifier
            Messenger.Default.Send<SettingMessage>(settingMsg, Flyout_msg_key);
        }

        private bool CanSaveSettings(object obj)
        {
            return true;
        }

        // Colors and themes 
        public void LoadAccentColorsAndThemes()
        {
            // Get a list of available Accent Colors
            _accentColorDataList = ThemeManager.Accents
                .Select(a => new AccentColorData()
                    {
                        Name = a.Name,
                        ColorBrush = a.Resources["AccentColorBrush"] as Brush
                    }
                ).ToList().ToObservableCollection();

            // Get a list of available Themes
            _appThemeDataList = ThemeManager.AppThemes
                .Select(a => new AppThemeData()
                    {
                        Name = a.Name,
                        BorderColorBrush = a.Resources["BlackColorBrush"] as Brush,
                        ColorBrush = a.Resources["WhiteColorBrush"] as Brush
                    }
                ).ToList().ToObservableCollection();
        }

        private void LoadColrAndThemeFromSettings()
        {
            // get from config object the settings fro Coloer and Theme
            string colorSetting = _userConfigService.GetConfigItemValue(Color_Config_Key);
            string themeSetting = _userConfigService.GetConfigItemValue(Theme_Config_Key);

            // If found, convert to accent color and theme data
            if (colorSetting != String.Empty && themeSetting != String.Empty)
            {
                // Look AccentColor and theme in the available list, save it 
                _selectedAccentColorData = _accentColorDataList.FirstOrDefault(a => a.Name == colorSetting);
                // Look Theme Data in the available list, and apply it
                _SelectedAppThemeData = _appThemeDataList.FirstOrDefault(t => t.Name == themeSetting);
            }
        }


        public void InitializeColorAndTheme()
        {
            // if not color and theme has been defined yet, use application current color, 
            // otherwise apply the one from the settings
            if (_selectedAccentColorData == null || _SelectedAppThemeData == null)
            {
                // Detect application current Accent Color and Theme
                Tuple<AppTheme, Accent> appStyle = ThemeManager.DetectAppStyle(Application.Current);
                Tuple<AppTheme, Accent> theme = ThemeManager.DetectAppStyle(Application.Current);

                // Look AccentColor and theme in the available list, save it 
                _selectedAccentColorData = _accentColorDataList.FirstOrDefault(a => a.Name == appStyle.Item2.Name);
                // Look Theme Data in the available list, and apply it
                _SelectedAppThemeData = _appThemeDataList.FirstOrDefault(t => t.Name == theme.Item1.Name);

            }

            // Apply color and theme
            ApplyAccentColor(_selectedAccentColorData);
            ApplyAppTheme(_SelectedAppThemeData);
        }
        
        // Apply Accent color and Theme
        // Note: Keep color and them in independent functions, otherwise the ChnageAppStyle
        // works for the last element. There must be a way to apply both at same time
        private void ApplyAccentColor(AccentColorData tgtAccentColor)
        {
            Tuple<AppTheme, Accent> appStyle = ThemeManager.DetectAppStyle(Application.Current);
            Accent accentColor = ThemeManager.GetAccent(tgtAccentColor.Name);
            ThemeManager.ChangeAppStyle(Application.Current, accentColor, appStyle.Item1);
        }

        private void ApplyAppTheme(AppThemeData tgtThemeData)
        {
            Tuple<AppTheme, Accent> appStyle = ThemeManager.DetectAppStyle(Application.Current);
            AppTheme theme = ThemeManager.GetAppTheme(tgtThemeData.Name);
            ThemeManager.ChangeAppStyle(Application.Current, appStyle.Item2, theme);
        }

    }
}
