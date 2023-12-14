using UserInterface.Stores;
using BusinessLogic.Classes;
using BusinessLogic.Projections;
using BusinessLogic.Services;
using UserInterface.Commands;
using System.Windows.Input;
using BusinessLogic.BusinessRules;
using BusinessLogic.Controllers;
using DataAccess.Repositories;
using System;
using System.Windows;

namespace UserInterface.ViewModels;

internal class TestResultViewModel : ViewModelBase
{
    #region dependencies
    private readonly NavigationStore navigationStore;
    private readonly TestResultService testResultService;
    private readonly EmailService emailService;
    #endregion

    #region properies
    private string? _testResultText;
    public string? TestResultText {
        get { return _testResultText; }
        set { _testResultText = value; OnPropertyChanged(nameof(TestResultText)); }
    }

    private string? _testResultExplanation;
    public string? TestResultExplanation {
        get { return _testResultExplanation; }
        set { _testResultExplanation = value; OnPropertyChanged(nameof(TestResultExplanation)); }
    }

    private Visibility _positiveTestResult = Visibility.Hidden;
    public Visibility PositiveTestResult
    {
        get { return _positiveTestResult; }
        set { _positiveTestResult = value; OnPropertyChanged(nameof(PositiveTestResult)); }
    }

    private Visibility _negativeTestResult = Visibility.Hidden;
    public Visibility NegativeTestResult
    {
        get { return _negativeTestResult; }
        set { _negativeTestResult = value; OnPropertyChanged(nameof(NegativeTestResult)); }
    }

    private string? _email;
    public string? Email
    {
        get { return _email; }
        set { _email = value; OnPropertyChanged(nameof(Email)); }
    }

    private Visibility _emailError = Visibility.Hidden;
    public Visibility EmailError
    {
        get { return _emailError; }
        set { _emailError = value; OnPropertyChanged(nameof(EmailError)); }
    }
    #endregion

    #region commands
    public ICommand SendEmailCommand => new Command(SendEmail);
    #endregion

    private Guid testResultId;

    public TestResultViewModel(NavigationStore navigationStore, TestProgressData testProgressData)
    {
        this.navigationStore = navigationStore;
        testResultService = new TestResultService(new TestResultRepository());

        // TODO: move this to a config file
        string email = "gehoortestapplicatie@gmail.com";
        string password = "f43^9%^Qh@8NLAb$wAkzd5mi";
        string key = "xgob ckck toxn exkz";
        string host = "smtp.gmail.com";
        // ====================

        emailService = new EmailService(new EmailProvider.EmailProvider().Initialize(host, email, key));

        GetTestResult(testProgressData);
    }

    public void GetTestResult(TestProgressData testProgressData)
    {
        TestResultProjection testResult = testResultService.GetTestResult(testProgressData);

        testResultId = testResult.TestResultId;
        TestResultText = testResult.TestResultText;
        TestResultExplanation = testResult.TestResultExplanation;

        NegativeTestResult = testResult.HasHearingLoss ? Visibility.Visible : Visibility.Hidden;
        PositiveTestResult = testResult.HasHearingLoss ? Visibility.Hidden : Visibility.Visible;
    }

    private void SendEmail()
    {
        if (!EmailBusinessRules.IsValidEmail(Email))
        {
            EmailError = Visibility.Visible;
            return;
        }

        EmailError = Visibility.Hidden;
        emailService.SendEmail(Email, testResultId);
    }
}