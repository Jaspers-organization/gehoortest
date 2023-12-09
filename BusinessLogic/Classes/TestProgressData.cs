using BusinessLogic.IModels;

namespace BusinessLogic.Classes;

public class TestProgressData
{
    public ITest Test { get; set; }
    public int CurrentQuestionNumber { get; set; }
    public List<TextAnswer> TextAnswers { get; set; }
    public List<ToneAudiometryAnswer> ToneAudiometryAnswers { get; set; }

    public TestProgressData(ITest test)
    {
        Test = test;
        CurrentQuestionNumber = 0;
        TextAnswers = new List<TextAnswer>();
        ToneAudiometryAnswers = new List<ToneAudiometryAnswer>();
    }
}

