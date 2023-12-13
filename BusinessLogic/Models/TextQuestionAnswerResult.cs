using BusinessLogic.IModels;

namespace BusinessLogic.Models;

public class TextQuestionAnswerResult : IModel
{
    public Guid Id { get; set; }
    public string Answer { get; set; }

    public Guid TextQuestionResultId { get; set; }
    public TextQuestionResult TextQuestionResult { get; set; }
}
