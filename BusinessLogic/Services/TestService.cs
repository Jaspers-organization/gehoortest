using BusinessLogic.IModels;
using BusinessLogic.IRepositories;
using BusinessLogic.Projections;
using System.Collections.ObjectModel;
using BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserInterface.Stores;

namespace BusinessLogic.Services;

public class TestService
{
    private ITestRepository testRepository;

    public TestService(ITestRepository testRepository)
    {
        this.testRepository = testRepository;
    }
    public List<ITest> GetAllTests()
    {
        return testRepository.GetAllTests();
    }
    public ITest? GetTest(int targetAudienceId)
    {
        return testRepository.GetTest(targetAudienceId);
    }
    public ObservableCollection<TestProjection> GetTestsProjectionForAudience(int id)
    {
        return testRepository.GetTestsProjectionForAudience(id);
    }
    public void SaveTest(ITest test)
    {
        testRepository.SaveTest(test);
    }
    public void DeleteTest(ITest test)
    {
        testRepository.DeleteTest(test);
    }
    public void UpdateTest(ITest test)
    {
        testRepository.UpdateTest(test);
    }
    public ITest CreateTest()
    {
       return testRepository.CreateTest();
    }
    public int GetNewHighestQuestionNumber(ITest test, IQuestion questionType)
    {
        switch (questionType)
        {
            case IToneAudiometryQuestion audioQuestion:
                return test.ToneAudiometryQuestions.Any()
                    ? test.ToneAudiometryQuestions.Max(q => q.QuestionNumber) + 1
                    : 1; 
            case ITextQuestion textQuestion:
                return test.TextQuestions.Any()
                    ? test.TextQuestions.Max(q => q.QuestionNumber) + 1
                    : 1; 
            default:
                return 0;
        }
    }

    public int GetQuestionNumberIndex<T>(List<T> questions, int questionNumber) where T : IQuestion
    {
        return questions.FindIndex(q => q.QuestionNumber == questionNumber);
    }
    public List<T> ShiftQuestionNumbers<T>(List<T> questions) where T : IQuestion
    {
        int newNumber = 1;

        foreach (IQuestion question in questions)
        {
            question.QuestionNumber = newNumber;
            newNumber++;
        }
        return questions;
    }
    public List<T> UpdateQuestion<T>(List<T> questions, int questionNumber, T question) where T : IQuestion
    {
        int index = GetQuestionNumberIndex(questions, questionNumber);
        questions[index] = question;
        return questions;
    }
    public List<T> DeleteQuestion<T>(List<T> questions, int questionNumber) where T : IQuestion
    {
        int index = GetQuestionNumberIndex(questions, questionNumber);
        questions.RemoveAt(index);
        questions = ShiftQuestionNumbers(questions);
        return questions;
    }
    public static bool ContatinsInvalidCharacters(string str)
    {
        return str.Contains(ErrorStore.IllegalCharacters);
    }
    public static bool IsValidHz(int hz)
    {
        return hz >= 125 && hz <= 8000;
    }
    public static bool IsValidDecibel(int decibel)
    {
        return decibel <= 0 && decibel > 120;
    }
    public static bool IsEmptyString(string str)
    {
        return string.IsNullOrEmpty(str);
    }
}
