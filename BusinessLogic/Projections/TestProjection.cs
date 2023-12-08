using BusinessLogic.Models;

namespace BusinessLogic.Projections;

public class TestProjection 
{
    public Guid Id { get; set; }
    public string Title { get; set; }

    public bool Active { get; set; }

    public string EmployeeName { get; set; }
    public int AmountOfQuestions { get; set; }
   
    public int TargetAudienceId { get; set; }
    public List<TextQuestion> TextQuestions { get; set; }
    public List<ToneAudiometryQuestion> ToneAudiometryQuestions { get; set; }
    
}
