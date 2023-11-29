using BusinessLogic.Enums;

namespace Service.Interfaces.Models;

public interface IToneAudiometryAnswer
{
    public int QuestionNumber { get; set; }
    public int Frequency { get; set; }
    public Ear Ear { get; set; }
    public int StartingDecibels { get; set; }
    public int LowerLimit { get; set; }
}
