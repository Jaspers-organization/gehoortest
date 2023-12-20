using BusinessLogic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.DataTransferObjects;

[Table("test")]
public class TestEntityTypeConfiguration : IEntityTypeConfiguration<Test>
{
    //[Key]
    //[Column("id", TypeName = "nvarchar(16)")]
    //public Guid Id { get; set; }

    //[Column("title", TypeName = "varchar(50)")]
    //public string Title { get; set; }

    //[Column("active", TypeName = "bit")]
    //public bool Active { get; set; }

    //[Column("target_audience_id", TypeName = "int")]
    //public Guid TargetAudienceId { get; set; }

    //public TargetAudienceDTO? TargetAudience { get; set; }

    //[Column("employee_id", TypeName = "int")]
    //public Guid EmployeeId { get; set; }

    //public EmployeeDTO? Employee { get; set; }

    //public ICollection<TextQuestionDTO>? TextQuestions { get; set; }

    //public ICollection<ToneAudiometryQuestionDTO>? ToneAudiometryQuestions { get; set; }
    public void Configure(EntityTypeBuilder<Test> builder)
    {
        throw new NotImplementedException();
    }
}
