using DataAccess.MockData;
using BusinessLogic.IModels;
using BusinessLogic.Projections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UserInterface.Stores;
using System.Windows.Input;
using UserInterface.Commands.TestManagementCommands;
using BusinessLogic.Services;
using System.Windows.Documents;
using System.Windows;

namespace UserInterface.ViewModels;

internal class TestOverviewViewModel : ViewModelBase
{
    private readonly NavigationStore? _navigationStore;
    private readonly TargetAudienceRepository _targetAudienceRepository;
    private readonly TestRepository _testRepository;
    private readonly TestService _testSerivce;
    private readonly TargetAudienceService _targetAudienceSerivce;

    public ICommand OpenTestCommand { get; }
    public ICommand NewTestCommand { get; }
    public ICommand GetTestsCommand { get; }

    #region propertys
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
        set { GetTests(value); }
    }

    
    private ObservableCollection<TestProjection>? _testCollection;

    public ObservableCollection<TestProjection>? TestCollection
    {
        get { return _testCollection; }
        set { _testCollection = value; OnPropertyChanged(nameof(TestCollection)); }
    }
    #endregion

    public TestOverviewViewModel(NavigationStore navigationStore)
    {
        _navigationStore = navigationStore;

        //commands
        OpenTestCommand = new OpenTestCommand(OpenTest);
        GetTestsCommand = new GetTestsCommand(GetTests);

        //repositories
        _targetAudienceRepository = new TargetAudienceRepository();
        _testRepository = new TestRepository();

        //services
        _testSerivce = new TestService(_testRepository);
        _targetAudienceSerivce = new TargetAudienceService(_targetAudienceRepository);

        List<ITargetAudience> targetAudiences = _targetAudienceSerivce.GetAllAudiences();
        AudiencesList = targetAudiences;
        Audience = targetAudiences.FirstOrDefault();
    }

    public void GetTests(int id)
    {
        TestCollection = this._testRepository.GetTestsProjectionForAudience(id);

    }
    public void OpenTest(int id)
    {
        ITest test = _testRepository.GetTest(id);
        _navigationStore!.CurrentViewModel = new TestManagementViewModel(_navigationStore, test);
    }
    public void NewTest()
    {
        _navigationStore!.CurrentViewModel = new TestManagementViewModel(_navigationStore);
    }
}
