using BusinessLogic.IModels;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using DataAccess.Entity.TestData_Management;
using DataAccess.MockData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using UserInterface.Commands;
using UserInterface.Stores;
using UserInterface.ViewModels.Modals;

namespace UserInterface.ViewModels;

internal class TestManagementViewModel : ViewModelBase, IConfirmation
{
    #region Dependencies
    private readonly NavigationStore navigationStore;
    private readonly TestRepository testRepository;
    private readonly TargetAudienceRepository targetAudienceRepository;
    private readonly TestService testSerivce;
    private readonly TargetAudienceService targetAudienceSerivce;
    private readonly TestOverviewViewModel testOverviewViewModel;
    private readonly bool newTest;
    #endregion

    #region Commands
    public ICommand SaveTestCommand => new Command(SaveTest);
    public ICommand DeleteTextQuestionCommand => new Command(DeleteTextQuestion);
    public ICommand DeleteAudioQuestionCommand => new Command(DeleteAudioQuestion);

    public ICommand OpenTextModalCommand => new Command(OpenTextModal);
    public ICommand OpenNewTextModalCommand => new Command(OpenNewTextModal);

    public ICommand OpenAudioModalCommand => new Command(OpenAudioModal);
    public ICommand OpenNewAudioModalCommand => new Command(OpenNewAudioModal);
    public ICommand BackToTestOverviewCommand => new Command(BackToTestOverview);

    #endregion

    #region Properties
    private List<ITargetAudience>? _audiencesList;
    public List<ITargetAudience>? AudiencesList
    {
        get { return _audiencesList; }
        set { _audiencesList = value; OnPropertyChanged(nameof(AudiencesList)); }
    }
    private ITargetAudience? _audience;
    public ITargetAudience? Audience
    {
        get { return _audience; }
        set { _audience = value; OnPropertyChanged(nameof(Audience)); }
    }
    private int _selected;
    public int Selected
    {
        get { return _selected; }
        set { _selected = value; OnPropertyChanged(nameof(Selected)); Test.TargetAudience.Id = value; }
    }

    private string? _status;
    public string? Status
    {
        get { return _status; }
        set { _status = value; OnPropertyChanged(nameof(Status)); }
    }

    private string? _testName;
    public string? TestName
    {
        get { return _testName; }
        set { _testName = value; OnPropertyChanged(nameof(TestName)); Test.Title = value; }
    }
    private ObservableCollection<ITextQuestion>? _textQuestions;
    public ObservableCollection<ITextQuestion>? TextQuestions
    {
        get { return _textQuestions; }
        set { _textQuestions = value; OnPropertyChanged(nameof(TextQuestions)); }
    }

