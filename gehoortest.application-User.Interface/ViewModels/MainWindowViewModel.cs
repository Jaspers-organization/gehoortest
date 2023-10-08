using CommunityToolkit.Mvvm.ComponentModel;
using gehoortest.application_Repository.Models.TestData_Management;
using gehoortest.application_User.Interface.Commands;
using gehoortest_application.Repository;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace gehoortest.application_User.Interface.ViewModels;

public class MainWindowViewModel : ObservableObject
{    
    public MainWindowViewModel(TargetAudienceRepository context)
    {
       // TargetAudience = context.GetDataFromTable<Target_Audience>();
        TargetAudience = context.GetAllAgesBelow(70);
        
    }
    public ICommand TestCommand { get; }

    //can this be generic?
    public ObservableCollection<TargetAudience>? TargetAudience { get; }
}
