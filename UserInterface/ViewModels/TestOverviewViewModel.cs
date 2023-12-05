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
using gehoortest_application.Repository;
using DataAccess.Repositories;

namespace UserInterface.ViewModels;

internal class TestOverviewViewModel : ViewModelBase, IConfirmation
{
    #region Dependencies
    private readonly NavigationStore? navigationStore;
    private readonly TargetAudienceRepository targetAudienceRepository;
    private readonly TestMockRepository testRepository;
    private readonly TestService testSerivce;
    private readonly TargetAudienceService targetAudienceSerivce;
    private readonly Repository repository;

    #endregion

    #region Commands
    public ICommand OpenTestCommand => new Command(OpenTest);
    public ICommand NewTestCommand => new Command(NewTest);
    public ICommand GetTestsCommand => new Command(GetTests);
    public ICommand DeleteTestCommand => new Command(DeleteTest);
    public ICommand BackToMainMenuCommand => new Command(BackToMainMenu);
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

    private bool _active;
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

    public bool IsConfirmed { get; set; }
    #endregion

    private ConfirmationModalViewModel confirmationModalViewModel { get; set; }
    public TestOverviewViewModel(NavigationStore navigationStore, Repository repository, ITargetAudience targetAudience = null)
    {
        this.navigationStore = navigationStore;
        this.repository = repository;

        targetAudienceRepository = new TargetAudienceRepository(repository);
        testRepository = new TestMockRepository();
        // Services
        testSerivce = new TestService(testRepository);
        targetAudienceSerivce = new TargetAudienceService(targetAudienceRepository);
        SetInitialValues(targetAudience);
    }
    private void SetInitialValues(ITargetAudience targetAudience)
    {
        List<ITargetAudience> targetAudiences = targetAudienceSerivce.GetAllTargetAudiences();
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
        navigationStore!.CurrentViewModel = new TestViewModel(navigationStore, repository);

    }
    private void GetTests(int id)
    {
        UpdateCollection(id);
    }
    //   private void SetActiveTest(TestProjection selectedTest)
    //{
    //    if (selectedTest != null)
    //    {
    //        foreach (var test in TestCollection)
    //        {
    //            test.IsActive = (test == selectedTest);
    //        }
    //    }
    //}

    private ObservableCollection<TestProjection> GetTestData(int id)
    {
        return testSerivce.GetTestsProjectionForAudience(id);
    }
    private void UpdateCollection(int id)
    {
        TestCollection = GetTestData(id);
    }
    private void OpenTest(int id)
    {
        if (Audience == null)
            return;
        ITest test = testSerivce.GetTest(id);
        navigationStore!.CurrentViewModel = new TestManagementViewModel(navigationStore, this, Audience, repository, test);
    }
    private void NewTest()
    {
        if (Audience == null)
            return;
        navigationStore!.CurrentViewModel = new TestManagementViewModel(navigationStore, this, Audience, repository);
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
