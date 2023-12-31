﻿using BusinessLogic.Enums;
using BusinessLogic.Projections;
using System.Windows;
using System.Windows.Input;
using UserInterface.Commands;
using UserInterface.Stores;

namespace UserInterface.ViewModels;

internal class EmployeePortalViewModel : ViewModelBase
{
    #region dependencies
    private NavigationStore navigationStore;
    #endregion

    #region commands
    public ICommand OpenTestOverviewCommand => new Command(OpenTestOverview);
    #endregion

    #region properties
    private string? _employeeName = "Welkom | ";
    public string? EmployeeName
    {
        get { return _employeeName; }
        set { _employeeName = $"Welkom | {value}"; OnPropertyChanged(nameof(EmployeeName)); }
    }

    private Visibility _showAdminButtons = Visibility.Hidden;
    public Visibility ShowAdminButtons
    {
        get { return _showAdminButtons; }
        set { _showAdminButtons = value; OnPropertyChanged(nameof(ShowAdminButtons)); }
    }
    #endregion

    public EmployeePortalViewModel(NavigationStore navigationStore)
    {
        this.navigationStore = navigationStore;
        this.navigationStore.ClearPreviousViewModel();

        SetEmployee();
    }

    private void SetEmployee()
    {
        EmployeeProjection employee = navigationStore.LoggedInEmployee!;

        EmployeeName = employee.FullName;
        if (employee.Role == Role.Administrator)
        {
            ShowAdminButtons = Visibility.Visible;
        }
    }

    private void OpenTestOverview()
    {
        navigationStore.CurrentViewModel = new TestOverviewViewModel(navigationStore);
    }
}
