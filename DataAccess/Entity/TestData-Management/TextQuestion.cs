using BusinessLogic.Enums;
using BusinessLogic.IModels;

namespace DataAccess.Entity.TestData_Management;

public class TextQuestion : ITextQuestion
{
    public int Id { get; set; }
    public string Question { get ; set ; }
    public List<string>? Options { get; set; }
    public bool IsMultiSelect { get ; set ; }
    public bool HasInputField { get ; set ; }
    public int QuestionNumber { get ; set ; }
    public QuestionType QuestionType { get; set; }
}
