using BusinessLogic.IRepositories;
using BusinessLogic.Models;
using System;
using BusinessLogic.Services;
using DataAccess.Repositories;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using UserInterface.Commands;
using UserInterface.Stores;

namespace UserInterface.ViewModels;

internal class MainWindowViewModel : ViewModelBase
{
    private NavigationStore navigationStore;
    private readonly SettingService settingService;
    private ResourceDictionary resourceDictTextStyles = new ResourceDictionary { Source = new Uri("pack://application:,,,/UserInterface;component/Assets/Styling/TextStyles.xaml") };
    
    #region properties
    private bool isBigFontSize = false;
    public bool IsBigFontSize
    {
        get { return isBigFontSize; }
        set
        {
            isBigFontSize = value;
            OnPropertyChanged(nameof(IsBigFontSize));
            OnPropertyChanged(nameof(ChangeText));
        }
    }

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

    private Visibility _showEnlargeTextButton = Visibility.Visible;
    public Visibility ShowEnlargeTextButton
    {
        get { return _showEnlargeTextButton; }
        set { _showEnlargeTextButton = value; OnPropertyChanged(nameof(ShowEnlargeTextButton)); }
    }

    private string changeText = "Vergroot tekst";
    public string ChangeText
    {
        get { return isBigFontSize ? "Verklein Tekst" : "Vergroot Tekst"; }
        set
        {
            if (changeText != value)
            {
                changeText = value;
                OnPropertyChanged(nameof(ChangeText));
            }
        }
    }
    #endregion

    #region commands
    public ICommand OpenLoginCommand => new Command(OpenLogin);
    public ICommand LogoutCommand => new Command(Logout);
    public ICommand BackCommand => new Command(Back);
    public ICommand CloseApplicationCommand => new Command(CloseApplication);
    public ICommand DoNothingCommand => new Command(DoNothing);
    public ICommand ChangeTextSizeCommand => new Command(ChangeTextSize);
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
        ShowEnlargeTextButton = Visibility.Hidden;
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
        ShowEnlargeTextButton = Visibility.Visible;

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

    private void ChangeToBig(ResourceDictionary resourceDict)
    {
        UpdateResource(resourceDict, TextBlock.FontSizeProperty, "Text", 40.0);
        UpdateResource(resourceDict, TextBlock.FontSizeProperty, "SubHeader", 48.0);
        UpdateResource(resourceDict, TextBlock.FontSizeProperty, "Header", 56.0);
    }
    private void ChangeToSmall(ResourceDictionary resourceDict)
    {
        UpdateResource(resourceDict,TextBlock.FontSizeProperty, "Text", 32.0);
        UpdateResource(resourceDict, TextBlock.FontSizeProperty, "SubHeader", 40.0);
        UpdateResource(resourceDict, TextBlock.FontSizeProperty, "Header", 48.0);
    }
    private void UpdateResource(ResourceDictionary resourceDict, DependencyProperty type, string field, double value)
    {
        Style existingStyle = resourceDict[field] as Style;
        if (existingStyle != null)
        {
            Style newStyle = new Style(typeof(TextBlock), existingStyle);
            newStyle.Setters.Add(new Setter(type, value));
            resourceDict[field] = newStyle;
        }
    }

    private void ChangeTextSize()
    {

        if (!IsBigFontSize)
            ChangeToBig(resourceDictTextStyles);
        else
            ChangeToSmall(resourceDictTextStyles);

        System.Windows.Application.Current.Resources.MergedDictionaries.Add(resourceDictTextStyles);

        IsBigFontSize = !IsBigFontSize;
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
        Color mediacolor = (Color)ColorConverter.ConvertFromString(savedSettings.Color);
        SolidColorBrush solidColorBrush = new SolidColorBrush(mediacolor);

        var drawingcolor = System.Drawing.Color.FromArgb(mediacolor.A, mediacolor.R, mediacolor.G, mediacolor.B);
        System.Drawing.Color lighterColor = System.Windows.Forms.ControlPaint.LightLight(drawingcolor);

        Color lighterMediaColor =Color.FromArgb(lighterColor.A, lighterColor.R, lighterColor.G, lighterColor.B);
        SolidColorBrush secondaryColorHighlight = new SolidColorBrush(lighterMediaColor);

        ResourceDictionary resourceDict = new ResourceDictionary();
        resourceDict.Source = new Uri("../../Assets/Styling/Colors.xaml", UriKind.RelativeOrAbsolute);
        App.Current.Resources["SecondaryColor"] = solidColorBrush;
        App.Current.Resources["SecondaryColor_Highlight"] = secondaryColorHighlight;
    }

   
}