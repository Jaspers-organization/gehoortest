using UserInterface.Stores;
using UserInterface.Views;
using System.Windows;
using UserInterface.ViewModels;
using DataAccess.Repositories;
using System.Windows.Media;

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
        // TargetAudienceRepository repository = new TargetAudienceRepository();
        // repository.FillTargetAudiences();
   
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        //MainWindow = new Sandbox();
   
        navigationStore.CurrentViewModel = new HomeViewModel(navigationStore);
        MainWindow = new MainWindow();
        MainWindow.DataContext = new MainWindowViewModel(navigationStore);

        MainWindow.Show();

        base.OnStartup(e);
    }
}
