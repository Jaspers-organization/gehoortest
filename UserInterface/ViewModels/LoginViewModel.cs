using BusinessLogic.Guards;
using BusinessLogic.Projections;
using BusinessLogic.Services;
using DataAccess.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.Windows;
using System.Windows.Input;
using UserInterface.Commands;
using UserInterface.Stores;

namespace UserInterface.ViewModels;

internal class LoginViewModel : ViewModelBase
{
    #region dependencies
    private NavigationStore navigationStore;
    private LoginService service;
    #endregion

    #region properties
    private string? _email;
    public string? Email
    {
        get { return _email; }
        set { _email = value; OnPropertyChanged(nameof(Email)); }
    }

    private string? _password;
    public string? Password
    {
        get { return _password; }
        set { _password = value; OnPropertyChanged(nameof(Password)); }
    }

    private Visibility _showInputError = Visibility.Hidden;
    public Visibility ShowInputError
    {
        get { return _showInputError; }
        set { _showInputError = value; OnPropertyChanged(nameof(ShowInputError)); }
    }
    #endregion

    #region commands
    public ICommand LoginCommand => new Command(Login);
    #endregion

    public LoginViewModel(NavigationStore navigationStore)
    {
        this.navigationStore = navigationStore;
        this.navigationStore.AddPreviousViewModel(new HomeViewModel(navigationStore));

        this.service = new LoginService(new EmployeeLoginRepository(), new HashingService.HashingService());
    }

    private void Login()
    {
        ShowInputError = Visibility.Hidden;

        if (Password.IsNullOrEmpty() || !Guard.IsValidEmail(Email))
        {
            ShowInputError = Visibility.Visible;
            return;
        }

        EmployeeProjection? employee = service.Login(Email, Password);

        if (employee == null)
        {
            ShowInputError = Visibility.Visible;
            return;
        }

        navigationStore.LoggedInEmployee = employee;
        navigationStore.CurrentViewModel = new EmployeePortalViewModel(navigationStore);
    }
}
