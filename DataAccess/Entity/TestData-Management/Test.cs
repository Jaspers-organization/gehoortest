using DataAccess.Models.LoginData_Management;
using BusinessLogic.IModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.TestData_Management;

[Table("test")]
public class Test: ITest
{
    [Column("id")]
    public int Id { get; set; }
    [Column("title")]
    public string? Title { get; set; }
    public ITargetAudience? TargetAudience { get; set; }

    [Column("active")]
    public bool Active { get; set; }

    public List<ITextQuestion>? TextQuestions { get; set; }

    public List<IToneAudiometryQuestion>? ToneAudiometryQuestions { get; set ; }
    public IEmployee? Employee { get; set; }

}
