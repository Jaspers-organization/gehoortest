using BusinessLogic.IModels;
using DataAccess.Entity.TestData_Management;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Documents;
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

    #region Propertys
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
                    if (string.IsNullOrEmpty(TestQuestion))
                        validationMessage = ErrorMessageStore.ErrorTestQuestion;
                    break;
                case "MultipleChoice":
                case "HasInputField":
                    if (!HasInputField && !MultipleChoice)
                        validationMessage = ErrorMessageStore.ErrorQuestionAnwserType;
                    if(MultipleChoice && Options.Count < 2)
                        validationMessage = ErrorMessageStore.ErrorMultipleChoiceOptions;
                    break;
                default:
                    break;
            }

            return validationMessage ?? string.Empty;
        }
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
    public void OpenErrorModal(string text)
    {
        errorModalViewModal = new ErrorModalViewModal(navigationStore, text);
        navigationStore.OpenModal(errorModalViewModal);
    }
    public void AddOption(string value)
    {
        Options.Add(value);
        OnPropertyChanged(nameof(Options));
    }
    public void RemoveOption(string value)
    {
        Options.Remove(value);
        OnPropertyChanged(nameof(Options));
    }
    public void SaveQuestion()
    {
        string testQuestionValidation = this["TestQuestion"];
        string anwserTypeValidation = this["MultipleChoice"];

        if (!string.IsNullOrEmpty(testQuestionValidation))
        {
            OpenErrorModal(testQuestionValidation);
            return;
        }

        if (!string.IsNullOrEmpty(anwserTypeValidation))
        {
            OpenErrorModal(anwserTypeValidation);
            return;
        }
        ITextQuestion question = new TextQuestion { Id = textQuestion.Id, HasInputField = HasInputField, IsMultiSelect = MultipleChoice, Question = TestQuestion, Options = Options.ToList(), QuestionNumber = textQuestion.QuestionNumber };
        
        if (newQuestion)
            testManagementViewModel.AddNewTextQuestion(question);
        else
            testManagementViewModel.UpdateTextQuestion(question);

        CloseModal();
    }
    
    private void CloseModal()
    {
        navigationStore.CloseModal(testManagementViewModel);
    }
}
