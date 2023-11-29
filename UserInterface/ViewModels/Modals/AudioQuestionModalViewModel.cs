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

internal class AudioQuestionModalViewModel : ViewModelBase
{
    private readonly NavigationStore navigationStore;

    public ICommand CloseModalCommand => new ObjectCommand(CloseModal);
    private readonly IToneAudiometryQuestion toneAudiometryQuestion;
    public AudioQuestionModalViewModel(NavigationStore navigationStore, IToneAudiometryQuestion toneAudiometryQuestion)
    {
        this.navigationStore = navigationStore;
        this.toneAudiometryQuestion = toneAudiometryQuestion;
    }

    private void CloseModal(object obj)
    {
        navigationStore.CloseModal();
    }
}