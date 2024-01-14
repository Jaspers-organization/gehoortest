using BusinessLogic.Services;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using UserInterface.Commands;
using UserInterface.Stores;
using DataAccess.Repositories;
using BusinessLogic.Models;
using UserInterface.ViewModels.Modals;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using BusinessLogic.Interfaces.Repositories;

namespace UserInterface.ViewModels
{
    internal class SettingsViewModel : ViewModelBase, INotifyPropertyChanged
    {
        #region Dependencies
        private readonly NavigationStore navigationStore;
        private readonly SettingService settingService;

        #endregion Dependencies 

        #region Commands
        public ICommand SaveSetting => new Command(ValidateColor);

        #endregion Commands

        #region Properties
        private SolidColorBrush savedColor;
        public SolidColorBrush SavedColor
        {
            get
            {
                return savedColor;
            }
            set
            {
                savedColor = value;
                OnPropertyChanged(nameof(SavedColor));
            }
        }

        private System.Windows.Media.Color selectedColor;
        public  System.Windows.Media.Color SelectedColor
        {
            get
            {
                return selectedColor;
            }
            set
            {
                selectedColor = value;
                OnPropertyChanged(nameof(SelectedColor));
                SetColor();
            }
        }

        protected BusinessLogic.Classes.Color convertedColor;
        private Settings savedSettings { get; set; }

        #endregion Properties

        #region Constructor

        public SettingsViewModel(NavigationStore navigationStore)
        {
            this.navigationStore = navigationStore;
            this.navigationStore.AddPreviousViewModel(new EmployeePortalViewModel(navigationStore));

            ISettingsRepository settingsRepository = new SettingsRepository();
            settingService = new SettingService(settingsRepository);
            GetSetting();
        }
        #endregion Constructor

        private void SetColor()
        {
            SolidColorBrush solidColorBrush = new SolidColorBrush(SelectedColor);
            if ( SavedColor != null && solidColorBrush != SavedColor)
            {
                SavedColor = solidColorBrush;
            }
        }

        private void GetSetting()
        {
            savedSettings = settingService.GetSetting();
            System.Windows.Media.Color mediacolor = (System.Windows.Media.Color)ColorConverter.ConvertFromString(savedSettings.Color);

            var drawingcolor = System.Drawing.Color.FromArgb(mediacolor.A, mediacolor.R, mediacolor.G, mediacolor.B);
            System.Drawing.Color lighterColor = ControlPaint.LightLight(drawingcolor);

            System.Windows.Media.Color lighterMediaColor = System.Windows.Media.Color.FromArgb(lighterColor.A, lighterColor.R, lighterColor.G, lighterColor.B);

            SelectedColor = (System.Windows.Media.Color)ColorConverter.ConvertFromString(savedSettings.Color);
            SolidColorBrush solidColorBrush = new SolidColorBrush(mediacolor);
            SavedColor = solidColorBrush;

            SolidColorBrush secondaryColorHighlight = new SolidColorBrush(lighterMediaColor);

            ResourceDictionary resourceDict = new ResourceDictionary();
            resourceDict.Source = new Uri("../../Assets/Styling/Colors.xaml", UriKind.RelativeOrAbsolute);
            App.Current.Resources["SecondaryColor"] = solidColorBrush;
            App.Current.Resources["SecondaryColor_Highlight"] = secondaryColorHighlight;
        }

        public void ValidateColor()
        {
            ConvertColor(SelectedColor);
            if (!settingService.ValidateColor(convertedColor))
            {
                OpenErrorModal("De alpha waarde van de kleur mag niet lager dan 255 zijn. Kies een nieuwe kleur.");
            }
            else
            {
                UpdateSetting();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            
        }

        public void  ConvertColor(System.Windows.Media.Color selColor)
        {
            convertedColor = new BusinessLogic.Classes.Color(selColor.A, selColor.R, selColor.G, selColor.B,selColor.ToString());
            savedSettings.Color = convertedColor.Hex;
        }
        
        private void UpdateSetting()
        {
            settingService.UpdateSetting(savedSettings);
            GetSetting();
        }

        private void OpenErrorModal(string text) => navigationStore.OpenModal(new ErrorModalViewModal(navigationStore, text));
    }
}
