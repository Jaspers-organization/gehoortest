using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using UserInterface.Commands;
using UserInterface.Stores;
using Xceed.Wpf.Toolkit;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;
using BusinessLogic.Classes;
using BusinessLogic.IRepositories;
using DataAccess.Repositories;
using BusinessLogic.Models;
using UserInterface.ViewModels.Modals;

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
            this.navigationStore.HideTopBar = false;

            ISettingsRepository settingsRepository = new SettingsRepository();
            settingService = new SettingService(settingsRepository);
            GetSetting();
        }
        #endregion Constructor

        private void GetSetting()
        {
            savedSettings = settingService.GetSetting();
            System.Windows.Media.Color color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(savedSettings.Color);
          
            SelectedColor = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(savedSettings.Color);
            SolidColorBrush solidColorBrush = new SolidColorBrush(color);
            SavedColor = solidColorBrush;

            ResourceDictionary resourceDict = new ResourceDictionary();
            resourceDict.Source = new Uri("../../Assets/Styling/Colors.xaml", UriKind.RelativeOrAbsolute);
            App.Current.Resources["SecondaryColor"] = solidColorBrush;
        }

        public void ValidateColor()
        {
            ConvertColor(SelectedColor);
            if (!settingService.ValidateColor(convertedColor))
            {
                OpenErrorModal("De alpha waarde van de kleur mag niet 0 zijn. Kies een nieuwe kleur.");
            }
            else
            {
                UpdateSetting();
            }
        }

        public void  ConvertColor(System.Windows.Media.Color selColor)
        {
            convertedColor = new BusinessLogic.Classes.Color(selColor.R, selColor.G, selColor.B, selColor.A, selColor.ToString());
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
