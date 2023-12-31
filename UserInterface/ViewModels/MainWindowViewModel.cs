using BusinessLogic.Projections;
using System;
using System.ComponentModel;
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

    private Visibility _showEnlargeTekstButton = Visibility.Visible;
    public Visibility ShowEnlargeTekstButton
    {
        get { return _showEnlargeTekstButton; }
        set { _showEnlargeTekstButton = value; OnPropertyChanged(nameof(ShowEnlargeTekstButton)); }
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
    }

    private void OpenLogin()
    {
        if (navigationStore.LoggedInEmployee != null || navigationStore.CurrentViewModel is not HomeViewModel) return;
        ShowEnlargeTekstButton = Visibility.Hidden;
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
        ShowEnlargeTekstButton = Visibility.Visible;

        navigationStore.CurrentViewModel = previousViewModel;
    }

    private void CloseApplication()
    {
        Application.Current.Shutdown();
    }

    private void OnCurrentViewModelChanged()
    {
        CurrentViewModel = navigationStore.CurrentViewModel;
    }

    private void ChangeToBig(ResourceDictionary resourceDict)
    {
        UpdateResource(resourceDict, "Text", 40.0);
        UpdateResource(resourceDict, "SubHeader", 48.0);
        UpdateResource(resourceDict, "Header", 56.0);
    }
    private void ChangeToSmall(ResourceDictionary resourceDict)
    {
        UpdateResource(resourceDict, "Text", 32.0);
        UpdateResource(resourceDict, "SubHeader", 40.0);
        UpdateResource(resourceDict, "Header", 48.0);
    }
    private void UpdateResource(ResourceDictionary resourceDict, string field, double value)
    {
        ((Style)resourceDict[field]).Setters.Add(new Setter(TextBlock.FontSizeProperty, value));
    }
    private void ChangeTextSize()
    {
        ResourceDictionary resourceDict = new ResourceDictionary { Source = new Uri("pack://application:,,,/UserInterface;component/Assets/Styling/TextStyles.xaml") };

        if (!IsBigFontSize)
            ChangeToBig(resourceDict);
        else
            ChangeToSmall(resourceDict);

        Application.Current.Resources.MergedDictionaries.Add(resourceDict);

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
}