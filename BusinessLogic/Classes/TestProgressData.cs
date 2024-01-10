
using BusinessLogic.Enums;
using BusinessLogic.Models;
using System.Diagnostics;

namespace BusinessLogic.Classes;

public class TestProgressData
{
    public Test Test { get; set; }
    public int CurrentQuestionNumber { get; set; }
    public Ear CurrentEar { get; set; }
    public int Decibel { get; set; }
    public List<(bool answer, Ear ear, ToneAudiometryQuestion question, int decibel)> CurrentToneAudioMetryAnswers { get; set; }
    public List<TextQuestionResult> TextQuestionResults { get; set; }
    public List<ToneAudiometryQuestionResult> ToneAudiometryQuestionResults { get; set; }


    public List<TextAnswer> TextAnswers { get; set; }
    public List<ToneAudiometryAnswer> ToneAudiometryAnswers { get; set; }
    public TestProgressData(Test test)
    {
        Test = test;
        CurrentQuestionNumber = 0;
        Decibel = 0;
        TextAnswers = new List<TextAnswer>();
        ToneAudiometryAnswers = new List<ToneAudiometryAnswer>();
        CurrentToneAudioMetryAnswers = new List<(bool, Ear, ToneAudiometryQuestion, int)>();
        TextQuestionResults = new List<TextQuestionResult>();
        ToneAudiometryQuestionResults = new List<ToneAudiometryQuestionResult>();
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

        TextQuestionResults.Add(givenAnswer);
    }

    public void Add(bool answer, Ear ear, ToneAudiometryQuestion question)
    {
        CurrentToneAudioMetryAnswers.Add((answer, ear, question, Decibel));
    }

    public TextQuestion? GetNextTextQuestion()
    {
        int maxNumber = Test.TextQuestions.Max(x => x.QuestionNumber);
        if(CurrentQuestionNumber >= maxNumber)
        {
            CurrentQuestionNumber = 0;
            return null;
        }
        else
        {
            CurrentQuestionNumber++;
            return Test.TextQuestions.First(x => x.QuestionNumber == CurrentQuestionNumber);
        }
    }

    public ToneAudiometryQuestion? GetNextToneAudiometryQuestion()
    {
        if (IsFrequencyDone())
        {
            //frequence is done for both ears, move onto the  next question
            SaveToneAudiometryQuestionResult();
            ResetToneAudiometryTest();

            int maxNumber = Test.ToneAudiometryQuestions.Max(x => x.QuestionNumber);
            if (CurrentQuestionNumber >= maxNumber)
            {
                CurrentQuestionNumber = 0;
                return null;
            }
            else
            {
                CurrentQuestionNumber++;
                return Test.ToneAudiometryQuestions.First(x => x.QuestionNumber == CurrentQuestionNumber);
            }
        }
        else
        {
            Random random = new Random();
            bool isLeftEar = random.Next(0, 2) == 1; 
            CurrentEar = isLeftEar ? Ear.Left : Ear.Right;

            //if the selected ear hasnt been tested yet, it means it's the decibel = startingdecibel.
            //There is no need for GetNextDecibels
            var answersOfSelectedEarr = CurrentToneAudioMetryAnswers.Where(x => x.Item2 == CurrentEar).ToList();
            if (answersOfSelectedEarr.Count()<= 0)
            {
                Decibel = Test.ToneAudiometryQuestions.First(x => x.QuestionNumber == CurrentQuestionNumber).StartingDecibels;
            }
            else
            {
                int? returnedDecibel = GetNextDecibels(CurrentEar);
                if (returnedDecibel != null)
                {
                    //not done testing with the selected ear ear
                    //check if there is a last answer
                    var answersOfSelectedEar = CurrentToneAudioMetryAnswers.Where(x => x.Item2 == CurrentEar).ToList();
                    var lastAnswer = answersOfSelectedEar[answersOfSelectedEar.Count() - 1];
                    Decibel = lastAnswer.Item4 + (int)returnedDecibel;
                }
                else if (returnedDecibel == null)
                {
                    //done with testing of selected ear so moving onto the other ear
                    CurrentEar = (CurrentEar == Ear.Left) ? Ear.Right : Ear.Left;
                    returnedDecibel = GetNextDecibels(CurrentEar);
                    if (returnedDecibel != null)
                    {
                        //check if there is a last answer
                        var answersOfSelectedEar = CurrentToneAudioMetryAnswers.Where(x => x.Item2 == CurrentEar).ToList();
                        var lastAnswer = answersOfSelectedEar[answersOfSelectedEar.Count() - 1];
                        Decibel = lastAnswer.Item4 + (int)returnedDecibel;
                    }
                }
            }

            return Test.ToneAudiometryQuestions.First(x => x.QuestionNumber == CurrentQuestionNumber);
        }
    }

