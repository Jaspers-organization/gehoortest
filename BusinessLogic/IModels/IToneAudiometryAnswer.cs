using BusinessLogic.Enums;

namespace BusinessLogic.IModels;

public interface IToneAudiometryAnswer
{
    public int QuestionNumber { get; set; }
    public int Frequency { get; set; }
    public Ear Ear { get; set; }
    public int StartingDecibels { get; set; }
    public int LowerLimit { get; set; }
}
