using gehoortest.application_Repository;
using gehoortest.application_Repository.Models.TestData_Management;
using gehoortest.application_User.Interface.ViewModels;
using gehoortest_application.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;

namespace gehoortest.application_User.Interface;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    [STAThread]
    protected static void Main(string[] args)
    {
        using IHost host = CreateHostBuilder(args).Build();
        host.Start();

        App app = new();
        app.InitializeComponent();
        app.MainWindow = host.Services.GetRequiredService<MainWindow>();
        app.MainWindow.Visibility = Visibility.Visible;
        app.Run();
    }
    protected static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                //Copy the 2 lines of code below to add new View and ViewModel.
                services.AddSingleton<MainWindowViewModel>();
                services.AddSingleton(s => new MainWindow()
                {
                    DataContext = s.GetRequiredService<MainWindowViewModel>()
                });
                //db string, set value in appsettings.json
                string? connectionString = hostContext.Configuration.GetConnectionString("Default");

                services.AddSingleton(new TargetAudienceRepository(connectionString));

                //Add refrence to a Repository (Like above)

            });
}
