using gehoortest.application_Repository.Models.TestData_Management;
using gehoortest.application_User.Interface.ViewModels;
using gehoortest.application_User.Interface.Views;
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
        app.MainWindow = host.Services.GetRequiredService<StartTest>();
        app.MainWindow.Visibility = Visibility.Visible;
        app.Run();
    }
    protected static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                //Copy the 2 lines of code below to add new View and ViewModel.
                services.AddSingleton<StartTestViewModel>();
                services.AddSingleton(s => new StartTest()
                {
                    DataContext = s.GetRequiredService<StartTestViewModel>()
                });
                //db string, set value in appsettings.json
                string? connectionString = hostContext.Configuration.GetConnectionString("Default");

                services.AddSingleton(new TargetAudienceRepository(connectionString));
                services.AddSingleton(new TestRepository(connectionString));

                //Add refrence to a Repository (Like above)

            });
}