    private bool IsFrequencyDone()
    {
        //if the list.count() <= 0 for either ear then there is no use to call GetNextDecibel.
        //It means the test hasnt even begun yet and therefore IsFrequencyDone is false.
        var leftEarAnswer = CurrentToneAudioMetryAnswers.Where(x => x.Item2 == Ear.Left).ToList();
        var rightEarAnswer = CurrentToneAudioMetryAnswers.Where(x => x.Item2 == Ear.Right).ToList();
        int? resultLeft;
        int? resultRight;
        
        if (leftEarAnswer.Count() > 0)
        {
            resultLeft = GetNextDecibels(Ear.Left);
        }
        else
        {
            return false;
        }

        if (rightEarAnswer.Count() > 0)
        {
            resultRight = GetNextDecibels(Ear.Right);
        }
        else
        {
            return false;
        }

        return resultLeft == null && resultRight == null;
    }

    private int? GetNextDecibels(Ear ear)
    {
        var answersOfSelectedEar = CurrentToneAudioMetryAnswers.Where(x => x.ear == ear).ToList();
        var lastAnswer = answersOfSelectedEar[answersOfSelectedEar.Count() - 1];
        //last answer was true
        if (lastAnswer.answer)
        {
            bool falseGivenAnswer = false;
            //check if any of the previous answers were false (opposite of the last given answer)
            foreach (var item in answersOfSelectedEar)
            {
                if (item.answer != lastAnswer.answer)
                {
                    falseGivenAnswer = true;
                    break;
                }
            }

            if (falseGivenAnswer)
            {
                //any false answers
                int? modulus = lastAnswer.decibel % 2;
                if (lastAnswer.decibel == 0 || modulus != 0)
                {
                    return null;
                }
                else
                {
                    return -5;
                }
            }
            else
            {
                //no false answers
                return -10;
            }
        }
        else
        {
            //last answer was false
            bool trueGivenAnswer = false;
            //check if any of the previous answers were true
            foreach (var item in answersOfSelectedEar)
            {
                if (item.answer != lastAnswer.answer)
                {
                    trueGivenAnswer = true;
                    break;
                }
            }

            if (trueGivenAnswer)
            {
                //any true answers given
                int? modulus = lastAnswer.decibel % 2;
                if (lastAnswer.decibel == 0 || modulus != 0)
                {
                    return null;
                }
                else
                {
                    return 5;
                }
            }
            else
            {
                //no true answers given
                return 10;
            }
        }
    }
    private void SaveToneAudiometryQuestionResult()
    {
        var lastAnswersRightEar = CurrentToneAudioMetryAnswers.Where(x => x.ear == Ear.Right).ToList().Last();
        ToneAudiometryQuestionResult finalAnswerRightEar = new ToneAudiometryQuestionResult()
        {
            Id = Guid.NewGuid(),
            Frequency = lastAnswersRightEar.question.Frequency,
            StartingDecibels = lastAnswersRightEar.question.StartingDecibels,
            LowestDecibels = Decibel,
            Ear = Ear.Right
        };

        ToneAudiometryQuestionResults.Add(finalAnswerRightEar);

        var lastAnswersLeftEar = CurrentToneAudioMetryAnswers.Where(x => x.ear == Ear.Left).ToList().Last();
        ToneAudiometryQuestionResult finalAnswerLeftEar = new ToneAudiometryQuestionResult()
        {
            Id = Guid.NewGuid(),
            Frequency = lastAnswersLeftEar.Item3.Frequency,
            StartingDecibels = lastAnswersLeftEar.Item3.StartingDecibels,
            LowestDecibels = Decibel,
            Ear = Ear.Right
        };
       
        ToneAudiometryQuestionResults.Add(finalAnswerLeftEar);
    }

    private void ResetToneAudiometryTest()
    {
        CurrentToneAudioMetryAnswers.Clear();
        Decibel = 0;
    }
}

