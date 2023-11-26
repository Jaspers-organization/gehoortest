﻿using BusinessLogic.Controllers;
using DataAccess.MockData;
using BusinessLogic.IModels;
using BusinessLogic.Projections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserInterface.Stores;
using BusinessLogic.Classes;

namespace UserInterface.ViewModels;

internal class TestOverviewViewModel: ViewModelBase
{
    public List<ITest> Tests {  get; set; }
    private readonly NavigationStore? _navigationStore;
    private readonly TargetAudienceRepository _targetAudienceRepository;
    private readonly TestRepository _testRepository;

    public TestOverviewViewModel(NavigationStore navigationStore)
    {
        _navigationStore = navigationStore;

        _targetAudienceRepository = new TargetAudienceRepository();
        ITargetAudience targetAudience = _targetAudienceRepository.GetAllAudiences().FirstOrDefault();
        _testRepository = new TestRepository(targetAudience);

        AudiencesList = _targetAudienceRepository.GetAllAudiences().Select(audience => audience.Label).ToList();
        TestCollection = _testRepository.GetTestsProjectionForAudience(targetAudience.Id);

    }
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
    
    public void EditTest(int id)
    {
        ITest test = _testRepository.GetTest(id);
        _navigationStore!.CurrentViewModel = new TestManagementViewModel(_navigationStore, test);

    }
    
}
