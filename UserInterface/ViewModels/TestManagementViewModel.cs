
using BusinessLogic.IModels;
using BusinessLogic.Services;
using DataAccess.Entity.TestData_Management;
using DataAccess.MockData;
using DataAccess.Models.TestData_Management;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using UserInterface.Commands;
using UserInterface.Stores;
using UserInterface.ViewModels.Modals;
using UserInterface.Views;

namespace UserInterface.ViewModels;

internal class TestManagementViewModel : ViewModelBase
{
    private readonly NavigationStore _navigationStore;
    private readonly TestRepository _testRepository;
    private readonly TargetAudienceRepository _targetAudienceRepository;
    private readonly TestService _testSerivce;
    private readonly TargetAudienceService _targetAudienceSerivce;

    private readonly TestOverviewViewModel _testOverviewViewModel;
    private readonly bool newTest;
    public ITest test { get; set; }
    public ICommand SaveTestCommand { get; }
    public ICommand DeleteTestCommand { get; }
    public ICommand OpenTextModalCommand { get; }
    public ICommand OpenNewTextModalCommand { get; }

    public ICommand OpenAudioModalCommand { get; }
    public ICommand OpenNewAudioModalCommand { get; }

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

    private string? _testName;
    public string? TestName
    {
        get { return _testName; }
        set { _testName = value; OnPropertyChanged(nameof(TestName)); }
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

    public TestManagementViewModel(NavigationStore navigationStore, TestOverviewViewModel testOverviewViewModel, ITest test = null)
    {
        _navigationStore = navigationStore;

        //_dialogService = new DialogService();
        _testRepository = new TestRepository();
        _targetAudienceRepository = new TargetAudienceRepository();
        //services
        _testSerivce = new TestService(_testRepository);
        _targetAudienceSerivce = new TargetAudienceService(_targetAudienceRepository);
        _testOverviewViewModel = testOverviewViewModel;
        if (test != null)
        {
            SetTestValues(test);
        }
        else
        {
            newTest = true;
            CreateTest();
        }

        // Commands initialization
        SaveTestCommand = new Command(SaveTest);
        DeleteTestCommand = new Command(DeleteTest);

        OpenTextModalCommand = new Command(OpenTextModal);
        OpenNewTextModalCommand = new Command(OpenNewTextModal);

        OpenAudioModalCommand = new Command(OpenAudioModal);
        OpenNewAudioModalCommand = new Command(OpenNewAudioModal);
    }


    public void OpenTextModal(int questionNumber)
    {
        ITextQuestion textQuestion = test.TextQuestions.First(q => q.QuestionNumber == questionNumber);
        _navigationStore.OpenModal(new TextQuestionModalViewModel(_navigationStore, textQuestion, false, this));
    }
    public void OpenNewTextModal()
    {
        ITextQuestion textQuestion = CreateTextQuestion();
        textQuestion.QuestionNumber = GetNewHighestQuestionNumber(textQuestion);
        _navigationStore.OpenModal(new TextQuestionModalViewModel(_navigationStore, textQuestion, true, this));
    }
    public void OpenAudioModal(int questionNumber)
    {
        IToneAudiometryQuestion audioQuestion = test.ToneAudiometryQuestions.First(q => q.QuestionNumber == questionNumber);
        _navigationStore.OpenModal(new AudioQuestionModalViewModel(_navigationStore, audioQuestion, false, this));
    }
    public void OpenNewAudioModal()
    {
        IToneAudiometryQuestion audioQuestion = CreateToneAudiometryQuestion();
        audioQuestion.QuestionNumber = GetNewHighestQuestionNumber(audioQuestion);
        _navigationStore.OpenModal(new AudioQuestionModalViewModel(_navigationStore, audioQuestion, true, this));
    }
    private void SetTargetAudience(int id)
    {
        //_audience = _audiencesList.FirstOrDefault(t => t.Id == id);
    }
    private void SetTestValues(ITest test)
    {
        this.test = test;
        AudioQuestions = new ObservableCollection<IToneAudiometryQuestion>(test.ToneAudiometryQuestions);
        TextQuestions = new ObservableCollection<ITextQuestion>(test.TextQuestions);

        List<ITargetAudience> targetAudiences = _targetAudienceSerivce.GetAllAudiences();
        AudiencesList = targetAudiences;
        Audience = targetAudiences.FirstOrDefault(t => t.Id == test.TargetAudience.Id);
        TestName = test.Title;
    }
    public void CreateTest()
    {
        this.test = new Test();
    }

    public void DeleteTest(ITest test)
    {

    }
    public ITextQuestion CreateTextQuestion()
    {
        ITextQuestion textQuestion = new TextQuestion();
        textQuestion.Options = new List<string>();
        return textQuestion;
    }

    public IToneAudiometryQuestion CreateToneAudiometryQuestion()
    {
        return new ToneAudiometryQuestion();
    }
    public void AddNewToneAudiometryQuestion(IToneAudiometryQuestion question)
    {
        test.ToneAudiometryQuestions.Add(question);
        AudioQuestions = new ObservableCollection<IToneAudiometryQuestion>(test.ToneAudiometryQuestions);
    }
    public void AddNewTextQuestion(ITextQuestion question)
    {
        test.TextQuestions.Add(question);
        TextQuestions = new ObservableCollection<ITextQuestion>(test.TextQuestions);
    }
    public void UpdateToneAudiometryQuestion(IToneAudiometryQuestion question)
    {
        int index = test.ToneAudiometryQuestions.FindIndex(q => q.QuestionNumber == question.QuestionNumber);
        test.ToneAudiometryQuestions[index] = question;
        AudioQuestions = new ObservableCollection<IToneAudiometryQuestion>(test.ToneAudiometryQuestions);
    }
    public void UpdateTextQuestion(ITextQuestion question)
    {
        int index = test.TextQuestions.FindIndex(q => q.QuestionNumber == question.QuestionNumber);
        test.TextQuestions[index] = question;
        TextQuestions = new ObservableCollection<ITextQuestion>(test.TextQuestions);
    }
    public void SaveTest()
    {
        if (newTest)
            _testSerivce.CreateTest(test);
        else
            _testSerivce.UpdateTest(test);

        _navigationStore!.CurrentViewModel = _testOverviewViewModel;

    }
    public int GetNewHighestQuestionNumber(IQuestion questionType)
    {
        switch (questionType)
        {
            case IToneAudiometryQuestion audioQuestion:
                return test.ToneAudiometryQuestions.Max(q => q.QuestionNumber) + 1;
            case ITextQuestion textQuestion:
                return test.TextQuestions.Max(q => q.QuestionNumber) + 1;
            default:
                return 0;
        }
    }
}
