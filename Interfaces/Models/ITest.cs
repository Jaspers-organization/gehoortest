namespace Interfaces.Models;

public interface ITest
{
    public ITargetAudience TargetAudience { get; set; }
    public IEmployee Employee { get; set; }
    public string Title { get; set; }
    public List<ITextQuestion> TextQuestions { get; set; }
    public List<IToneAudiometryQuestion> ToneAudiometryQuestions { get; set; }
    public bool Active { get; set; }
}
