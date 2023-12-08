using BusinessLogic.IModels;

namespace BusinessLogic.Models;

public class TextQuestionOption : IModel
{
    public Guid Id { get; set; }
    public string Option { get; set; }

    public Guid TextQuestionId { get; set; }
    public TextQuestion? TextQuestion { get; set; }

}
