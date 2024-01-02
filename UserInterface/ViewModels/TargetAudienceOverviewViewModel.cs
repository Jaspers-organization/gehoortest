using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using BusinessLogic.Services;
using DataAccess.Repositories;
using Service.Projections;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using UserInterface.Commands;
using UserInterface.Stores;
using UserInterface.ViewModels.Modals;

namespace UserInterface.ViewModels;

internal class TargetAudienceOverviewViewModel : ViewModelBase, IConfirmation
{
    #region dependencies
    private NavigationStore navigationStore;
    private TargetAudienceService service;
    #endregion

    #region properties
    private List<TargetAudienceProjection> _targetAudiences;
    public List<TargetAudienceProjection> TargetAudiences
    {
        get { return _targetAudiences; }
        set { _targetAudiences = value; OnPropertyChanged(nameof(TargetAudiences)); }
    }

    public bool IsConfirmed { get; set; }
    #endregion

    #region commands
    public ICommand CreateCommand => new Command(Create);
    public ICommand UpdateCommand => new Command(Update);
    public ICommand DeleteCommand => new Command(Delete);
    #endregion

    public TargetAudienceOverviewViewModel(NavigationStore navigationStore)
    {
        this.navigationStore = navigationStore;
        this.navigationStore.AddPreviousViewModel(new EmployeePortalViewModel(navigationStore));

        service = new TargetAudienceService(new TargetAudienceRepository(), new TestRepository());

        Get();
    }

    private void Get()
    {
        TargetAudiences = service.GetAllTargetAudienceProjections();
    }

    private void Create()
    {
        navigationStore.OpenModal(new TargetAudienceModalViewModel(navigationStore, null));
    }

    private void Update(Guid id)
    {
        TargetAudience? targetAudience = service.Get(id);
        navigationStore.OpenModal(new TargetAudienceModalViewModel(navigationStore, targetAudience));
    }

    private void Delete(Guid id)
    {
        Action confirmDeleteAction = CreateAction(() =>
        {
            try 
            {
                service.Delete(id); 
            }
            catch (Exception e)
            {
                navigationStore.OpenModal(new ErrorModalViewModal(navigationStore, e.Message));
            }
            Get();
        });

        OpenConfirmationModal(confirmDeleteAction, "Weet je zeker dat je deze leeftijdsgroep wilt verwijderen?");
    }

    public Action CreateAction(Action action)
    {
        return () =>
        {
            if (!IsConfirmed) return;
            action.Invoke();
        };
    }

    public void OpenConfirmationModal(Action action, string text)
    {
        navigationStore.OpenModal(new ConfirmationModalViewModel(navigationStore, text, this, action));
    }
}
