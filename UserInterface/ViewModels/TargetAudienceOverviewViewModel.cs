using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using BusinessLogic.Services;
using DataAccess.Repositories;
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
    private List<TargetAudience> _targetAudiences;
    public List<TargetAudience> TargetAudiences
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
        // TODO hide close application button
        this.navigationStore.AddPreviousViewModel(new EmployeePortalViewModel(navigationStore));

        service = new TargetAudienceService(new TargetAudienceRepository(), new TestRepository());

        Get();
    }

    private void Get()
    {
        TargetAudiences = service.GetAllTargetAudiences();
    }

    private void Create()
    {
        navigationStore.OpenModal(new TargetAudienceFormViewModel(navigationStore, null));
    }

    private void Update(TargetAudience targetAudience)
    {
        navigationStore.OpenModal(new TargetAudienceFormViewModel(navigationStore, targetAudience));
    }

    private void Delete(Guid id)
    {
        // TODO refresh list after deletion

        navigationStore.OpenModal(new ConfirmationModalViewModel(
            navigationStore, 
            "Weet je zeker dat je deze leeftijdsgroep wilt verwijderen?",
            this,
            () => service.Delete(id)
        ));
    }
}
