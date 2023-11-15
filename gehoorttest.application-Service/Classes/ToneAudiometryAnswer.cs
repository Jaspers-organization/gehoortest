using gehoorttest.application_Service.Enums;
using gehoorttest.application_Service.IModels;

namespace gehoorttest.application_Service.Classes;

public class ToneAudiometryAnswer : IToneAudiometryAnswer
{
    public int QuestionNumber { get; set; }
    public int Frequency { get; set; }
    public Ear Ear { get; set; }
    public int StartingDecibels { get; set; }
    public int LowerLimit { get; set; }

    public ToneAudiometryAnswer(int questionNumber, int frequency, Ear ear, int startingDecibels, int lowerLimit)
    {
        QuestionNumber = questionNumber;
        Frequency = frequency;
        Ear = ear;
        StartingDecibels = startingDecibels;
        LowerLimit = lowerLimit;
    }
}
