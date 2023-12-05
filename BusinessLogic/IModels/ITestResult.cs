namespace BusinessLogic.IModels;

public interface ITestResult : IModel
{
    public IBranch Branch { get; set; }
    public DateTime StartDate { get; set; }
    public int TestDuration { get; set; }
    public List<ITextAnswer> TextAnswers { get; set; }
    public List<IToneAudiometryAnswer> ToneAudiometryAnswers { get; set; }
}
