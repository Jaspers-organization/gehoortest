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
    private readonly NavigationStore _navigationStore;
    private readonly TestRepository _testRepository;
    private readonly TargetAudienceRepository _targetAudienceRepository;
    private readonly TestService _testSerivce;
    private readonly TargetAudienceService _targetAudienceSerivce;
    private readonly TestOverviewViewModel _testOverviewViewModel;
    private readonly bool newTest;
    #endregion

    #region Commands
    public ICommand SaveTestCommand { get; }
    public ICommand DeleteTextQuestionCommand { get; }
    public ICommand DeleteAudioQuestionCommand { get; }

    public ICommand OpenTextModalCommand { get; }
    public ICommand OpenNewTextModalCommand { get; }

    public ICommand OpenAudioModalCommand { get; }
    public ICommand OpenNewAudioModalCommand { get; }
    public ICommand BackToTestOverviewCommand { get; }

    #endregion

    #region Propertys
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
    //todo make it so that the right targetaudicence gets set
    private int _selected;
    public int Selected
    {
        get { return _selected; }
        set { SetTargetAudience(value); }
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
        set { _testName = value; OnPropertyChanged(nameof(TestName)); test.Title = value; }
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
                    if (string.IsNullOrEmpty(TestName))
                        validationMessage = ErrorMessageStore.ErrorTestName;
                    else if(TestName.Contains(ErrorMessageStore.IllegalCharacters))
                        validationMessage = ErrorMessageStore.ErrorIllegalCharacters;
                    break;
                case "Audience":
                    if (Audience == null || string.IsNullOrEmpty(Audience.Label))
                        validationMessage = ErrorMessageStore.ErrorAudience;
                    break;
                default:
                    break;
            }

            return validationMessage ?? string.Empty;
        }
    }
    #endregion

    public bool IsConfirmed { get; set; }
    public ITest test { get; set; }
    private ITargetAudience tempTargetAudience;

    private ErrorModalViewModal _errorModalViewModal { get; set; }
    private ConfirmationModalViewModel _confirmationModalViewModel { get; set; }

    public TestManagementViewModel(NavigationStore navigationStore, TestOverviewViewModel testOverviewViewModel,ITargetAudience targetAudience, ITest test = null)
    {
        //Dependencies initialization
        _testRepository = new TestRepository();
        _targetAudienceRepository = new TargetAudienceRepository();
        _testSerivce = new TestService(_testRepository);
        _targetAudienceSerivce = new TargetAudienceService(_targetAudienceRepository);
        _testOverviewViewModel = testOverviewViewModel;
        _navigationStore = navigationStore;

        tempTargetAudience = targetAudience;
        //set values
        List<ITargetAudience> targetAudiences = _targetAudienceSerivce.GetAllAudiences();
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

        // Commands initialization
        SaveTestCommand = new Command(SaveTest);
        DeleteTextQuestionCommand = new Command(DeleteTextQuestion);
        DeleteAudioQuestionCommand = new Command(DeleteAudioQuestion);

        OpenTextModalCommand = new Command(OpenTextModal);
        OpenNewTextModalCommand = new Command(OpenNewTextModal);

        OpenAudioModalCommand = new Command(OpenAudioModal);
        OpenNewAudioModalCommand = new Command(OpenNewAudioModal);

        BackToTestOverviewCommand =new Command(BackToTestOverview);
    }
    private void BackToTestOverview()
    {
        Action backAction = () =>
        {
            _navigationStore!.CurrentViewModel = new TestOverviewViewModel(_navigationStore, tempTargetAudience);
        };

        OpenConfirmationModal(CreateAction(backAction), "Weet je zeker dat je terug wilt gaan? Alle wijzigingen worden ongedaan gemaakt.");
    }
    private void OpenTextModal(int questionNumber)
    {
        ITextQuestion textQuestion = test.TextQuestions.First(q => q.QuestionNumber == questionNumber);
        _navigationStore.OpenModal(new TextQuestionModalViewModel(_navigationStore, textQuestion, false, this));
    }
    private void OpenNewTextModal()
    {
        ITextQuestion textQuestion = CreateTextQuestion();
        textQuestion.QuestionNumber = _testSerivce.GetNewHighestQuestionNumber(test,textQuestion);
        _navigationStore.OpenModal(new TextQuestionModalViewModel(_navigationStore, textQuestion, true, this));
    }
    private void OpenAudioModal(int questionNumber)
    {
        IToneAudiometryQuestion audioQuestion = test.ToneAudiometryQuestions.First(q => q.QuestionNumber == questionNumber);
        _navigationStore.OpenModal(new AudioQuestionModalViewModel(_navigationStore, audioQuestion, false, this));
    }
    private void OpenNewAudioModal()
    {
        IToneAudiometryQuestion audioQuestion = CreateToneAudiometryQuestion();
        audioQuestion.QuestionNumber = _testSerivce.GetNewHighestQuestionNumber(test,audioQuestion);
        _navigationStore.OpenModal(new AudioQuestionModalViewModel(_navigationStore, audioQuestion, true, this));
    }
    //todo FIX THIS
    private void SetTargetAudience(int id)
    {
       // _audience = AudiencesList.FirstOrDefault(t => t.Id == id);
    }
    private void SetTestValues(ITest test)
    {
        this.test = test;
        AudioQuestions = new ObservableCollection<IToneAudiometryQuestion>(test.ToneAudiometryQuestions);
        TextQuestions = new ObservableCollection<ITextQuestion>(test.TextQuestions);
        Audience = AudiencesList.FirstOrDefault(t => t.Id == test.TargetAudience.Id);
        TestName = test.Title;
        Status = test.Active ? "Actief" : "Inactief";
    }
    public void CreateTest()
    {
        test = _testSerivce.CreateTest();
        test.TextQuestions = new List<ITextQuestion>();
        test.ToneAudiometryQuestions = new List<IToneAudiometryQuestion>();
    }

    public void DeleteTest(ITest test)
    {

    }
    public void OpenConfirmationModal(Action action, string text)
    {
        _confirmationModalViewModel = new ConfirmationModalViewModel(_navigationStore, text, this, action);
        _navigationStore.OpenModal(_confirmationModalViewModel);
    }
    public void OpenErrorModal(string text)
    {
        _errorModalViewModal = new ErrorModalViewModal(_navigationStore, text);
        _navigationStore.OpenModal(_errorModalViewModal);
    }
    private void DeleteTextQuestion(int questionNumber)
    {
        Action deleteAction = () =>
        {
            test.TextQuestions = _testSerivce.DeleteQuestion(test.TextQuestions, questionNumber);
            UpdateTextQuestionListView();
        };

        OpenConfirmationModal(CreateAction(deleteAction), "Weet je zeker dat je deze vraag wilt verwijderen?");
    }
    private void UpdateTextQuestionListView()
    {
        TextQuestions = new ObservableCollection<ITextQuestion>(test.TextQuestions);
    }
    private void UpdateAudioQuestionListView()
    {
        AudioQuestions = new ObservableCollection<IToneAudiometryQuestion>(test.ToneAudiometryQuestions);
    }
    private void DeleteAudioQuestion(int questionNumber)
    {
        Action deleteAction = () =>
        {
            test.ToneAudiometryQuestions = _testSerivce.DeleteQuestion(test.ToneAudiometryQuestions, questionNumber);
            UpdateAudioQuestionListView();
        };

        OpenConfirmationModal(CreateAction(deleteAction), "Weet je zeker dat je deze vraag wilt verwijderen?");
    }

    public Action CreateAction(Action action)
    {
        return () =>
        {
            if (!IsConfirmed) return;
            action?.Invoke();
        };
    }

    public ITextQuestion CreateTextQuestion()
    {
        ITextQuestion textQuestion = new TextQuestion
        {
            Options = new List<string>()
        };
        return textQuestion;
    }

    public IToneAudiometryQuestion CreateToneAudiometryQuestion()
    {
        return new ToneAudiometryQuestion();
    }

    public void AddNewToneAudiometryQuestion(IToneAudiometryQuestion question)
    {
        test.ToneAudiometryQuestions.Add(question);
        UpdateAudioQuestionListView();
    }
    public void AddNewTextQuestion(ITextQuestion question)
    {
        test.TextQuestions.Add(question);
        UpdateTextQuestionListView();
    }
    public void UpdateToneAudiometryQuestion(IToneAudiometryQuestion question)
    {
        test.ToneAudiometryQuestions = _testSerivce.UpdateQuestion(test.ToneAudiometryQuestions, question.QuestionNumber, question);
        UpdateAudioQuestionListView();
    }
    public void UpdateTextQuestion(ITextQuestion question)
    {
        test.TextQuestions = _testSerivce.UpdateQuestion(test.TextQuestions, question.QuestionNumber, question);
        UpdateTextQuestionListView();
    }
    
    private void SaveTest()
    {
        string testNameValidation = this["TestName"];
        string audienceValidation = this["Audience"];

        if (!string.IsNullOrEmpty(testNameValidation))
        {
            OpenErrorModal(testNameValidation);
            return;
        }

        if (!string.IsNullOrEmpty(audienceValidation))
        {
            OpenErrorModal(audienceValidation);
            return;
        }
        Action SaveAction = () =>
        {
            if (newTest)
                _testSerivce.SaveTest(test);
            else
                _testSerivce.UpdateTest(test);

            _navigationStore!.CurrentViewModel = _testOverviewViewModel;
        };
        OpenConfirmationModal(CreateAction(SaveAction), "Weet je zeker dat je deze test wilt opslaan?");
    }

    public void SetConfirmed(bool value)
    {
        IsConfirmed = value;
    }
}
