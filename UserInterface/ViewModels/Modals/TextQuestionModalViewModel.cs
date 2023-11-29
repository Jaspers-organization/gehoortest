using BusinessLogic.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UserInterface.Commands.TestManagementCommands;
using UserInterface.Stores;

namespace UserInterface.ViewModels.Modals;

internal class TextQuestionModalViewModel : ViewModelBase
{
    private readonly NavigationStore navigationStore;

    public ICommand CloseModalCommand => new CloseModalCommand(CloseModal);
    private readonly ITextQuestion textQuestion;
    private readonly TestManagementViewModel testManagementViewModel;

    public TextQuestionModalViewModel(NavigationStore navigationStore, ITextQuestion textQuestion, TestManagementViewModel testManagementViewModel)
    {
        this.navigationStore = navigationStore;
        this.textQuestion = textQuestion;
        this.testManagementViewModel = testManagementViewModel;
        
    }
    
    private void CloseModal(object obj)
    {
        navigationStore.CloseModal();
    }
}
