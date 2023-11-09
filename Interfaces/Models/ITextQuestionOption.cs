namespace Interfaces.Models;

public interface ITextQuestionOption : IModel
{
    public int TestQuestionId { get; set; }
    public string Option { get; set; }
}
