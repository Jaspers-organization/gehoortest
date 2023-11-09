namespace Interfaces.Models;

public interface ITextQuestionOptionResult : IModel
{
    public int TestQuestionResultId { get; set; }
    public string Option { get; set; }
}
