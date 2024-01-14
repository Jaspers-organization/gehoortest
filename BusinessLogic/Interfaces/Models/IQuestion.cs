using BusinessLogic.Enums;

namespace BusinessLogic.Interfaces.Models;

public interface IQuestion
{
    public int QuestionNumber { get; set; }
    public QuestionType QuestionType { get; set; }
}
