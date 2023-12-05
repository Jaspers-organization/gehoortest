using DataAccess.MockData;
using UserInterface.Stores;
using BusinessLogic.Classes;
using BusinessLogic.Controllers;
using BusinessLogic.Enums;
using BusinessLogic.Projections;

namespace UserInterface.ViewModels;

internal class TestResultViewModel : ViewModelBase
{
    private readonly NavigationStore navigationStore;
    private readonly TestProgressData testProgressData;
    private readonly TestResultMockRepository testResultRepository;
    private readonly TestResultBusinessLogic testResultBusinessLogic;

    private string? _testResultText;
    private string? _testResultExplanation;

    public string? TestResultText
    {
        get { return _testResultText; }
        set { _testResultText = value; OnPropertyChanged(nameof(TestResultText)); }
    }
    public string? TestResultExplanation
    {
        get { return _testResultExplanation; }
        set { _testResultExplanation = value; OnPropertyChanged(nameof(TestResultExplanation)); }
    }

    public TestResultViewModel(NavigationStore navigationStore, TestProgressData testProgressData)
    {
        this.navigationStore = navigationStore;
        this.testProgressData = testProgressData;
        testResultRepository = new TestResultMockRepository();
        testResultBusinessLogic = new TestResultBusinessLogic(testResultRepository);

        // PopulateMockData();
        GetTestResult();
    }

    private void PopulateMockData()
    {
        /** positive test - based on Dinny */
        testProgressData.ToneAudiometryAnswers.Add(new ToneAudiometryAnswer(1, 250, Ear.Left, 30, 30));
        testProgressData.ToneAudiometryAnswers.Add(new ToneAudiometryAnswer(2, 500, Ear.Left, 30, 35));
        testProgressData.ToneAudiometryAnswers.Add(new ToneAudiometryAnswer(3, 1000, Ear.Left, 30, 30));
        testProgressData.ToneAudiometryAnswers.Add(new ToneAudiometryAnswer(4, 2000, Ear.Left, 30, 30));
        testProgressData.ToneAudiometryAnswers.Add(new ToneAudiometryAnswer(5, 4000, Ear.Left, 30, 35));
        testProgressData.ToneAudiometryAnswers.Add(new ToneAudiometryAnswer(6, 8000, Ear.Left, 30, 35));

        testProgressData.ToneAudiometryAnswers.Add(new ToneAudiometryAnswer(7, 250, Ear.Right, 30, 35));
        testProgressData.ToneAudiometryAnswers.Add(new ToneAudiometryAnswer(8, 500, Ear.Right, 30, 30));
        testProgressData.ToneAudiometryAnswers.Add(new ToneAudiometryAnswer(9, 1000, Ear.Right, 30, 30));
        testProgressData.ToneAudiometryAnswers.Add(new ToneAudiometryAnswer(10, 2000, Ear.Right, 30, 30));
        testProgressData.ToneAudiometryAnswers.Add(new ToneAudiometryAnswer(11, 4000, Ear.Right, 30, 30));
        testProgressData.ToneAudiometryAnswers.Add(new ToneAudiometryAnswer(12, 8000, Ear.Right, 30, 30));
    }

    public void GetTestResult()
    {
        TestResultProjection testResult = testResultBusinessLogic.GetTestResult(testProgressData);

        TestResultText = testResult.hasHearingLoss
            ? "Gehoorschade"
            : "Gezond gehoor";

        TestResultExplanation = testResult.hasHearingLoss
            ? "Volgens de testresultaten is er mogelijk gehoorschade gevonden. Wij adviseren dat u een afspraak maakt voor een volledige gehoortest met een van onze audiciens."
            : "Volgens de testresultaten heeft u een gezond gehoor. Wij adviseren u om uw gehoor eens per jaar te laten testen.";
    }
}
