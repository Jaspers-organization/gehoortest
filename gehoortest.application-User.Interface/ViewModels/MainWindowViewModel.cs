using CommunityToolkit.Mvvm.ComponentModel;
using gehoortest.application_Repository.Models.TestData_Management;
using gehoortest.application_User.Interface.Commands;
using gehoortest_application.Repository;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace gehoortest.application_User.Interface.ViewModels;

public class MainWindowViewModel : ObservableObject
{
    public MainWindowViewModel(Repository context)
    {
        TargetAudience = context.GetDataFromTable<Target_Audience>();
        // TargetAudience = context.GetDataFromTable<Target_Audience>(entity => entity.From == 19);
        TestCommand = new TestCommand();
    }
    public ICommand TestCommand { get; }

    public ObservableCollection<Target_Audience>? TargetAudience { get; }
}
