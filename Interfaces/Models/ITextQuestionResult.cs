namespace Interfaces.Models;

public interface ITextQuestionResult : IModel
{
    public int TestResultId { get; set; }
    public string Question { get; set; }
    public List<ITextQuestionOptionResult> TextQuestionOptionResults { get; set; }
    public List<ITextQuestionAnswerResult> TextQuestionAnswerResults { get; set; }
}
