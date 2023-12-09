
using BusinessLogic.Models;

namespace BusinessLogic.Classes;

public class TestProgressData
{
    public Test Test { get; set; }
    public int AudimertryTestProgress { get; set; }


    #region Sisi
    //TestProjection Test { get; set; }
    public int CurrentQuestionNumber { get; set; }
    public List<TextAnswer> TextAnswers { get; set; }
    public List<ToneAudiometryAnswer> ToneAudiometryAnswers { get; set; }
    #endregion Sisi
    public TestProgressData(Test test)
    {
        Test = test;
        CurrentQuestionNumber = 0;
        TextAnswers = new List<TextAnswer>();
        AudimertryTestProgress = 0;
        ToneAudiometryAnswers = new List<ToneAudiometryAnswer>();
    }

}

