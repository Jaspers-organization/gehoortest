using System.Windows.Input;
using UserInterface.Commands;
using UserInterface.Stores;

namespace UserInterface.ViewModels;

internal class HomeViewModel : ViewModelBase
{
    #region dependencies
    private NavigationStore navigationStore;
    #endregion

    #region commands
    public ICommand StartTestCommand => new Command(StartTest);
    #endregion

    public HomeViewModel(NavigationStore navigationStore)
    {
        this.navigationStore = navigationStore;
    }

    private void StartTest()
    {
        navigationStore.CurrentViewModel = new TestViewModel(navigationStore);
    }
}
