using UserInterface.Assets.Styling;
using UserInterface.Stores;
using UserInterface.ViewModels;
using UserInterface.Views;
using System.Windows;

namespace UserInterface;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly NavigationStore navigationStore;

    public App()
    {
        navigationStore = new NavigationStore();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        //MainWindow = new Sandbox();
 
        //_navigationStore.CurrentViewModel = new StartTestViewModel(navigationStore);
        //_navigationStore.CurrentViewModel = new StartTestViewModel(navigationStore);
        navigationStore.CurrentViewModel = new TestManagementViewModel(navigationStore);
        MainWindow = new MainWindow();
        MainWindow.DataContext = new MainWindowViewModel(navigationStore);

        MainWindow.Show();

        base.OnStartup(e);
    }
}
