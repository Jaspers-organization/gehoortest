using Service.IModels;
using UserInterface.Stores;

namespace UserInterface.ViewModels;

internal class ManageTestViewModel : ViewModelBase
{
    private readonly NavigationStore? _navigationStore;
    public ManageTestViewModel(NavigationStore navigationStore)
    {
        _navigationStore = navigationStore;
    }
    public ITest test { get; set; }

    public void AddQuestion()
    {

    }
    public void EditQuestion(IQuestion question) { 
    
    }
    public void ToggleStatus()
    {
        test.Active = !test.Active;
    }
    public void SaveTest()
    {

    }
}
