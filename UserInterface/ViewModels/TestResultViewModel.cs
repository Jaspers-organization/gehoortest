using DataAccess.MockData;
using UserInterface.Stores;
using BusinessLogic.Classes;
using BusinessLogic.Controllers;
using BusinessLogic.Projections;
using UserInterface.Commands;
using System.Windows.Input;
using System.Text.RegularExpressions;

namespace UserInterface.ViewModels;

internal class TestResultViewModel : ViewModelBase
{
    private readonly NavigationStore navigationStore;
    private readonly TestProgressData testProgressData;
    private readonly TestResultRepository testResultRepository;
    private readonly TestResultBusinessLogic testResultBusinessLogic;
    private readonly Service.Services.EmailService emailService;
    private TestResultProjection testResult;

    private string? _testResultText;
    private string? _testResultExplanation;
    private string _positiveTestResult = "Hidden";
    private string _negativeTestResult = "Hidden";
    private string? _email;
    private string _emailError = "Hidden";

    public string? TestResultText {
        get { return _testResultText; }
        set { _testResultText = value; OnPropertyChanged(nameof(TestResultText)); }
    }
    public string? TestResultExplanation {
        get { return _testResultExplanation; }
        set { _testResultExplanation = value; OnPropertyChanged(nameof(TestResultExplanation)); }
    }
    public string PositiveTestResult
    {
        get { return _positiveTestResult; }
        set { _positiveTestResult = value; OnPropertyChanged(nameof(PositiveTestResult)); }
    }
    public string NegativeTestResult
    {
        get { return _negativeTestResult; }
        set { _negativeTestResult = value; OnPropertyChanged(nameof(NegativeTestResult)); }
    }
    public string? Email
    {
        get { return _email; }
        set { _email = value; OnPropertyChanged(nameof(Email)); }
    }
    public string EmailError
    {
        get { return _emailError; }
        set { _emailError = value; OnPropertyChanged(nameof(EmailError)); }
    }

    public ICommand SendEmailCommand => new Command(SendEmail);

    public TestResultViewModel(NavigationStore navigationStore, TestProgressData testProgressData)
    {
        this.navigationStore = navigationStore;
        this.testProgressData = testProgressData;
        testResultRepository = new TestResultRepository();
        testResultBusinessLogic = new TestResultBusinessLogic(testResultRepository);

        // TODO: move this somewhere? 
        string email = "gehoortestapplicatie@gmail.com";
        string password = "f43^9%^Qh@8NLAb$wAkzd5mi";
        string key = "xgob ckck toxn exkz";
        string host = "smtp.gmail.com";
        // ====================

        emailService = new Service.Services.EmailService(new EmailService.EmailService().Initialize(host, email, key));

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
        if (!IsValidEmail())
        {
            EmailError = "Visible";
            return;
        }

        EmailError = "Hidden";
        emailService.SendEmail(Email, testResult);
    }

    private bool IsValidEmail()
    {
        if (string.IsNullOrEmpty(Email)) return false;

        string emailPattern = "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$";
        return Regex.Matches(Email, emailPattern).Count == 1;
    }
}
