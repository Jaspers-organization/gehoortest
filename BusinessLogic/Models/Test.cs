using BusinessLogic.IModels;

namespace BusinessLogic.Models;

public class Test : IModel
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public bool Active { get; set; }

    public Guid TargetAudienceId { get; set; }
    public TargetAudience TargetAudience { get; set; }

    public ICollection<TextQuestion> TextQuestions { get; set; } = new List<TextQuestion>();

    public ICollection<ToneAudiometryQuestion> ToneAudiometryQuestions { get; set; } = new List<ToneAudiometryQuestion>();

    public Guid EmployeeId { get; set; }
    public Employee Employee { get; set; }
}

