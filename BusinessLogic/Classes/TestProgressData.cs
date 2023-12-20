
using BusinessLogic.Models;

namespace BusinessLogic.Classes;

public class TestProgressData
{
    public Test Test { get; set; }
    public int CurrentQuestionNumber { get; set; }
    public List<TextAnswer> TextAnswers { get; set; }
    public List<ToneAudiometryAnswer> ToneAudiometryAnswers { get; set; }
    public TestProgressData(Test test)
    {
        Test = test;
        CurrentQuestionNumber = 0;
        TextAnswers = new List<TextAnswer>();
        ToneAudiometryAnswers = new List<ToneAudiometryAnswer>();
    }
}

