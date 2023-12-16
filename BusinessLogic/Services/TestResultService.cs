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
        TestResult testResult = AnalyzeTestResult(testProgressData);

        repository.Store(testResult);

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

    private TestResult AnalyzeTestResult(TestProgressData testProgressData)
    {
        return new TestResult()
        {
            Id = new Guid(),
            TargetAudience = testProgressData.Test.TargetAudience.Label!,
            TestDateTime = DateTime.Now,
            Duration = 0, // TODO Temporary until timer is implemented
            HasHearingLoss = CalculateHearingLoss(testProgressData.ToneAudiometryAnswers),
            TextQuestions = MapTextQuestions(testProgressData.Test.Id, testProgressData.TextAnswers),
            ToneAudiometryQuestions = MapToneAudiometryQuestions(testProgressData.Test.Id, testProgressData.ToneAudiometryAnswers),
        };
    }

    private bool CalculateHearingLoss(List<ToneAudiometryAnswer> answers)
    {
        bool hasHearingLoss = false;

        foreach (ToneAudiometryAnswer answer in answers)
        {
            if (hasHearingLoss == true) break;

            FrequencyMap frequencyMap = FrequencyMapping.Frequencies.First(x => x.Frequency == answer.Frequency);
            int min = frequencyMap.HearingLoss.Min;
            int max = frequencyMap.HearingLoss.Max;

            if (min <= answer.LowestLimitDecibels && max >= answer.LowestLimitDecibels) hasHearingLoss = true;
        } 
        
        return hasHearingLoss;
    }

    // TODO: remove this. change the TestProgressData
    // This is because TestProgressData uses different answer classes than the database
    private List<TextQuestionResult> MapTextQuestions(Guid testResultId, List<TextAnswer> answers)
    {
        List<TextQuestionResult> result = new();

        foreach (TextAnswer answer in answers)
        {
            Guid textAnswerId = new Guid();

            List<TextQuestionOptionResult> tempOption = new();
            answer.Options.ForEach(o => {
                tempOption.Add(new TextQuestionOptionResult() { Id = new Guid(), Option = o, TextQuestionResultId = textAnswerId });
            });

            List<TextQuestionAnswerResult> tempAnswer = new();
            answer.Options.ForEach(a =>{
                tempAnswer.Add(new TextQuestionAnswerResult() { Id = new Guid(), Answer = a, TextQuestionResultId = textAnswerId });
            });

            result.Add(new TextQuestionResult()
            {
                Id = textAnswerId,
                Question = answer.Question,
                Options = tempOption,
                Answers = tempAnswer,
                TestResultId = testResultId
            });
        }

        return result;
    }

    // TODO: remove this. change the TestProgressData
    // This is because TestProgressData uses different answer classes than the database
    private List<ToneAudiometryQuestionResult> MapToneAudiometryQuestions(Guid testResultId, List<ToneAudiometryAnswer> answers)
    {
        List<ToneAudiometryQuestionResult> result = new();

        foreach (ToneAudiometryAnswer answer in answers)
        {
            result.Add(new ToneAudiometryQuestionResult()
            {
                Id = new Guid(),
                Frequency = answer.Frequency,
                StartingDecibels = answer.StartingDecibels,
                LowestDecibels = answer.LowestLimitDecibels,
                Ear = answer.Ear,
                TestResultId = testResultId,
            });
        }

        return result;
    }
}
