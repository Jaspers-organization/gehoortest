namespace Interfaces.Models;

public interface ITextQuestion : IModel
{
    public int TestId { get; set; }
    public int QuestionNumber { get; set; }
    public string Question { get; set; }
    public List<ITextQuestionOption> TextQuestionOption { get; set; }
    public bool IsMultipleSelect { get; set; }
    public bool HasInputField { get; set; }
    public string? Image { get; set; }
}
