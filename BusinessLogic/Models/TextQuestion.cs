using BusinessLogic.Enums;
using BusinessLogic.IModels;

namespace BusinessLogic.Models;

public class TextQuestion : ITextQuestion
{
    public int Id { get; set; }
    public string Question { get; set; }
    public virtual List<string>? Options { get; set; }
    public bool IsMultiSelect { get; set; }
    public bool HasInputField { get; set; }
    public int QuestionNumber { get; set; }
    public QuestionType QuestionType { get; set; }

}
