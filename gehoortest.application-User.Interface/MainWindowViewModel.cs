using CommunityToolkit.Mvvm.ComponentModel;
using gehoortest.application_Repository.Models.TestData_Management;
using gehoortest_application.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gehoortest.application_User.Interface;

public class MainWindowViewModel : ObservableObject
{
    public MainWindowViewModel(Repository context)
    {
        context.Set<Target_Audience>().ToList();
        TargetAudience = context.Set<Target_Audience>().Local.ToObservableCollection();        
    }

    public ObservableCollection<Target_Audience>? TargetAudience { get; }
}
