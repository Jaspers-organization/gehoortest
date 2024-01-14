using BusinessLogic.Enums;
using BusinessLogic.Interfaces.Models;

namespace BusinessLogic.Models;

public class ToneAudiometryQuestionResult : IModel
{
    public Guid Id { get; set; }
    public int Frequency { get; set; }
    public int StartingDecibels { get; set; }
    public int LowestDecibels { get; set; }
    public Ear Ear { get; set; }

    public Guid TestResultId { get; set; }
    public TestResult TestResult { get; set; }

    public ToneAudiometryQuestionResult() { }

    public ToneAudiometryQuestionResult(Guid id, int frequency, int startingDecibels, int lowestDecibels, Ear ear)
    {
        Id = id;
        Frequency = frequency;
        StartingDecibels = startingDecibels;
        LowestDecibels = lowestDecibels;
        Ear = ear;
    }
}
