using UserInterface.Stores;
using UserInterface.Views;
using System.Windows;
using UserInterface.ViewModels;

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

        navigationStore.CurrentViewModel = new TestViewModel(navigationStore);
        MainWindow = new MainWindow();
        MainWindow.DataContext = new MainWindowViewModel(navigationStore);

        MainWindow.Show();

        base.OnStartup(e);
    }
}
