using BusinessLogic.Enums;
using BusinessLogic.IModels;

namespace BusinessLogic.Classes;

public class ToneAudiometryAnswer
{
    public int QuestionNumber { get; set; }
    //public int Frequency { get; set; }
    //public Ear Ear { get; set; }
   // public int StartingDecibels { get; set; }
    public int LowerLimit { get; set; }

    #region Sisi
    public int Frequency { get; set; }
    public int StartingDecibels { get; set; }
    public int LowestLimitDecibels { get; set; }
    public Ear Ear { get; set; }

    public ToneAudiometryAnswer(int frequency, int startingDecibels, int lowestLimitDecibels, Ear ear)
    {
        Frequency = frequency;
        StartingDecibels = startingDecibels;
        LowestLimitDecibels = lowestLimitDecibels;
        Ear = ear;
    }
    #endregion Sisi

    public ToneAudiometryAnswer(int questionNumber, int frequency, Ear ear, int startingDecibels, int lowerLimit)
    {
        QuestionNumber = questionNumber;
        Frequency = frequency;
        Ear = ear;
        StartingDecibels = startingDecibels;
        LowerLimit = lowerLimit;
    }

    
   
}
