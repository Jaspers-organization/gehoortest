using BusinessLogic.IModels;

namespace BusinessLogic.Projections;

public class TestProjection
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool Active { get; set; }
    public string EmployeeName { get; set; }
    public int AmountOfQuestions { get; set; }
   
    public int TargetAudienceId { get; set; }
    public List<ITextQuestion> TextQuestions { get; set; }
    public List<IToneAudiometryQuestion> ToneAudiometryQuestions { get; set; }
    
}
