using BusinessLogic.IModels;
using System.Collections.Generic;
using System.Windows.Input;
using UserInterface.Commands;
using UserInterface.Commands.TestManagementCommands;
using UserInterface.Stores;
using UserInterface.Views;

namespace UserInterface.ViewModels;

internal class TestManagementViewModel : ViewModelBase
{
    private readonly NavigationStore? _navigationStore;
    public ITest test { get; set; }
    public ICommand SaveTestCommand { get; }
    public ICommand DeleteTestCommand { get; }

    public TestManagementViewModel(NavigationStore navigationStore, ITest test)
    {
        _navigationStore = navigationStore;
        this.test = test;
        SaveTestCommand = new SaveTestCommand(SaveTest);
        DeleteTestCommand = new DeleteTestCommand(DeleteTest);

    }
    public TestManagementViewModel(NavigationStore navigationStore)
    {
        _navigationStore = navigationStore;
        SaveTestCommand = new SaveTestCommand(SaveTest);
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
    public void SaveTest(ITest test)
    {

    }
}
