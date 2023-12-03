using DataAccess.MockData;
using BusinessLogic.IModels;
using BusinessLogic.Projections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UserInterface.Stores;
using System.Windows.Input;
using BusinessLogic.Services;
using UserInterface.Commands;
using BusinessLogic.Interfaces;
using System;
using UserInterface.ViewModels.Modals;

namespace UserInterface.ViewModels;

internal class TestOverviewViewModel : ViewModelBase, IConfirmation
{
    #region Dependencies
    private readonly NavigationStore? navigationStore;
    private readonly TargetAudienceRepository targetAudienceRepository;
    private readonly TestRepository testRepository;
    private readonly TestService testSerivce;
    private readonly TargetAudienceService _targetAudienceSerivce;
    #endregion

    #region Commands
    public ICommand OpenTestCommand { get; }
    public ICommand NewTestCommand { get; }
    public ICommand GetTestsCommand { get; }
    public ICommand DeleteTestCommand { get; }
    public ICommand BackToMainMenuCommand { get; }

    #endregion

    #region properties
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
        set
        {
            _audience = value;
            if (value != null)
            {
                // Set the Selected property to the Id if Audience is not null
                Selected = value.Id;
            }
            else
            {
                // Set the Selected property to the Id of the first item in targetAudiences
                var firstAudience = AudiencesList.FirstOrDefault();
                if (firstAudience != null)
                {
                    Selected = firstAudience.Id;
                }
            }
            OnPropertyChanged(nameof(Audience));
        }
    }

    private int _selected;
    public int Selected
    {
        get { return _selected; }
        set
        {
            _selected = value;
            GetTests(value);
            OnPropertyChanged(nameof(Selected));
        }
    }

    private  bool _active;
    public bool Active
    {
        get { return _active; }
        set
        {
            if (_active != value)
            {
                _active = value;
                OnPropertyChanged(nameof(Active));
            }
        }
    }

    private ObservableCollection<TestProjection>? _testCollection;
    public ObservableCollection<TestProjection>? TestCollection
    {
        get { return _testCollection; }
        set { _testCollection = value; OnPropertyChanged(nameof(TestCollection)); }
    }

    public bool IsConfirmed { get ; set ; }
    #endregion

    private ConfirmationModalViewModel confirmationModalViewModel { get; set; }
    public TestOverviewViewModel(NavigationStore navigationStore, ITargetAudience targetAudience = null)
    {
        this.navigationStore = navigationStore;

        // Commands
        OpenTestCommand = new Command(OpenTest);
        GetTestsCommand = new Command(GetTests);
        NewTestCommand = new Command(NewTest);
        DeleteTestCommand = new Command(DeleteTest);
        BackToMainMenuCommand = new Command(BackToMainMenu);

        // Repositories
        targetAudienceRepository = new TargetAudienceRepository();
        testRepository = new TestRepository();

        // Services
        testSerivce = new TestService(testRepository);
        _targetAudienceSerivce = new TargetAudienceService(targetAudienceRepository);
        SetInitialValues(targetAudience);
    }
    private void SetInitialValues(ITargetAudience targetAudience)
    {
        List<ITargetAudience> targetAudiences = _targetAudienceSerivce.GetAllTargetAudiences();
        AudiencesList = targetAudiences;

        if (targetAudience != null)
        {
            Selected = targetAudience.Id;
            Audience = targetAudience;
        }
        else
        {
            Audience = targetAudiences.FirstOrDefault();
            if (Audience != null)
            {
                Selected = Audience.Id;
            }
        }
    }
    private void BackToMainMenu()
    {
        navigationStore!.CurrentViewModel = new TestViewModel(navigationStore);

    }
    private void GetTests(int id)
    {
        UpdateCollection(id);
    }
    private void UpdateCollection(int id)
    {
        TestCollection = testSerivce.GetTestsProjectionForAudience(id);
    }
    private void OpenTest(int id)
    {
        ITest test = testSerivce.GetTest(id);
        navigationStore!.CurrentViewModel = new TestManagementViewModel(navigationStore, this, Audience, test);
    }
    private void NewTest()
    {
        navigationStore!.CurrentViewModel = new TestManagementViewModel(navigationStore, this, Audience);
    }

    private void DeleteTest(int id)
    {
        Action SaveAction = () =>
        {
            ITest test = testSerivce.GetTest(id);
            testSerivce.DeleteTest(test);
            UpdateCollection(id);
        };
        OpenConfirmationModal(CreateAction(SaveAction), "Weet je zeker dat je deze test wilt verwijderen?");
    }

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
}
