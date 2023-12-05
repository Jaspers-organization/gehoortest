using BusinessLogic.IModels;
using BusinessLogic.Services;
using DataAccess.Entity.TestData_Management;
using System;
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
        try
        {
            if (int.TryParse(FrequencyString, out int frequency))
            {
                if (!TestService.IsValidHz(frequency))
                {
                    return ErrorStore.ErrorFrequencyLimit;
                }
                else
                {
                    Frequency = frequency;
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
        }
        catch (Exception ex)
        {
            // Log or handle the exception as needed
            return $"Er is wat misgegaan";
        }

        return string.Empty;
    }

    private string ValidateDecibels()
    {
        try
        {
            if (int.TryParse(StartingDecibelsString, out int startingDecibels))
            {
                if (!TestService.IsValidDecibel(startingDecibels))
                {
                    return ErrorStore.ErrorStartingDecibels;
                }
                else
                {
                    StartingDecibels = startingDecibels;
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
        }
        catch (Exception ex)
        {
            // Log or handle the exception as needed
            return $"Er is wat misgegaan";
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

        // Opens an error modal with the provided error text
    }
    public void OpenErrorModal(string text) => navigationStore.OpenModal(new ErrorModalViewModal(navigationStore, text));


    private void SaveQuestion()
    {
        try
        {
            // Checks the validity of input data before proceeding
            if (!CheckValidityInput())
                return;

            // Creates a new ToneAudiometryQuestion object based on the provided data
            IToneAudiometryQuestion question = new ToneAudiometryQuestion
            {
                Id = toneAudiometryQuestion.Id,
                StartingDecibels = StartingDecibels,
                Frequency = Frequency,
                QuestionNumber = toneAudiometryQuestion.QuestionNumber
            };

            // Adds a new tone audiometry question if it's a new question, otherwise updates the existing one
            if (newQuestion)
                testManagementViewModel.AddNewToneAudiometryQuestion(question);
            else
                testManagementViewModel.UpdateToneAudiometryQuestion(question);

            // Closes the modal after successful question saving or updating
            CloseModal();
        }
        catch (Exception ex)
        {
            // If an exception occurs during the saving process, opens an error modal
            OpenErrorModal("Er is wat foutgegaan bij het opslaan van de vraag");
        }
    }

    private void CloseModal()
    {
        // Closes the current modal
        navigationStore.CloseModal();
    }

}