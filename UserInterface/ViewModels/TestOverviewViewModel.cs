﻿using DataAccess.MockData;
using BusinessLogic.IModels;
using BusinessLogic.Projections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UserInterface.Stores;
using System.Windows.Input;
using BusinessLogic.Services;
using System.Windows.Documents;
using System.Windows;
using UserInterface.Commands;
using BusinessLogic.Interfaces;
using System;
using UserInterface.ViewModels.Modals;

namespace UserInterface.ViewModels;

internal class TestOverviewViewModel : ViewModelBase, IConfirmation
{
    #region Dependencies
    private readonly NavigationStore? _navigationStore;
    private readonly TargetAudienceRepository _targetAudienceRepository;
    private readonly TestRepository _testRepository;
    private readonly TestService _testSerivce;
    private readonly TargetAudienceService _targetAudienceSerivce;
    #endregion

    #region Commands
    public ICommand OpenTestCommand { get; }
    public ICommand NewTestCommand { get; }
    public ICommand GetTestsCommand { get; }
    public ICommand DeleteTestCommand { get; }
    #endregion

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

    private ConfirmationModalViewModel _confirmationModalViewModel { get; set; }
    public TestOverviewViewModel(NavigationStore navigationStore)
    {
        _navigationStore = navigationStore;

        //commands
        OpenTestCommand = new Command(OpenTest);
        GetTestsCommand = new Command(GetTests);
        NewTestCommand = new Command(NewTest);
        DeleteTestCommand = new Command(DeleteTest);

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
        UpdateCollection(id);
    }
    private void UpdateCollection(int id)
    {
        TestCollection = _testSerivce.GetTestsProjectionForAudience(id);
    }
    public void OpenTest(int id)
    {
        ITest test = _testSerivce.GetTest(id);
        _navigationStore!.CurrentViewModel = new TestManagementViewModel(_navigationStore, this, test);
    }
    public void NewTest()
    {
        _navigationStore!.CurrentViewModel = new TestManagementViewModel(_navigationStore, this);
    }
   
    public void DeleteTest(int id)
    {
        Action SaveAction = () =>
        {
            ITest test = _testSerivce.GetTest(id);
            _testSerivce.DeleteTest(test);
            UpdateCollection(id);
        };
        OpenConfirmationModal(CreateAction(SaveAction), "Weet je zeker dat je deze test wilt verwijderen?");
    }
    public void SetConfirmed(bool value)
    {
        IsConfirmed = value;
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
        _confirmationModalViewModel = new ConfirmationModalViewModel(_navigationStore, text, this, action);
        _navigationStore.OpenModal(_confirmationModalViewModel);
    }
}
