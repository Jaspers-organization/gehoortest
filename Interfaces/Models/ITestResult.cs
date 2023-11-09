namespace Interfaces.Models;

public interface ITestResult : IModel
{
    public ITargetAudience TargetAudience { get; set; }
    public IBranch Branch{ get; set; }
    public DateTime TestDateTime { get; set; }
    public int Duration { get; set; }
    public List<ITextQuestionResult> TextQuestionResults { get; set; }
    public List<IToneAudiometryQuestionResult> ToneAudiometryQuestions { get; set; }
}
