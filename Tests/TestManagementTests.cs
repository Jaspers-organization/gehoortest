﻿using BusinessLogic.Enums;
using BusinessLogic.IModels;
using BusinessLogic.Models;
using BusinessLogic.Services;

namespace Tests;

public class TestManagementTests
{
    private Test test;
    public TestManagementTests()
    {
        test = new Test
        {
            Id = new Guid(),
            TargetAudience = new TargetAudience { Id = new Guid(), From = 0, To = 18, Label = "-18" },
            Active = false,
            Title = "Test test",
            TextQuestions = new List<TextQuestion>(),
            ToneAudiometryQuestions = new List<ToneAudiometryQuestion>(),
            Employee = new Employee { Id = new Guid(), EmployeeNumber = "123456789", FirstName = "Dinny", Infix = "van", LastName = "Huizen" }
        };
    }

    //[Theory]
    //[InlineData(0, 1, QuestionType.AudioQuestion)]
    //[InlineData(2, 3, QuestionType.AudioQuestion)]
    //[InlineData(56, 57, QuestionType.AudioQuestion)]
    //[InlineData(0, 1, QuestionType.TextQuestion)]
    //[InlineData(2, 3, QuestionType.TextQuestion)]
    //[InlineData(56, 57, QuestionType.TextQuestion)]
    //public void GetNewHighestQuestionNumber_ReturnsNextNumber_ForAudioQuestions(int existingQuestionsCount, int expectedNextNumber, QuestionType type)
    //{
    //    switch (type)
    //    {
    //        case QuestionType.AudioQuestion:
    //            for (var i = 0; i < existingQuestionsCount; i++)
    //            {
    //                test.ToneAudiometryQuestions.Add(new ToneAudiometryQuestion { Id = i, Frequency = 1000 + i * 1000, StartingDecibels = 30 + i * 5, QuestionNumber = i + 1 });
    //            }
    //            break;
    //        case QuestionType.TextQuestion:

    //            for (var i = 0; i < existingQuestionsCount; i++)
    //            {
    //                test.TextQuestions.Add(new TextQuestion { Id = i, QuestionNumber = i + 1, HasInputField = false, IsMultiSelect = false, Options = new List<string>(), Question = "test" });
    //            }
    //            break;
    //        default
    //            :
    //            return;
    //    }
        
    //    var result = TestService.GetNewHighestQuestionNumber(test, type);

    //    Assert.Equal(expectedNextNumber, result);
    //}

    [Theory]
    [InlineData(QuestionType.AudioQuestion)]
    [InlineData(QuestionType.TextQuestion)]
    public void GetNewHighestQuestionNumber_ThrowsArgumentNullException_WhenTestIsNull(QuestionType type)
    {
        Test nullTest = null;
        var questionType = type;
        Assert.Throws<ArgumentNullException>(() => TestService.GetNewHighestQuestionNumber(nullTest, questionType));
    }

    [Theory]
    [InlineData("This is a valid string", false)]
    [InlineData("ContainsInvalidCharacters!", false)]
    [InlineData("!@#$%^&*()[]{};:'`|<>", true)]
    public void ContainsInvalidCharacters_ReturnsValidString(string str, bool expectedResult)
    {
        bool actualResult = TestService.ContatinsInvalidCharacters(str);

        Assert.Equal(expectedResult, actualResult);
    }

    [Theory]
    [InlineData(124, false)]
    [InlineData(125, true)]
    [InlineData(8000, true)]
    [InlineData(8001, false)]
    public void IsValidHz_ReturnsExpectedResult(int hz, bool expectedResult)
    {
        bool actualResult = TestService.IsValidHz(hz);

        Assert.Equal(expectedResult, actualResult);
    }

    [Theory]
    [InlineData(-1, false)]
    [InlineData(0, true)]
    [InlineData(1, true)]
    [InlineData(120, true)]
    [InlineData(121, false)]
    public void IsValidDecibel_ReturnsExpectedResult(int decibel, bool expectedResult)
    {
        bool actualResult = TestService.IsValidDecibel(decibel);

        Assert.Equal(expectedResult, actualResult);
    }

    [Theory]
    [InlineData(null, true)]
    [InlineData("", true)]
    [InlineData("ValidString", false)]
    public void IsEmptyString_ReturnsExpectedResult(string str, bool expectedResult)
    {
        bool actualResult = TestService.IsEmptyString(str);

        Assert.Equal(expectedResult, actualResult);
    }
}
