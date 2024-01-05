using UserInterface.Stores;
using BusinessLogic.Classes;
using BusinessLogic.Services;
using UserInterface.Commands;
using System.Windows.Input;
using DataAccess.Repositories;
using System;
using System.Windows;
using BusinessLogic.Guards;
using BusinessLogic.Projections;

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

    private Visibility _ShowPositiveImage = Visibility.Hidden;
    public Visibility ShowPositiveImage
    {
        get { return _ShowPositiveImage; }
        set { _ShowPositiveImage = value; OnPropertyChanged(nameof(ShowPositiveImage)); }
    }

    private Visibility _showNegativeImage = Visibility.Hidden;
    public Visibility ShowNegativeImage
    {
        get { return _showNegativeImage; }
        set { _showNegativeImage = value; OnPropertyChanged(nameof(ShowNegativeImage)); }
    }

    private string? _email;
    public string? Email
    {
        get { return _email; }
        set { _email = value; OnPropertyChanged(nameof(Email)); }
    }

    private Visibility _emailInvalidError = Visibility.Hidden;
    public Visibility EmailInvalidError
    {
        get { return _emailInvalidError; }
        set { _emailInvalidError = value; OnPropertyChanged(nameof(EmailInvalidError)); }
    }

    private Visibility _emailSendError = Visibility.Hidden;
    public Visibility EmailSendError
    {
        get { return _emailSendError; }
        set { _emailSendError = value; OnPropertyChanged(nameof(EmailSendError)); }
    }

    private Visibility _emailSendSuccess = Visibility.Hidden;
    public Visibility EmailSendSuccess
    {
        get { return _emailSendSuccess; }
        set { _emailSendSuccess = value; OnPropertyChanged(nameof(EmailSendSuccess)); }
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

        emailService = new EmailService(
            new TestResultRepository(), 
            new SettingsRepository(),
            new EmailProvider.EmailProvider().Initialize(host, email, key)
        );

        GetTestResult(testProgressData);
    }

    public void GetTestResult(TestProgressData testProgressData)
    {
        TestResultProjection testResult = testResultService.GetTestResult(testProgressData);

        testResultId = testResult.TestResultId;
        TestResultText = testResult.TestResultText;
        TestResultExplanation = testResult.TestResultExplanation;

        ShowNegativeImage = testResult.HasHearingLoss ? Visibility.Visible : Visibility.Hidden;
        ShowPositiveImage = testResult.HasHearingLoss ? Visibility.Hidden : Visibility.Visible;
    }

    private void SendEmail()
    {
        if (!Guard.IsValidEmail(Email))
        {
            EmailInvalidError = Visibility.Visible;
            return;
        }
        EmailInvalidError = Visibility.Hidden;

        try
        {
            emailService.SendEmail(Email, testResultId);
        } 
        catch (Exception e) 
        {
            ShowEmailSendError();
            return;
        }

        ShowEmailSendSuccess();
    }

    private void ShowEmailSendError()
    {
        EmailSendSuccess = Visibility.Hidden;
        EmailSendError = Visibility.Visible;
    }

    private void ShowEmailSendSuccess()
    {
        EmailSendError = Visibility.Hidden;
        EmailSendSuccess = Visibility.Visible;
    }
}