using BusinessLogic.IModels;
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
    private readonly NavigationStore? _navigationStore;
    private readonly TestRepository _testRepository;
    private readonly TargetAudienceRepository _targetAudienceRepository;
    private List<ITargetAudience> targetAudiences;
    public ITest test { get; set; }
    public ICommand SaveTestCommand { get; }
    public ICommand DeleteTestCommand { get; }

    
    #region Propertys
    private List<string>? _audiencesList;
    public List<string>? AudiencesList
    {
        get { return _audiencesList; }
        set { _audiencesList = value; OnPropertyChanged(nameof(AudiencesList)); }
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
    public TestManagementViewModel(NavigationStore navigationStore, ITest test)
    {
        //instantiate stuff 
        _navigationStore = navigationStore;
        _testRepository = new TestRepository(test.TargetAudience);
        _targetAudienceRepository = new TargetAudienceRepository();

        this.test = test;
        AudioQuestions = test.ToneAudiometryQuestions.ToList();
        TextQuestions = test.TextQuestions.ToList();

        //get Target audiences
        targetAudiences = _targetAudienceRepository.GetAllAudiences();
        AudiencesList = targetAudiences.Select(audience => audience.Label).ToList();

        //create commands
        SaveTestCommand = new SaveTestCommand(SaveTest);
        DeleteTestCommand = new DeleteTestCommand(DeleteTest);

    }
    public TestManagementViewModel(NavigationStore navigationStore)
    {
        _navigationStore = navigationStore;
        SaveTestCommand = new SaveTestCommand(SaveTest);
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
