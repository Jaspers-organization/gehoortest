using BusinessLogic.Enums;
using BusinessLogic.IModels;

namespace BusinessLogic.Models;

public class TextQuestion : IModel, IQuestion
{
    public Guid Id { get; set; }
    public string Question { get; set; }
    public ICollection<TextQuestionOption>? Options { get; set; } = new List<TextQuestionOption>();
    public bool IsMultiSelect { get; set; }
    public bool HasInputField { get; set; }
    public int QuestionNumber { get; set; }
    public QuestionType QuestionType { get; set; }

    public Guid TestId { get; set; }
    public Test? Test { get; set; }
}

