using BusinessLogic.IModels;

namespace BusinessLogic.Models;

public class TextQuestionResult : IModel
{
    public Guid Id { get; set; }
    public int Question { get; set; }
    public ICollection<TextQuestionOptionResult>? Options { get; set; }
    public ICollection<TextQuestionAnswerResult> Answers { get; set; }

    public Guid TestResultId { get; set; }
    public TestResult TestResult { get; set; }
}
