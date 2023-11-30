using BusinessLogic.IModels;
using DataAccess.Entity.TestData_Management;
using System.Windows.Input;
using UserInterface.Commands;
using UserInterface.Stores;

namespace UserInterface.ViewModels.Modals;

internal class AudioQuestionModalViewModel : ViewModelBase
{
    #region Dependencies
    private readonly NavigationStore navigationStore;
    private readonly TestManagementViewModel testManagementViewModel;

    private readonly IToneAudiometryQuestion toneAudiometryQuestion;
    private readonly bool newQuestion;
    #endregion

    #region Commands
    public ICommand CloseModalCommand => new Command(CloseModal);
    public ICommand SaveQuestionCommand => new Command(SaveQuestion);
    #endregion

    #region Propertys
    private int _frequency;
    public int Frequency
    {
        get { return _frequency; }
        set
        {
            _frequency = value;
            OnPropertyChanged(nameof(Frequency));
        }
    }
    private int _startingDecibels;
    public int StartingDecibels
    {
        get { return _startingDecibels; }
        set
        {
            _startingDecibels = value;
            OnPropertyChanged(nameof(StartingDecibels));
        }
    }
    #endregion

    public AudioQuestionModalViewModel(NavigationStore navigationStore, IToneAudiometryQuestion toneAudiometryQuestion, bool newQuestion, TestManagementViewModel testManagementViewModel)
    {
        this.navigationStore = navigationStore;
        this.testManagementViewModel = testManagementViewModel;
        this.toneAudiometryQuestion = toneAudiometryQuestion;
        this.newQuestion = newQuestion;
        StartingDecibels = toneAudiometryQuestion.StartingDecibels;
        Frequency = toneAudiometryQuestion.Frequency;
    }
    private void SaveQuestion()
    {
        IToneAudiometryQuestion question = new ToneAudiometryQuestion { Id = toneAudiometryQuestion.Id, StartingDecibels = StartingDecibels, Frequency = Frequency, QuestionNumber = toneAudiometryQuestion.QuestionNumber };

        if (newQuestion)
            testManagementViewModel.AddNewToneAudiometryQuestion(question);
        else
            testManagementViewModel.UpdateToneAudiometryQuestion(question);

        CloseModal();
    }
    private void CloseModal()
    {
        navigationStore.CloseModal();
    }
}