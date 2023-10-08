using CommunityToolkit.Mvvm.ComponentModel;
using gehoortest.application_Repository.Models.TestData_Management;
using gehoortest_application.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml;

namespace gehoortest.application_User.Interface;

public class MainWindowViewModel : ObservableObject 
{
    
    public MainWindowViewModel(Repository context)
    {
        // TargetAudience = context.GetDataFromTable<Target_Audience>();
        // TargetAudience = context.GetDataFromTable<Target_Audience>(entity => entity.From == 19);
        TestCommand = new TestCommand();
    }
    public ICommand TestCommand { get; }
    
    public ObservableCollection<Target_Audience>? TargetAudience { get; }
}
