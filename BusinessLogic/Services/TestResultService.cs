using BusinessLogic.Classes;
using BusinessLogic.Projections;
using BusinessLogic.IRepositories;
using BusinessLogic.Models;
using BusinessLogic.DataMappings;

namespace BusinessLogic.Services;

public class TestResultService
{
    private readonly ITestResultRepository repository;

    public TestResultService(ITestResultRepository repository)
    {
        this.repository = repository;
    }

    public TestResultProjection GetTestResult(TestProgressData testProgressData)
    {
        bool hasHearingLoss = CalculateHearingLoss(testProgressData.ToneAudiometryQuestionResults);

        TestResult testResult = CreateTestResult(hasHearingLoss, testProgressData);

        StoreTestResult(testResult);

        return CreateTestResultProjection(testResult);
    }

    private bool CalculateHearingLoss(List<ToneAudiometryQuestionResult> answers)
    {
        bool hasHearingLoss = false;

        foreach (ToneAudiometryQuestionResult answer in answers)
        {
            if (hasHearingLoss == true) break;

            FrequencyMap frequencyMap = FrequencyMapping.Frequencies.First(x => x.Frequency == answer.Frequency);
            int min = frequencyMap.HearingLoss.Min;
            int max = frequencyMap.HearingLoss.Max;

            if (min <= answer.LowestDecibels && max >= answer.LowestDecibels) hasHearingLoss = true;
        }

        return hasHearingLoss;
    }

    private TestResult CreateTestResult(bool hasHearingLoss, TestProgressData testProgressData)
    {
        Guid testResultId = Guid.NewGuid();

        testProgressData.ToneAudiometryQuestionResults.ForEach(item => item.TestResultId = testResultId);
        testProgressData.TextQuestionResults.ForEach(item => item.TestResultId = testResultId);

        return new TestResult()
        {
            Id = testResultId,
            TargetAudience = testProgressData.Test.TargetAudience.Label,
            TestDateTime = DateTime.Now,
            Duration = 0, // TODO Temporary until timer is implemented
            HasHearingLoss = hasHearingLoss,
            TextQuestions = testProgressData.TextQuestionResults,
            ToneAudiometryQuestions = testProgressData.ToneAudiometryQuestionResults,
        };
    }

    private void StoreTestResult(TestResult testResult)
    {
        repository.Store(testResult);
    }

    private TestResultProjection CreateTestResultProjection(TestResult testResult)
    {
        return new TestResultProjection()
        {
            TestResultId = testResult.Id,
            TestResultText = testResult.HasHearingLoss
                ? "Mogelijk gehoorschade"
                : "Gezond gehoor",
            TestResultExplanation = testResult.HasHearingLoss
                ? "Volgens de testresultaten is er mogelijk gehoorschade gevonden. Wij adviseren dat u een afspraak maakt voor een volledige gehoortest met een van onze audiciens."
                : "Volgens de testresultaten heeft u een gezond gehoor. Wij adviseren u om uw gehoor eens per jaar te laten testen.",
            HasHearingLoss = testResult.HasHearingLoss,
        };
    }
}
