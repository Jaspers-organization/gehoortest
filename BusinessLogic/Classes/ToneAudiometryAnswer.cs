using BusinessLogic.Enums;
using BusinessLogic.IModels;

namespace BusinessLogic.Classes;

public class ToneAudiometryAnswer : IToneAudiometryAnswer
{
    public int QuestionNumber { get; set; }
    public int Frequency { get; set; }
    public Ear Ear { get; set; }
    public int StartingDecibels { get; set; }
    public int LowestLimitDecibels { get; set; }
    public string Answer { get; set; }

    public ToneAudiometryAnswer(int questionNumber, int frequency, Ear ear, int startingDecibels, int lowestLimitDecibels, string answer)
    {
        QuestionNumber = questionNumber;
        Frequency = frequency;
        Ear = ear;
        StartingDecibels = startingDecibels;
        LowestLimitDecibels = lowestLimitDecibels;
        Answer = answer;
    }
  

    

    
   
}
