using Interfaces.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

[Table("test")]
internal sealed class Test : ITest
{
    [Column("id")]
    public int Id { get; set; }

    [Column("target_audience_id")]
    public int TargetAudienceId { get; set; }
    public ITargetAudience TargetAudience { get; set; }

    [Column("employee_id")]
    public int EmployeeId { get; set; }
    public IEmployee Employee { get; set; }

    [Column("title")]
    public string Title { get; set; }

    public List<ITextQuestion> TextQuestions { get; set; }

    public List<IToneAudiometryQuestion> ToneAudiometryQuestions { get; set; }

    [Column("active")]
    public bool Active { get; set; }
}
