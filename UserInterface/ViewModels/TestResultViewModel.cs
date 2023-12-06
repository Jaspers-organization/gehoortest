using DataAccess.MockData;
using UserInterface.Stores;
using BusinessLogic.Classes;
using BusinessLogic.Controllers;
using BusinessLogic.Projections;
using BusinessLogic.Services;
using UserInterface.Commands;
using System.Windows.Input;
using BusinessLogic.BusinessRules;

namespace UserInterface.ViewModels;

internal class TestResultViewModel : ViewModelBase
{
    #region dependencies
    private readonly NavigationStore navigationStore;
    private readonly TestProgressData testProgressData;
    private readonly TestResultRepository testResultRepository;
    private readonly TestResultBusinessLogic testResultBusinessLogic;
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

    private string _positiveTestResult = "Hidden";
    public string PositiveTestResult
    {
        get { return _positiveTestResult; }
        set { _positiveTestResult = value; OnPropertyChanged(nameof(PositiveTestResult)); }
    }

    private string _negativeTestResult = "Hidden";
    public string NegativeTestResult
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

    private string _emailError = "Hidden";
    public string EmailError
    {
        get { return _emailError; }
        set { _emailError = value; OnPropertyChanged(nameof(EmailError)); }
    }
    #endregion

    private TestResultProjection testResult;

    public ICommand SendEmailCommand => new Command(SendEmail);

    public TestResultViewModel(NavigationStore navigationStore, TestProgressData testProgressData)
    {
        this.navigationStore = navigationStore;
        this.testProgressData = testProgressData;
        testResultRepository = new TestResultRepository();
        testResultBusinessLogic = new TestResultBusinessLogic(testResultRepository);

        // TODO: move this to a config file
        string email = "gehoortestapplicatie@gmail.com";
        string password = "f43^9%^Qh@8NLAb$wAkzd5mi";
        string key = "xgob ckck toxn exkz";
        string host = "smtp.gmail.com";
        // ====================

        emailService = new EmailService(new EmailProvider.EmailProvider().Initialize(host, email, key));

        GetTestResult();
    }

    public void GetTestResult() {
        testResult = testResultBusinessLogic.GetTestResult(testProgressData);

        if (testResult.hasHearingLoss)
        {
            TestResultText = "Gehoorschade";
            TestResultExplanation = "Volgens de testresultaten is er mogelijk gehoorschade gevonden. Wij adviseren dat u een afspraak maakt voor een volledige gehoortest met een van onze audiciens.";
            NegativeTestResult = "Visible";
            return;
        }

        TestResultText =  "Gezond gehoor";
        TestResultExplanation = "Volgens de testresultaten heeft u een gezond gehoor. Wij adviseren u om uw gehoor eens per jaar te laten testen.";
        PositiveTestResult = "Visible";
    }

    private void SendEmail()
    {
        if (!EmailBusinessRules.IsValidEmail(Email))
        {
            EmailError = "Visible";
            return;
        }

        EmailError = "Hidden";
        emailService.SendEmail(Email, testResult);
    }
}
