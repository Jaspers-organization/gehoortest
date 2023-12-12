using BusinessLogic.IModels;

namespace BusinessLogic.Models;

public class TextQuestionOptionResult : IModel
{
    public Guid Id { get; set; }
    public string Option { get; set; }

    public Guid TextQuestionResultId { get; set; }
    public TextQuestionResult TextQuestionResult { get; set; }
}
