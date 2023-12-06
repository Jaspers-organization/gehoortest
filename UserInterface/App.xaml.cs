using UserInterface.Stores;
using UserInterface.Views;
using System.Windows;
using UserInterface.ViewModels;
using gehoortest_application.Repository;

namespace UserInterface;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly NavigationStore navigationStore;
    private readonly Repository repository;

    public App()
    {
        navigationStore = new NavigationStore();
        //todo make this config?
        repository = new Repository();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        //MainWindow = new Sandbox();
        navigationStore.CurrentViewModel = new TestViewModel(navigationStore, repository);
        MainWindow = new MainWindow();
        MainWindow.DataContext = new MainWindowViewModel(navigationStore);

        MainWindow.Show();

        base.OnStartup(e);
    }
}
