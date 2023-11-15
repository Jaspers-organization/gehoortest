using gehoorttest.application_Service.Classes;
using gehoorttest.application_Service.IRepositories;
using gehoorttest.application_Service.Projections;

namespace gehoorttest.application_Service.Controllers;

public class TestResultService
{
    private readonly ITestResultRepository _repository;

    public TestResultService(ITestResultRepository repository) => _repository = repository;

    public TestResultProjection GetTestResult(TestProgressData testProgressData)
    {
        bool hasHearingLoss = AnalyzeTestResult(testProgressData);

        // TODO: Save test data.

        return new TestResultProjection(hasHearingLoss);
    }

    private bool AnalyzeTestResult(TestProgressData testProgressData)
    {
        // Temporary for the demo
        return testProgressData.ToneAudiometryAnswers.Where(answer => answer.LowerLimit == 65).Any();
        // ======

        //int leftEarHearingLoss = calculateHearingLoss(answers.LeftEarAnswers);
        //int rightEarHearingLoss = calculateHearingLoss(answers.RightEarAnswers);

        //return leftEarHearingLoss >= 5 || rightEarHearingLoss >= 5;
    }

    //private int calculateHearingLoss((int frequency, int decibels)[] answers)
    //{
    //    int hearingLoss = 0;

    //    // TODO: move this between the loop if the starting decibels change.
    //    int start = ToneAudiometryMapping.GetStartingDecibels(answers[0].frequency);

    //    foreach (var (frequency, decibels) in answers)
    //    {
    //        var (min, max) = ToneAudiometryMapping.GetHearingLossRange(frequency);
            
    //        int difference = decibels - start;
    //        if (difference >= min && difference <= max) hearingLoss++;
    //    }

    //    return hearingLoss;
    //}
}
