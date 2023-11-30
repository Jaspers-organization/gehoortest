using BusinessLogic.IModels;

namespace BusinessLogic.IModels;

public interface ITest: IModel
{
    public string Title { get; set; }
    public ITargetAudience TargetAudience { get; set; }
    public bool Active { get; set; }
    public List<ITextQuestion> TextQuestions {  get; set; }
    public List<IToneAudiometryQuestion> ToneAudiometryQuestions { get; set; }
    public IEmployee Employee { get; set; }
}
