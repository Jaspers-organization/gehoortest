using gehoortest.application_User.Interface.Stores;
using gehoortest.application_User.Interface.ViewModels;
using gehoortest.application_User.Interface.Views;
using System.Windows;

namespace gehoortest.application_User.Interface;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly NavigationStore _navigationStore;

    public App()
    {
        _navigationStore = new NavigationStore();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        _navigationStore.CurrentViewModel = new StartTestViewModel(_navigationStore);

        MainWindow = new MainWindow();
        MainWindow.DataContext = new MainWindowViewModel(_navigationStore);
        MainWindow.Show();

        base.OnStartup(e);
    }
}
