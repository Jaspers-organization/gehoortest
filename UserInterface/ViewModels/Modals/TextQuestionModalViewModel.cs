using BusinessLogic.Models;
using BusinessLogic.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using UserInterface.Commands;
using UserInterface.Stores;

namespace UserInterface.ViewModels.Modals;

internal class TextQuestionModalViewModel : ViewModelBase
{
    #region Dependencies
    private readonly NavigationStore navigationStore;
    private readonly TestService testService;
    private readonly TestManagementViewModel testManagementViewModel;
    private readonly TextQuestion textQuestion;
    private readonly bool newQuestion;
    #endregion

    #region Commands
    public ICommand AddOptionCommand => new Command(AddOption);
    public ICommand RemoveOptionCommand => new Command(RemoveOption);
    public ICommand CloseModalCommand => new Command(CloseModal);
    public ICommand SaveQuestionCommand => new Command(SaveQuestion);
    #endregion

    #region Properties
    private string _optionText;
    public string OptionText
    {
        get { return _optionText; }
        set
        {
            _optionText = value;
            OnPropertyChanged(nameof(OptionText));
        }
    }

    private string _testQuestion;
    public string TestQuestion
    {
        get { return _testQuestion; }
        set
        {
            _testQuestion = value;
            OnPropertyChanged(nameof(TestQuestion));
        }
    }

    private ObservableCollection<string> _options;
    public ObservableCollection<string> Options
    {
        get { return _options; }
        set
        {
            _options = value;
            OnPropertyChanged(nameof(Options));
        }
    }

    private bool _multipleChoice;
    public bool MultipleChoice
    {
        get { return _multipleChoice; }
        set
        {
            _multipleChoice = value;
            OnPropertyChanged(nameof(MultipleChoice));
        }
    }
    private bool _hasInputField;
    public bool HasInputField
    {
        get { return _hasInputField; }
        set
        {
            _hasInputField = value;
            OnPropertyChanged(nameof(HasInputField));
        }
    }
    #endregion

    #region Errors
    private bool CheckValidityInput()
    {
        string testQuestionValidation = ErrorService.ValidateInput("TestQuestion", TestQuestion);
        string anwserTypeValidation = ErrorService.ValidateInput("MultipleChoice", HasInputField, MultipleChoice, Options.ToList());
        if (!string.IsNullOrEmpty(testQuestionValidation))
        {
            OpenErrorModal(testQuestionValidation);
            return false;
        }

        if (!string.IsNullOrEmpty(anwserTypeValidation))
        {
            OpenErrorModal(anwserTypeValidation);
            return false;
        }
        return true;
    }
    #endregion
    private ErrorModalViewModal errorModalViewModal { get; set; }

    public TextQuestionModalViewModel(NavigationStore navigationStore, TextQuestion textQuestion, bool newQuestion, TestManagementViewModel testManagementViewModel, TestService testService)
    {
        this.navigationStore = navigationStore;
        this.textQuestion = textQuestion;
        this.testManagementViewModel = testManagementViewModel;
        this.newQuestion = newQuestion;
        this.testService = testService;
        SetValues();
    }
    private void SetValues()
    {
        MultipleChoice = textQuestion.IsMultiSelect;
        HasInputField = textQuestion.HasInputField;
        TestQuestion = textQuestion.Question;
        Options = new ObservableCollection<string>(testService.ConvertQuestionOptionsToStrings(textQuestion.Options!));
    }

    private void OpenErrorModal(string text)
    {
        errorModalViewModal = new ErrorModalViewModal(navigationStore, text);
        navigationStore.OpenModal(errorModalViewModal);
    }
    private void AddOption(string value)
    {
        try
        {
            if (!ErrorService.IsEmptyString(value))
            {
                Options.Add(value);
                OnPropertyChanged(nameof(Options));
            }
            else
            {
                OpenErrorModal("De optie mag niet leeg zijn.");
            }
        }
        catch (Exception ex)
        {
            OpenErrorModal($"Er is wat fout gegaan tijdens het toevoegen van de optie. {ex.Message}");

        }
    }

    private void RemoveOption(string value)
    {
        try
        {
            if (!ErrorService.IsEmptyString(value) && Options.Contains(value))
            {
                Options.Remove(value);
                OnPropertyChanged(nameof(Options));
            }
            else
            {
                OpenErrorModal("Er is wat fout gegaan bij het verwijderen van de optie.");
            }
        }
        catch (Exception ex)
        {
            OpenErrorModal($"Er is wat fout gegaan bij het verwijderen van de optie");
        }
    }

    private void SaveQuestion()
    {
        try
        {
            if (!CheckValidityInput())
                return;

            TextQuestion question = new TextQuestion
            {
                Id = textQuestion.Id,
                HasInputField = HasInputField,
                IsMultiSelect = MultipleChoice,
                Question = TestQuestion,
                Options = testService.ConvertStringsToQuestionOptions(Options.ToList(), textQuestion.Id),
                QuestionNumber = textQuestion.QuestionNumber
            };

            if (newQuestion)
                testManagementViewModel.AddNewTextQuestion(question);
            else
                testManagementViewModel.UpdateTextQuestion(question);

            CloseModal();
        }
        catch (Exception ex)
        {
            OpenErrorModal("Er is wat fout gegaan bij het opslaan van de vraag.");
        }

    }

    private void CloseModal()
    {
        navigationStore.CloseModal(testManagementViewModel);
    }
}
