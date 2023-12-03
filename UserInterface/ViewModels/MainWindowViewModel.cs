using UserInterface.Stores;

namespace UserInterface.ViewModels;

internal class MainWindowViewModel : ViewModelBase
{
    private NavigationStore navigationStore;

    private ViewModelBase? currentViewModel;
    private ViewModelBase? currentModalViewModel;
    private string showModal = "Hidden";

    public ViewModelBase? CurrentViewModel
    {
        get { return currentViewModel; }
        set { currentViewModel = value; OnPropertyChanged(nameof(CurrentViewModel)); }
    }
    public ViewModelBase? CurrentModalViewModel
    {
        get { return currentModalViewModel; }
        set { currentModalViewModel = value; OnPropertyChanged(nameof(CurrentModalViewModel)); }
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