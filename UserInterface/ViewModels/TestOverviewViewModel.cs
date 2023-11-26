using DataAccess.MockData;
using BusinessLogic.IModels;
using BusinessLogic.Projections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UserInterface.Stores;
using System.Windows.Input;
using UserInterface.Commands.TestManagementCommands;

namespace UserInterface.ViewModels;

internal class TestOverviewViewModel: ViewModelBase
{
    private readonly NavigationStore? _navigationStore;
    private readonly TargetAudienceRepository _targetAudienceRepository;
    private readonly TestRepository _testRepository;
    public ICommand OpenTestCommand { get; }
    public ICommand NewTestCommand { get; }

    #region propertys
    private List<string>? _audiencesList;
    public List<string>? AudiencesList
    {
        get { return _audiencesList; }
        set { _audiencesList = value; OnPropertyChanged(nameof(AudiencesList)); }
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

        OpenTestCommand = new OpenTestCommand(OpenTest);

        _targetAudienceRepository = new TargetAudienceRepository();

        ITargetAudience targetAudience = _targetAudienceRepository.GetAllAudiences().FirstOrDefault();
        _testRepository = new TestRepository(targetAudience);

        AudiencesList = _targetAudienceRepository.GetAllAudiences().Select(audience => audience.Label).ToList();
        TestCollection = _testRepository.GetTestsProjectionForAudience(targetAudience.Id);
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
