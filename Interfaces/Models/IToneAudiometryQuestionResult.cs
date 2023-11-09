using Interfaces.Enums;

namespace Interfaces.Models;

public interface IToneAudiometryQuestionResult : IModel
{
    public int TestResultId { get; set; }
    public int Frequency { get; set; }
    public Ear Ear { get; set; }
    public int StartingDecibels { get; set; }
    public int Answer { get; set; }
}
