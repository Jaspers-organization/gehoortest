using BusinessLogic.IModels;
using BusinessLogic.Services;
using DataAccess.Entity.TestData_Management;
using System;
using System.Collections.Generic;
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
    private readonly TestManagementViewModel testManagementViewModel;
    private readonly ITextQuestion textQuestion;
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
    private string this[string columnName]
    {
        get
        {
            string? validationMessage = null;

            switch (columnName)
            {
                case "TestQuestion":
                    validationMessage = ValidateTestQuestion();
                    break;
                case "MultipleChoice":
                case "HasInputField":
                    validationMessage = ValidateQuestionType();
                    break;
                default:
                    break;
            }
            return validationMessage ?? string.Empty;
        }
    }
    private string ValidateTestQuestion()
    {
        if (TestService.IsEmptyString(TestQuestion))
            return ErrorStore.ErrorTestQuestion;
        else if (TestService.ContatinsInvalidCharacters(TestQuestion))
            return ErrorStore.ErrorIllegalCharacters;
        return string.Empty;
    }
    private string ValidateQuestionType()
    {
        if (!HasInputField && !MultipleChoice)
            return ErrorStore.ErrorQuestionAnwserType;
        if (MultipleChoice && Options.Count < 2)
            return ErrorStore.ErrorMultipleChoiceOptions;
        return string.Empty;
    }
    private bool CheckValidityInput()
    {
        string testQuestionValidation = this["TestQuestion"];
        string anwserTypeValidation = this["MultipleChoice"];

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

    public TextQuestionModalViewModel(NavigationStore navigationStore, ITextQuestion textQuestion, bool newQuestion, TestManagementViewModel testManagementViewModel)
    {
        this.navigationStore = navigationStore;
        this.textQuestion = textQuestion;
        this.testManagementViewModel = testManagementViewModel;
        this.newQuestion = newQuestion;
        MultipleChoice = textQuestion.IsMultiSelect;
        HasInputField = textQuestion.HasInputField;
        TestQuestion = textQuestion.Question;
        Options = new ObservableCollection<string>(textQuestion.Options ?? new List<string>());
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
            if (!TestService.IsEmptyString(value))
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
            if (!TestService.IsEmptyString(value) && Options.Contains(value))
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

            ITextQuestion question = new TextQuestion
            {
                Id = textQuestion.Id,
                HasInputField = HasInputField,
                IsMultiSelect = MultipleChoice,
                Question = TestQuestion,
                Options = Options.ToList(),
                QuestionNumber = textQuestion.QuestionNumber
            };

            if (newQuestion)
                testManagementViewModel.AddNewTextQuestion(question);
            else
                testManagementViewModel.UpdateTextQuestion(question);

            CloseModal();
        }
        catch(Exception ex)
        {
            OpenErrorModal("Er is wat fout gegaan bij het opslaan van de vraag.");
        }
        
    }
    
    private void CloseModal()
    {
        navigationStore.CloseModal(testManagementViewModel);
    }
}
