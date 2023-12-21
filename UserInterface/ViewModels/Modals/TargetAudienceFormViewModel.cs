using BusinessLogic.Models;
using BusinessLogic.Services;
using DataAccess.Repositories;
using System;
using System.Windows.Input;
using UserInterface.Commands;
using UserInterface.Stores;

namespace UserInterface.ViewModels.Modals;

internal class TargetAudienceFormViewModel : ViewModelBase
{
    #region dependencies
    private NavigationStore navigationStore;
    private TargetAudienceService service;
    #endregion

    #region properties
    public bool isNew;

    private TargetAudience _targetAudience;
    public TargetAudience TargetAudience
    {
        get { return _targetAudience; }
        set { _targetAudience = value; OnPropertyChanged(nameof(TargetAudience)); }
    }
    #endregion

    #region commands
    public ICommand SaveCommand => new Command(Save);
    public ICommand CancelCommand => new Command(Cancel);
    #endregion

    public TargetAudienceFormViewModel(NavigationStore navigationStore, TargetAudience? targetAudience) 
    {
        this.navigationStore = navigationStore;
        service = new TargetAudienceService(new TargetAudienceRepository(), new TestRepository());

        SetForm(targetAudience);
    }

    private void SetForm(TargetAudience? targetAudience)
    {
        if (targetAudience == null) 
        {
            isNew = true;
            TargetAudience = new TargetAudience() { Id = Guid.NewGuid() };
            return;
        }

        isNew = false;
        TargetAudience =  targetAudience;
    }

    private void Save() 
    {
        try
        {
            if (isNew)
            {
                service.Create(TargetAudience);
            }
            else
            {
                service.Update(TargetAudience);
            }
        } 
        catch(Exception e)
        {
            // TODO
        }

        navigationStore.CloseModal(new TargetAudienceOverviewViewModel(navigationStore));
    }

    private void Cancel() 
    {
        navigationStore.CloseModal(new TargetAudienceOverviewViewModel(navigationStore));
    }
}
