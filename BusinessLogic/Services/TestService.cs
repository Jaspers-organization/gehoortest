using BusinessLogic.Projections;
using BusinessLogic.Enums;
using BusinessLogic.Models;
using BusinessLogic.BusinessRules;
using BusinessLogic.Interfaces.Repositories;
using BusinessLogic.Interfaces.Models;

namespace BusinessLogic.Services;

public class TestService
{
    private Test test;

    private ITestRepository testRepository;

    public TestService(ITestRepository testRepository) => this.testRepository = testRepository;

    #region Test Retrieval
    public List<Test> GetAllTests() => testRepository.GetAllTests();

    public Test GetTestById(Guid id) => testRepository.GetTestById(id);

    public List<TestProjection>? GetTestProjectionsByTargetAudienceId(Guid id) {
        if(id == Guid.Empty) return testRepository.GetTestProjectionsByNoTargetAudience();

       return testRepository.GetTestProjectionsByTargetAudienceId(id);
    }

    public Test? GetTestByTargetAudienceIdAndActive(Guid targetAudienceId) => testRepository.GetTestByTargetAudienceIdAndActive(targetAudienceId);
    #endregion

    #region CRUD Test
    public Test CreateTest()
    {
        test = testRepository.CreateTest();
        return test;
    }

    public void SaveTest(Test test) => testRepository.SaveTest(test);

    public void DeleteTest(Test test) => testRepository.DeleteTest(test);

    public void UpdateTest(Test test) => testRepository.UpdateTest(test);
    #endregion

    public void SetTest(Test test) => this.test = test;


    public void ProcessTest(Test test, bool newTest, Guid initalId)
    {
        ValidateTestAgainstBusinessRules(test, newTest, initalId);

        if (newTest)
            SaveTest(test);
        else
        {
            UpdateTest(test);
        }
    }

    private void ValidateTestAgainstBusinessRules(Test test, bool newTest, Guid initalId)
    {
        TestBusinessRules.ValidateTestValues(test.Title, test.TargetAudience);
        if(!newTest) CheckTargetAudience(test.TargetAudience.Id, initalId);
    }
    private void CheckTargetAudience(Guid id, Guid initialId)
    {
        if (TargetAudienceChanged(id, initialId))
            test.Active = false;
    }
  
    public static bool TargetAudienceChanged(Guid currentTargetAudienceId, Guid initalTargetAudienceId)
    {

        return currentTargetAudienceId != initalTargetAudienceId;
    }

    #region ???? not sure yet TODO
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
    #endregion

    #region Questions
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
    #endregion

    #region Test Manipulation 
    public void ToggleActiveStatus(Guid id)
    {
        Test? test = GetTestById(id);
        if (test == null) return;

        if (!test.Active)
        {
            Test? activeTest = testRepository.GetTestByTargetAudienceIdAndActive(test.TargetAudienceId);
            if (activeTest != null)
            {
                activeTest.Active = false;
                testRepository.UpdateTest(activeTest);
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
        TestBusinessRules.AssertQuestions(questions);

        return questions.FindIndex(q => q.QuestionNumber == questionNumber);
    }

    public static List<T> ShiftQuestionNumbers<T>(List<T> questions) where T : IQuestion
    {
        TestBusinessRules.AssertQuestions(questions);
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
        TestBusinessRules.AssertQuestions(questions);
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
            case ToneAudiometryQuestion:
                test.ToneAudiometryQuestions = questions.Cast<ToneAudiometryQuestion>().ToList(); ;
                break;
        }
        return questions;
    }

    public List<T> DeleteQuestion<T>(List<T> questions, int questionNumber) where T : IQuestion
    {
        TestBusinessRules.AssertQuestions(questions);

        int index = GetQuestionNumberIndex(questions, questionNumber);
        if (index != -1)
        {
            if (index == 0 && questions.Count == 0)
                return questions;

            switch (questions[index])
            {
                case TextQuestion:
                    test.TextQuestions.ToList().RemoveAt(index);
                    break;
                case ToneAudiometryQuestion:
                    test.ToneAudiometryQuestions.ToList().RemoveAt(index);
                    break;
            }

            questions.RemoveAt(index);
            questions = ShiftQuestionNumbers(questions);
        }

        return questions;
    }
    #endregion
}
