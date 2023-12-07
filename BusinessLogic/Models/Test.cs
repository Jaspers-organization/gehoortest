using BusinessLogic.IModels;

namespace BusinessLogic.Models;

public class Test : ITest
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public bool Active { get; set; }
    public ITargetAudience TargetAudience { get; set; }
    public List<ITextQuestion> TextQuestions { get; set; }
    public List<IToneAudiometryQuestion> ToneAudiometryQuestions { get; set; }
    public IEmployee Employee { get; set; }
}
