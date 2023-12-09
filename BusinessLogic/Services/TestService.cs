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

    public Test? GetTestByTargetAudienceIdAndActive(Guid targetAudienceId) => testRepository.GetTestByTargetAudienceIdAndActive(targetAudienceId);

    public void SaveTest(Test test) => testRepository.SaveTest(test);

    public void DeleteTest(Test test) => testRepository.DeleteTest(test);

    public void UpdateTest(Test test) => testRepository.UpdateTest(test);

    public Test CreateTest()
    {
        test = testRepository.CreateTest();

        return test;
    }
    public void SetTest(Test test)
    {
        this.test = test;
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
            Test? activeTest = testRepository.GetTestByTargetAudienceIdAndActive(test.TargetAudienceId);
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
    public static int ParseStringToInt(string str)
    {
        int parsedInt;
        bool success = int.TryParse(str, out parsedInt);

        if (success)
            return parsedInt;
        else
            return -1;

    }
    public static string ValidateDecibels(string StartingDecibelsString)
    {
        try
        {
            int startingDecibels = ParseStringToInt(StartingDecibelsString);

            if (startingDecibels == -1)
            {
                return ErrorStore.ErrorNotValidInteger;
            }

            if (!TestService.IsValidDecibel(startingDecibels))
            {
                return ErrorStore.ErrorStartingDecibels;
            }

            return string.Empty; // No error, return empty string
        }
        catch (Exception ex)
        {
            // Log or handle the exception as needed
            return "Er is wat misgegaan"; // Error message for exception
        }
    }

    public static string ValidateFrequency(string FrequencyString)
    {
        try
        {
            int frequency = ParseStringToInt(FrequencyString);

            if (frequency == -1)
            {
                return ErrorStore.ErrorNotValidInteger;
            }

            if (!TestService.IsValidHz(frequency))
            {
                return ErrorStore.ErrorFrequencyLimit;
            }

            return string.Empty; // No error, return empty string
        }
        catch (Exception ex)
        {
            // Log or handle the exception as needed
            return "Er is wat misgegaan"; // Error message for exception
        }
    }
      
    public static string ValidateInput(string columnName, params object[] values)
    {
        string validationMessage = string.Empty;

        switch (columnName)
        {
            case "TestName":
                validationMessage = ValidateTestName((string)values[0]);
                break;
            case "Audience":
                validationMessage = ValidateAudience((TargetAudience)values[0]);
                break;
            case "TestQuestion":
                validationMessage = ValidateTestQuestion((string)values[0]);
                break;
            case "MultipleChoice":
            case "HasInputField":
                validationMessage = ValidateQuestionType((bool)values[0], (bool)values[1], (List<string>)values[2]);
                break;
            case "Frequency":
                validationMessage = ValidateFrequency((string)values[0]);
                break;
            case "StartingDecibelsString":
                validationMessage = ValidateDecibels((string)values[0]);
                break;
            default:
                break;
        }

        return validationMessage;
    }


    public static string ValidateTestName(string str)
    {
        // Validate if the test name is empty or contains illegal characters
        if (TestService.IsEmptyString(str!))
            return ErrorStore.ErrorTestName;
        else if (str.Contains(ErrorStore.IllegalCharacters))
            return ErrorStore.ErrorIllegalCharacters;
        return string.Empty;
    }
    public static string ValidateAudience(TargetAudience targetAudience)
    {
        // Validate if the audience is correctly selected
        if (targetAudience == null || TestService.IsEmptyString(targetAudience.Label))
            return ErrorStore.ErrorAudience;
        return string.Empty;
    }

    public static string ValidateTestQuestion(string question)
    {
        if (IsEmptyString(question))
            return ErrorStore.ErrorTestQuestion;
        else if (ContatinsInvalidCharacters(question))
            return ErrorStore.ErrorIllegalCharacters;
        return string.Empty;
    }
    public static string ValidateQuestionType(bool inputField, bool multipleChoice, List<string> options)
    {
        if (!inputField && !multipleChoice)
            return ErrorStore.ErrorQuestionAnwserType;
        if (multipleChoice && options.Count < 2)
            return ErrorStore.ErrorMultipleChoiceOptions;
        return string.Empty;
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
