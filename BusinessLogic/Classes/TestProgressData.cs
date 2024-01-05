
using BusinessLogic.Enums;
using BusinessLogic.Models;

namespace BusinessLogic.Classes;

public class TestProgressData
{
    public Test Test { get; set; }
    public int CurrentQuestionNumber { get; set; }
    public List<string> CurrentToneAudioMetryAnswers { get; set; }
    public List<TextQuestionResult> TextQuestionResults { get; set; }
    public List<ToneAudiometryQuestionResult> ToneAudiometryQuestionResults { get; set; }


    public List<TextAnswer> TextAnswers { get; set; }
    public List<ToneAudiometryAnswer> ToneAudiometryAnswers { get; set; }
    public TestProgressData(Test test)
    {
        Test = test;
        CurrentQuestionNumber = 0;
        TextAnswers = new List<TextAnswer>();
        ToneAudiometryAnswers = new List<ToneAudiometryAnswer>();
    }

    public void Add(string answer, TextQuestion question)
    {
        TextQuestionResult givenAnswer = new TextQuestionResult();
        givenAnswer.Question = question.QuestionNumber;
        
        ICollection<TextQuestionOptionResult> resultList = new List<TextQuestionOptionResult>();
        foreach (TextQuestionOption textQOption in question.Options)
        {
            TextQuestionOptionResult option = new TextQuestionOptionResult();
            option.Id = textQOption.Id;
            option.Option = textQOption.Option;
            resultList.Add(option);
        }
        givenAnswer.Options = resultList;


        ICollection<TextQuestionAnswerResult> answerList = new List<TextQuestionAnswerResult>();
        TextQuestionAnswerResult textQAnswerResult = new TextQuestionAnswerResult();
        textQAnswerResult.Id = Guid.NewGuid();
        textQAnswerResult.Answer = answer;
        answerList.Add(textQAnswerResult);
        givenAnswer.Answers = answerList;   
    }

    public void Add(bool answer, Ear ear, ToneAudiometryQuestion question)
    {

    }

    //public TextQuestion? GetNextTextQuestion()
    //{

    //}

    //public ToneAudiometryQuestion? GetNextTOneAudiometryQuestion()
    //{

    //}

    //private bool IsFrequencyDone()
    //{

    //}

    //private int? GetNExtDecibels(Ear ear)
    //{

    //}
    private void SaveToneAudiometryQuestionResult()
    {

    }

    private void ResetToneAudiometryTest()
    {

    }
}

