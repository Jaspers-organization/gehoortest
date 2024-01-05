using BusinessLogic.IRepositories;
using BusinessLogic.Models;
using BusinessLogic.Projections;
using BusinessLogic.Services;
using DataAccess.Repositories;
using System;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using UserInterface.Commands;
using UserInterface.Stores;

namespace UserInterface.ViewModels;

internal class MainWindowViewModel : ViewModelBase
{
    private NavigationStore navigationStore;

    #region properties
    private readonly SettingService settingService;
    private ViewModelBase? _currentViewModel;
    public ViewModelBase? CurrentViewModel
    {
        get { return _currentViewModel; }
        set { _currentViewModel = value; OnPropertyChanged(nameof(CurrentViewModel)); }
    }

    private ViewModelBase? _currentModalViewModel;
    public ViewModelBase? CurrentModalViewModel
    {
        get { return _currentModalViewModel; }
        set { _currentModalViewModel = value; OnPropertyChanged(nameof(CurrentModalViewModel)); }
    }

    private Visibility _showModal = Visibility.Hidden;
    public Visibility ShowModal
    {
        get { return _showModal; }
        set { _showModal = value; OnPropertyChanged(nameof(ShowModal)); }
    }

    private Visibility _showLogoutButton = Visibility.Hidden;
    public Visibility ShowLogoutButton
    {
        get { return _showLogoutButton; }
        set { _showLogoutButton = value; OnPropertyChanged(nameof(ShowLogoutButton)); }
    }

    private Visibility _showBackButton = Visibility.Hidden;
    public Visibility ShowBackButton
    {
        get { return _showBackButton; }
        set { _showBackButton = value; OnPropertyChanged(nameof(ShowBackButton)); }
    }

    private Visibility _showCloseApplicationButton = Visibility.Hidden;
    public Visibility ShowCloseApplicationButton
    {
        get { return _showCloseApplicationButton; }
        set { _showCloseApplicationButton = value; OnPropertyChanged(nameof(ShowCloseApplicationButton)); }
    }
    #endregion

    #region commands
    public ICommand OpenLoginCommand => new Command(OpenLogin);
    public ICommand LogoutCommand => new Command(Logout);
    public ICommand BackCommand => new Command(Back);
    public ICommand CloseApplicationCommand => new Command(CloseApplication);
    public ICommand DoNothingCommand => new Command(DoNothing);
    #endregion

    public MainWindowViewModel(NavigationStore navigationStore)
    {
        this.navigationStore = navigationStore;

        this.navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        this.navigationStore.IsModalOpenChanged += OnIsModalOpenChanged;
        this.navigationStore.LoggedInEmployeeChanged += LoggedInEmployeeChanged;
        this.navigationStore.PreviousViewModelChanged += PreviousViewModelChanged;
        this.navigationStore.HideTopBarChanged += ToggleTopBar;
        CurrentViewModel = this.navigationStore.CurrentViewModel;
        
        ISettingsRepository settingsRepository = new SettingsRepository();
        settingService = new SettingService(settingsRepository);
        GetColorSetting();
    }

    private void OpenLogin()
    {
        if (navigationStore.LoggedInEmployee != null || navigationStore.CurrentViewModel is not HomeViewModel) return;

        navigationStore.CurrentViewModel = new LoginViewModel(navigationStore);
    }

    private void Logout()
    {
        navigationStore.LoggedInEmployee = null;
        ShowLogoutButton = Visibility.Hidden;
        ShowCloseApplicationButton = Visibility.Hidden;
        navigationStore.CurrentViewModel = new HomeViewModel(navigationStore);
    }    
    private void ToggleTopBar()
    {
        if (navigationStore.HideTopBar)
        {
            ShowBackButton = Visibility.Hidden;
            ShowCloseApplicationButton = Visibility.Hidden;
            ShowLogoutButton = Visibility.Hidden;
        }
        else
        {
            ShowLogoutButton = Visibility.Visible;
            ShowBackButton = Visibility.Visible;
            ShowCloseApplicationButton = Visibility.Visible;
        }

    }
    private void Back()
    {
        if (navigationStore.PreviousViewModel.Count == 0)
        {
            ShowBackButton = Visibility.Hidden;
            return;
        }

        ViewModelBase previousViewModel = navigationStore.PreviousViewModel.Pop();

        if (navigationStore.PreviousViewModel.Count == 0)
        {
            ShowBackButton = Visibility.Hidden;
        }

        navigationStore.CurrentViewModel = previousViewModel;
    }

    private void CloseApplication()
    {
        System.Windows.Application.Current.Shutdown();
    }

    private void OnCurrentViewModelChanged()
    {
        CurrentViewModel = navigationStore.CurrentViewModel;
    }

    private void OnIsModalOpenChanged()
    {
        CurrentModalViewModel = navigationStore.CurrentModalViewModel;
        ShowModal = navigationStore.IsModalOpen ? Visibility.Visible : Visibility.Hidden;
    }

    private void LoggedInEmployeeChanged()
    {
        ShowLogoutButton = navigationStore.LoggedInEmployee == null ? Visibility.Hidden : Visibility.Visible;
        ShowCloseApplicationButton = navigationStore.LoggedInEmployee == null ? Visibility.Hidden : Visibility.Visible;
    }

    private void PreviousViewModelChanged()
    {
        ShowBackButton = navigationStore.PreviousViewModel.Count == 0 ? Visibility.Hidden : Visibility.Visible;
    }

    /// <summary>
    /// This method is only used to make sure the application can't be stopped using alt+f4
    /// </summary>
    private void DoNothing() { }

    private void GetColorSetting()
    {
        Settings savedSettings = settingService.GetSetting();
        System.Windows.Media.Color mediacolor = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(savedSettings.Color);
        SolidColorBrush solidColorBrush = new SolidColorBrush(mediacolor);

        var drawingcolor = System.Drawing.Color.FromArgb(mediacolor.A, mediacolor.R, mediacolor.G, mediacolor.B);
        System.Drawing.Color lighterColor = ControlPaint.LightLight(drawingcolor);

        System.Windows.Media.Color lighterMediaColor = System.Windows.Media.Color.FromArgb(lighterColor.A, lighterColor.R, lighterColor.G, lighterColor.B);
        SolidColorBrush secondaryColorHighlight = new SolidColorBrush(lighterMediaColor);

        ResourceDictionary resourceDict = new ResourceDictionary();
        resourceDict.Source = new Uri("../../Assets/Styling/Colors.xaml", UriKind.RelativeOrAbsolute);
        App.Current.Resources["SecondaryColor"] = solidColorBrush;
        App.Current.Resources["SecondaryColor_Highlight"] = secondaryColorHighlight;
    }

   
}