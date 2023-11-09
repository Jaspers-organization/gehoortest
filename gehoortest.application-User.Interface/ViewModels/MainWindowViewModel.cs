using CommunityToolkit.Mvvm.ComponentModel;
using DataAccess.Repositories;
using gehoortest.application_User.Interface.Commands;
using gehoorttest.application_Service.Classes;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace gehoortest.application_User.Interface.ViewModels;

public class MainWindowViewModel : ObservableObject
{
    public MainWindowViewModel(TestRepository context)
    {
        // TargetAudience = new ObservableCollection<Test>(context.GetAllActiveTests());
        //TargetAudience = new ObservableCollection<Test>(context.GetAllActiveTests());

    }
    public ICommand TestCommand { get; }

    //can this be generic?
    public ObservableCollection<Test>? TargetAudience { get; }
}
