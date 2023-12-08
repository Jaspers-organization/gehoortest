using BusinessLogic.IRepositories;
using BusinessLogic.Projections;
using UserInterface.Stores;
using BusinessLogic.IModels;
using BusinessLogic.Enums;
using BusinessLogic.Models;
using BusinessLogic.Classes;

namespace BusinessLogic.Services;

public class TestService
{
    private Test test;

    private ITestRepository testRepository;

    public TestService(ITestRepository testRepository) => this.testRepository = testRepository;

    public List<Test> GetAllTests() => testRepository.GetAllTests();

    public Test GetTestById(Guid id) => testRepository.GetTestById(id);

    public List<TestProjection>? GetTestProjectionsByTargetAudienceId(Guid id) => testRepository.GetTestProjectionsByTargetAudienceId(id);

    public Test? GetTestByTargetAudienceId(Guid targetAudienceId) => testRepository.GetTestByTargetAudienceId(targetAudienceId);

    public void SaveTest(Test test) => testRepository.SaveTest(test);

    public void DeleteTest(Test test) => testRepository.DeleteTest(test);

    public void UpdateTest(Test test) => testRepository.UpdateTest(test);

    public Test CreateTest()
    {
        test = testRepository.CreateTest();
        return test;
    }

    public TextQuestionOption ConvertStringToQuestionOption(string text, Guid textQuestionId)
    {
        return new TextQuestionOption { Id = new Guid(), Option = text, TextQuestionId = textQuestionId };
    }

    public string ConvertQuestionOptionToString(TextQuestionOption questionOption)
    {
        return questionOption.Option;
    }

    public List<TextQuestionOption> ConvertStringsToQuestionOptions(List<string> texts, Guid textQuestionId)
    {
        return texts.Select(text => ConvertStringToQuestionOption(text, textQuestionId)).ToList();
    }

    public List<string> ConvertQuestionOptionsToStrings(List<TextQuestionOption> questionOptions)
    {
        return questionOptions.Select(ConvertQuestionOptionToString).ToList();
    }

    public void SaveOrUpdateTest(Test test, bool newTest)
    {
        if (newTest)
            SaveTest(test);
        else
            UpdateTest(test);
    }
    public ToneAudiometryQuestion CreateToneAudiometryQuestion()
    {
        ToneAudiometryQuestion audioQuestion = new ToneAudiometryQuestion { Id = new Guid(), QuestionType = QuestionType.AudioQuestion };
        audioQuestion.QuestionNumber = GetNewHighestQuestionNumber(test, QuestionType.AudioQuestion);
        return audioQuestion;
    }
    public TextQuestion CreateTextQuestion()
    {
        TextQuestion textQuestion = new TextQuestion
        {
            Id = new Guid(),
            Options = new List<TextQuestionOption>(),
            QuestionType = QuestionType.TextQuestion
        };
        textQuestion.QuestionNumber = GetNewHighestQuestionNumber(test, QuestionType.TextQuestion);
        return textQuestion;
    }

    public List<TextQuestion> AddTextQuestion(TextQuestion textQuestion)
    {
        test.TextQuestions.Add(textQuestion);
        return test.TextQuestions.ToList();
    }
    public List<ToneAudiometryQuestion> AddToneAudiometryQuestion(ToneAudiometryQuestion audioQuestion)
    {
        test.ToneAudiometryQuestions.Add(audioQuestion);
        return test.ToneAudiometryQuestions.ToList();
    }

    public void ToggleActiveStatus(Guid id)
    {
        Test? test = GetTestById(id);
        if (test == null) return;


        if (!test.Active)
        {
            Test? activeTest = testRepository.GetActiveTest();
            if (activeTest != null)
            {
                activeTest.Active = false;
                testRepository.UpdateTest(test);
            }
        }
        test.Active = !test.Active;
        testRepository.UpdateTest(test);
    }

    public static int GetNewHighestQuestionNumber(Test test, QuestionType questionType)
    {
        if (test == null || test.TextQuestions == null || test.ToneAudiometryQuestions == null)
        {
            throw new ArgumentNullException("Test, Textquestios, audioquestions or QuestionType cannot be null");
        }

        switch (questionType)
        {
            case QuestionType.AudioQuestion:
                return test.ToneAudiometryQuestions.Any()
                    ? test.ToneAudiometryQuestions.Max(q => q.QuestionNumber) + 1
                    : 1;
            case QuestionType.TextQuestion:
                return test.TextQuestions.Any()
                    ? test.TextQuestions.Max(q => q.QuestionNumber) + 1
                    : 1;
            default:
                return 0;
        }
    }

    public static int GetQuestionNumberIndex<T>(List<T> questions, int questionNumber) where T : IQuestion
    {
        AssertQuestions(questions);

        return questions.FindIndex(q => q.QuestionNumber == questionNumber);
    }

    public static List<T> ShiftQuestionNumbers<T>(List<T> questions) where T : IQuestion
    {
        AssertQuestions(questions);
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
        AssertQuestions(questions);
        int index = GetQuestionNumberIndex(questions, questionNumber);
        if (index != -1)
        {
            questions[index] = question;
        }

        switch (question)
        {
            case TextQuestion:
                test.TextQuestions = questions.Cast<TextQuestion>().ToList(); ;
                break;
            case AudiometryQuestion:
                test.ToneAudiometryQuestions = questions.Cast<ToneAudiometryQuestion>().ToList(); ;
                break;
        }

        return questions;
    }

    private static void AssertQuestions<T>(List<T> questions) where T : IQuestion
    {
        if (questions == null)
        {
            throw new ArgumentNullException(nameof(questions), "Questions list cannot be null");
        }

        foreach (var question in questions)
        {
            if (question == null)
            {
                throw new ArgumentNullException(nameof(question), "Individual question in the list cannot be null");
            }

            if (question.QuestionNumber < 0)
            {
                throw new ArgumentException("Invalid QuestionNumber found in the list");
            }
        }
    }

    public List<T> DeleteQuestion<T>(List<T> questions, int questionNumber) where T : IQuestion
    {
        AssertQuestions(questions);

        int index = GetQuestionNumberIndex(questions, questionNumber);
        if (index != -1)
        {
            questions.RemoveAt(index);
            questions = ShiftQuestionNumbers(questions);
            switch (questions[index])
            {
                case TextQuestion:
                    test.TextQuestions.ToList().RemoveAt(index);
                    break;
                case AudiometryQuestion:
                    test.ToneAudiometryQuestions.ToList().RemoveAt(index);
                    break;
            }
        }

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
        return decibel >= 0 && decibel <= 120;
    }

    public static bool IsEmptyString(string str)
    {
        return string.IsNullOrEmpty(str);
    }
}
