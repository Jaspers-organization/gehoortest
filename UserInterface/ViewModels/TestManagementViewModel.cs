using BusinessLogic.IModels;
using System.Collections.Generic;
using System.Windows.Input;
using UserInterface.Commands;
using UserInterface.Stores;
using UserInterface.Views;

namespace UserInterface.ViewModels;

internal class TestManagementViewModel : ViewModelBase
{
    private readonly NavigationStore? _navigationStore;
    public ITest test { get; set; }
    public List<ITest> tests { get; set; }

    public TestManagementViewModel(NavigationStore navigationStore)
    {
        _navigationStore = navigationStore;
    }

    public void CreateTest()
    {

    }
    public void EditTest(ITest test)
    {

    }
    public void DeleteTest(ITest test)
    {

    }
    public void CreateTextQuestion()
    {

    }
    public void UpdateTextQuestion()
    {

    }
    public void CreateToneAudiometryQuestion()
    {

    }
    public void UpdateToneAudiometryQuestion()
    {

    }
    public void ToggleStatus()
    {

    }
    public void SaveTest()
    {

    }


    //public ICommand StartTestCommand => new CustomCommands();





}
