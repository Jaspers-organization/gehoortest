using UserInterface.Stores;

namespace UserInterface.ViewModels;

internal class MainWindowViewModel : ViewModelBase
{
    private NavigationStore navigationStore;

    private string showModal = "Hidden";

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
    public string ShowModal
    {
        get { return showModal; }
        set { showModal = value; OnPropertyChanged(nameof(ShowModal)); }
    }

    public MainWindowViewModel(NavigationStore navigationStore)
    {
        this.navigationStore = navigationStore;

        this.navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        this.navigationStore.IsModalOpenChanged += OnIsModalOpenChanged;

        CurrentViewModel = this.navigationStore.CurrentViewModel;
    }

    private void OnCurrentViewModelChanged()
    {
        CurrentViewModel = navigationStore.CurrentViewModel;
    }

    private void OnIsModalOpenChanged()
    {
        CurrentModalViewModel = navigationStore.CurrentModalViewModel;
        ShowModal = navigationStore.IsModalOpen ? "Visible" : "Hidden";
    }
}