    private ObservableCollection<IToneAudiometryQuestion>? _audioQuestions;
    public ObservableCollection<IToneAudiometryQuestion>? AudioQuestions
    {
        get { return _audioQuestions; }
        set { _audioQuestions = value; OnPropertyChanged(nameof(AudioQuestions)); }
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
                case "TestName":
                    validationMessage = ValidateTestName(TestName!);
                    break;
                case "Audience":
                    validationMessage = ValidateAudience(Audience!);
                    break;
                default:
                    break;
            }
            return validationMessage ?? string.Empty;
        }
    }
    private string ValidateTestName(string str)
    {
        if (TestService.IsEmptyString(str!))
            return ErrorStore.ErrorTestName;
        else if (str.Contains(ErrorStore.IllegalCharacters))
            return ErrorStore.ErrorIllegalCharacters;
        return string.Empty;
    }
    private string ValidateAudience(ITargetAudience targetAudience)
    {
        if (Audience == null || TestService.IsEmptyString(Audience.Label))
            return ErrorStore.ErrorAudience;
        return string.Empty;

    }
    private bool CheckValidityInput()
    {
        string testNameValidation = this["TestName"];
        string audienceValidation = this["Audience"];

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
    #endregion

    public bool IsConfirmed { get; set; }
    public ITest Test { get; set; }
    private ErrorModalViewModal errorModalViewModal { get; set; }
    private ConfirmationModalViewModel confirmationModalViewModel { get; set; }
    private ITargetAudience tempTargetAudience;

    public TestManagementViewModel(NavigationStore navigationStore, TestOverviewViewModel testOverviewViewModel,ITargetAudience targetAudience, ITest test = null)
    {
        //Dependencies initialization
        testRepository = new TestRepository();
        targetAudienceRepository = new TargetAudienceRepository();
        testSerivce = new TestService(testRepository);
        targetAudienceSerivce = new TargetAudienceService(targetAudienceRepository);
        this.testOverviewViewModel = testOverviewViewModel;
        this.navigationStore = navigationStore;
        tempTargetAudience = targetAudience;

        //set values
        List<ITargetAudience> targetAudiences = targetAudienceSerivce.GetAllTargetAudiences();
        AudiencesList = targetAudiences;
        if (test != null)
        {
            SetTestValues(test);
        }
        else
        {
            newTest = true;
            Status = "Inactief";
            CreateTest();
        }
    }

    #region Navigation
    private void BackToTestOverview()
    {
        Action backAction = () =>
        {
            navigationStore!.CurrentViewModel = new TestOverviewViewModel(navigationStore, tempTargetAudience);
        };

        OpenConfirmationModal(CreateAction(backAction), "Weet je zeker dat je terug wilt gaan? Alle wijzigingen worden ongedaan gemaakt.");
    }
    #endregion

    #region Initialization
    private void SetTestValues(ITest test)
    {
        this.Test = test;
        AudioQuestions = new ObservableCollection<IToneAudiometryQuestion>(test.ToneAudiometryQuestions);
        TextQuestions = new ObservableCollection<ITextQuestion>(test.TextQuestions);
        Audience = AudiencesList.FirstOrDefault(t => t.Id == test.TargetAudience.Id);
        TestName = test.Title;
        Status = test.Active ? "Actief" : "Inactief";
        Selected = test.TargetAudience.Id;
    }
    private void CreateTest()
    {
        Test = testSerivce.CreateTest();
        Test.TextQuestions = new List<ITextQuestion>();
        Test.ToneAudiometryQuestions = new List<IToneAudiometryQuestion>();
    }
    #endregion

    #region Modals 
    #region Text
    private void OpenNewTextModal()
    {
        ITextQuestion textQuestion = CreateTextQuestion();
        textQuestion.QuestionNumber = testSerivce.GetNewHighestQuestionNumber(Test, textQuestion);
        navigationStore.OpenModal(new TextQuestionModalViewModel(navigationStore, textQuestion, true, this));
    }
    private void OpenTextModal(int questionNumber)
    {
        ITextQuestion textQuestion = Test.TextQuestions.First(q => q.QuestionNumber == questionNumber);
        navigationStore.OpenModal(new TextQuestionModalViewModel(navigationStore, textQuestion, false, this));
    }
    #endregion
    #region Audio
    private void OpenNewAudioModal()
    {
        IToneAudiometryQuestion audioQuestion = CreateToneAudiometryQuestion();
        audioQuestion.QuestionNumber = testSerivce.GetNewHighestQuestionNumber(Test, audioQuestion);
        navigationStore.OpenModal(new AudioQuestionModalViewModel(navigationStore, audioQuestion, true, this));
    }
    private void OpenAudioModal(int questionNumber)
    {
        IToneAudiometryQuestion audioQuestion = Test.ToneAudiometryQuestions.First(q => q.QuestionNumber == questionNumber);
        navigationStore.OpenModal(new AudioQuestionModalViewModel(navigationStore, audioQuestion, false, this));
    }

    #endregion
    public Action CreateAction(Action action)
    {
        return () =>
        {
            if (!IsConfirmed) return;
            action?.Invoke();
        };
    }
    public void OpenConfirmationModal(Action action, string text)
    {
        confirmationModalViewModel = new ConfirmationModalViewModel(navigationStore, text, this, action);
        navigationStore.OpenModal(confirmationModalViewModel);
    }
    private void OpenErrorModal(string text)
    {
        errorModalViewModal = new ErrorModalViewModal(navigationStore, text);
        navigationStore.OpenModal(errorModalViewModal);
    }
    #endregion

    #region Test Modification
    #region Text
    private ITextQuestion CreateTextQuestion()
    {
        ITextQuestion textQuestion = new TextQuestion
        {
            Options = new List<string>()
        };
        return textQuestion;
    }
    public void AddNewTextQuestion(ITextQuestion question)
    {
        Test.TextQuestions.Add(question);
        UpdateTextQuestionListView();
    }
    public void UpdateTextQuestion(ITextQuestion question)
    {
        Test.TextQuestions = testSerivce.UpdateQuestion(Test.TextQuestions, question.QuestionNumber, question);
        UpdateTextQuestionListView();
    }
    private void UpdateTextQuestionListView()
    {
        TextQuestions = new ObservableCollection<ITextQuestion>(Test.TextQuestions);
    }
    private void DeleteTextQuestion(int questionNumber)
    {
        Action deleteAction = () =>
        {
            Test.TextQuestions = testSerivce.DeleteQuestion(Test.TextQuestions, questionNumber);
            UpdateTextQuestionListView();
        };

        OpenConfirmationModal(CreateAction(deleteAction), "Weet je zeker dat je deze vraag wilt verwijderen?");
    }
    #endregion
    #region Audio
    private IToneAudiometryQuestion CreateToneAudiometryQuestion()
    {
        return new ToneAudiometryQuestion();
    }
    public void AddNewToneAudiometryQuestion(IToneAudiometryQuestion question)
    {
        Test.ToneAudiometryQuestions.Add(question);
        UpdateAudioQuestionListView();
    }
    public void UpdateToneAudiometryQuestion(IToneAudiometryQuestion question)
    {
        Test.ToneAudiometryQuestions = testSerivce.UpdateQuestion(Test.ToneAudiometryQuestions, question.QuestionNumber, question);
        UpdateAudioQuestionListView();
    }
    private void UpdateAudioQuestionListView()
    {
        AudioQuestions = new ObservableCollection<IToneAudiometryQuestion>(Test.ToneAudiometryQuestions);
    }
    private void DeleteAudioQuestion(int questionNumber)
    {
        Action deleteAction = () =>
        {
            Test.ToneAudiometryQuestions = testSerivce.DeleteQuestion(Test.ToneAudiometryQuestions, questionNumber);
            UpdateAudioQuestionListView();
        };

        OpenConfirmationModal(CreateAction(deleteAction), "Weet je zeker dat je deze vraag wilt verwijderen?");
    }
    #endregion
    private void SaveTest()
    {
        if (!CheckValidityInput())
            return;

        if (newTest)
            testSerivce.SaveTest(Test);
        else
            testSerivce.UpdateTest(Test);

        navigationStore!.CurrentViewModel = testOverviewViewModel;

    }
    #endregion
}
