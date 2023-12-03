using BusinessLogic.IModels;
using BusinessLogic.Services;
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
    private ErrorModalViewModal errorModalViewModal { get; set; }

    private readonly IToneAudiometryQuestion toneAudiometryQuestion;
    private readonly bool newQuestion;
    #endregion

    #region Commands
    public ICommand CloseModalCommand => new Command(CloseModal);
    public ICommand SaveQuestionCommand => new Command(SaveQuestion);
    #endregion

    #region Propertys
    private string _frequencyString;
    public string FrequencyString
    {
        get { return _frequencyString; }
        set
        {
            _frequencyString = value;
            OnPropertyChanged(nameof(FrequencyString));
        }
    }
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

    private string _startingDecibelsString;
    public string StartingDecibelsString
    {
        get { return _startingDecibelsString; }
        set
        {
            _startingDecibelsString = value;
            OnPropertyChanged(nameof(StartingDecibelsString));
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

    #region Errors

    public string this[string columnName]
    {
        get
        {
            string? validationMessage = null;

            switch (columnName)
            {
                case "Frequency":
                    validationMessage = ValidateFrequency();
                    break;
                case "StartingDecibelsString":
                    validationMessage = ValidateDecibels();
                    break;
                default:
                    break;
            }
            return validationMessage ?? string.Empty;
        }
    }

    private bool CheckValidityInput()
    {
        string testNameValidation = this["Frequency"];
        string audienceValidation = this["StartingDecibelsString"];

        if (!string.IsNullOrEmpty(testNameValidation))
        {
            OpenErrorModal(testNameValidation);
            return false;
        }

        if (!string.IsNullOrEmpty(audienceValidation))
        {
            OpenErrorModal(audienceValidation);
            return false;
        }
        return true;
    }
    private string ValidateFrequency()
    {
        if (int.TryParse(FrequencyString, out int Frequency))
        {
            if (!TestService.IsValidHz(Frequency))
            {
                return ErrorStore.ErrorFrequencyLimit;
            }
            else
            {
                this.Frequency = Frequency;
            }
        }
        else if (TestService.IsEmptyString(FrequencyString))
        {
            return ErrorStore.ErrorEmpty;
        }
        else
        {
            return ErrorStore.ErrorNotValidInteger;
        }
        return string.Empty;
    }

    private string ValidateDecibels()
    {
        if (int.TryParse(StartingDecibelsString, out int StartingDecibels))
        {
            if (!TestService.IsValidDecibel(StartingDecibels))
            {
                return ErrorStore.ErrorStartingDecibels;
            }
            else
            {
                this.StartingDecibels = StartingDecibels;
            }
        }
        else if (TestService.IsEmptyString(StartingDecibelsString))
        {
            return ErrorStore.ErrorEmpty;
        }
        else
        {
            return ErrorStore.ErrorNotValidInteger;
        }
        return string.Empty;
    }

    #endregion

    public AudioQuestionModalViewModel(NavigationStore navigationStore, IToneAudiometryQuestion toneAudiometryQuestion, bool newQuestion, TestManagementViewModel testManagementViewModel)
    {
        this.navigationStore = navigationStore;
        this.testManagementViewModel = testManagementViewModel;
        this.toneAudiometryQuestion = toneAudiometryQuestion;
        this.newQuestion = newQuestion;
        StartingDecibelsString = toneAudiometryQuestion.StartingDecibels.ToString();
        FrequencyString = toneAudiometryQuestion.Frequency.ToString();
    }
    public void OpenErrorModal(string text)
    {
        errorModalViewModal = new ErrorModalViewModal(navigationStore, text);
        navigationStore.OpenModal(errorModalViewModal);
    }
    private void SaveQuestion()
    {
        if (!CheckValidityInput())
            return;

        IToneAudiometryQuestion question = new ToneAudiometryQuestion { 
            Id = toneAudiometryQuestion.Id, 
            StartingDecibels = StartingDecibels, 
            Frequency = Frequency, 
            QuestionNumber = toneAudiometryQuestion.QuestionNumber 
        };

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