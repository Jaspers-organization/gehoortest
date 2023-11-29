using BusinessLogic.IModels;
using BusinessLogic.Services;
using DataAccess.MockData;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using UserInterface.Commands;
using UserInterface.Commands.TestManagementCommands;
using UserInterface.Stores;
using UserInterface.Views;

namespace UserInterface.ViewModels;

internal class TestManagementViewModel : ViewModelBase
{
    private readonly NavigationStore navigationStore;
    private readonly TestRepository _testRepository;
    private readonly TargetAudienceRepository _targetAudienceRepository;
    private readonly TestService _testSerivce;
    private readonly TargetAudienceService _targetAudienceSerivce;

    public ITest test { get; set; }
    public ICommand SaveTestCommand { get; }
    public ICommand DeleteTestCommand { get; }
    public ICommand OpenModalCommand { get; }

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
    private List<ITextQuestion>? _textQuestions;
    public List<ITextQuestion>? TextQuestions
    {
        get { return _textQuestions; }
        set { _textQuestions = value; OnPropertyChanged(nameof(TextQuestions)); }
    }

    private List<IToneAudiometryQuestion>? _audioQuestions;
    public List<IToneAudiometryQuestion>? AudioQuestions
    {
        get { return _audioQuestions; }
        set { _audioQuestions = value; OnPropertyChanged(nameof(AudioQuestions)); }
    }

    #endregion

    public TestManagementViewModel(NavigationStore navigationStore, ITest test = null)
    {
        this.navigationStore = navigationStore;

        //_dialogService = new DialogService();
        _testRepository = new TestRepository();
        _targetAudienceRepository = new TargetAudienceRepository();
        //services
        _testSerivce = new TestService(_testRepository);
        _targetAudienceSerivce = new TargetAudienceService(_targetAudienceRepository);

        if (test != null)
        {
            this.test = test;
            AudioQuestions = test.ToneAudiometryQuestions.ToList();
            TextQuestions = test.TextQuestions.ToList();

            List<ITargetAudience> targetAudiences = _targetAudienceSerivce.GetAllAudiences();
            AudiencesList = targetAudiences;
            Audience = targetAudiences.FirstOrDefault(t => t.Id == test.TargetAudience.Id);
            TestName = test.Title;
        }

        // Commands initialization
        SaveTestCommand = new SaveTestCommand(SaveTest);
        DeleteTestCommand = new DeleteTestCommand(DeleteTest);
        OpenModalCommand = new OpenModalCommand(OpenModal);
    }


    public void OpenModal(object temp)
    {
        navigationStore.OpenModal(new QuestionModalViewModel(navigationStore));
    }

    private void SetTargetAudience(int id)
    {
        //_audience = _audiencesList.FirstOrDefault(t => t.Id == id);
    }    

    public void CreateTest()
    {

    }
    public void EditTest(ITest test)
    {

    }
    public void DeleteTest(ITest test)
    {

    }
    public void CreateTextQuestion()
    {

    }
    public void UpdateTextQuestion()
    {

    }
    public void CreateToneAudiometryQuestion()
    {

    }
    public void UpdateToneAudiometryQuestion()
    {

    }
    public void SaveTest(ITest test)
    {

    }
}
