using Interfaces.Models;

public interface ITextQuestionAnswerResult : IModel
{
    public int TestQuestionResultId { get; set; }
    public string Answer { get; set; }
}
