namespace Interfaces.Models;

public interface IToneAudiometryQuestion: IModel
{
    public int TestId { get; set; }
    public int QuestionNumber { get; set; }
    public int Frequency { get; set; }
    public int StartingDecibels { get; set; }
}
