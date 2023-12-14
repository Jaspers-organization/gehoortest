using BusinessLogic.IModels;

namespace BusinessLogic.Models;

public class TestResult : IModel
{
    public Guid Id { get; set; }
    public string TargetAudience { get; set; }
    public DateTime TestDateTime { get; set; }
    public int Duration { get; set; }
    public bool HasHearingLoss { get; set; }

    public ICollection<TextQuestionResult>? TextQuestions { get; set; }
    public ICollection<ToneAudiometryQuestionResult>? ToneAudiometryQuestions { get; set; }
}
