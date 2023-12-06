using BusinessLogic.IRepositories;
using BusinessLogic.Projections;
using System.Collections.ObjectModel;
using UserInterface.Stores;
using BusinessLogic.IModels;
using BusinessLogic.Enums;

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
    public ITest GetTest(int targetAudienceId)
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
    /// <summary>
    /// Gets the new highest question number based on the test and question type.
    /// </summary>
    /// <param name="test">The test entity containing questions.</param>
    /// <param name="questionType">The type of question.</param>
    public static int GetNewHighestQuestionNumber(ITest test, QuestionType questionType)
    {
        if (test == null)
        {
            throw new ArgumentNullException("Test or QuestionType cannot be null");
        }

        switch (questionType)
        {
            case QuestionType.TextQuestion:
                return test.ToneAudiometryQuestions.Any()
                    ? test.ToneAudiometryQuestions.Max(q => q.QuestionNumber) + 1
                    : 1;
            case QuestionType.AudioQuestion:
                return test.TextQuestions.Any()
                    ? test.TextQuestions.Max(q => q.QuestionNumber) + 1
                    : 1;
            default:
                return 0;
        }
    }
    /// <summary>
    /// Gets the index of a question based on its number within a list of questions.
    /// </summary>
    /// <typeparam name="T">The type of question.</typeparam>
    /// <param name="questions">The list of questions to search through.</param>
    /// <param name="questionNumber">The number of the question to find.</param>
    /// <returns>The index of the question in the list, or -1 if not found.</returns>
    public static int GetQuestionNumberIndex<T>(List<T> questions, int questionNumber) where T : IQuestion
    {
        AssertQuestions(questions);

        return questions.FindIndex(q => q.QuestionNumber == questionNumber);
    }

    /// <summary>
    /// Shifts the question numbers in a list of questions to consecutive numbers starting from 1.
    /// </summary>
    /// <typeparam name="T">The type of question.</typeparam>
    /// <param name="questions">The list of questions to renumber.</param>
    /// <returns>The updated list of questions with renumbered question numbers.</returns>
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

    /// <summary>
    /// Updates a question in the list based on its number.
    /// </summary>
    /// <typeparam name="T">The type of question.</typeparam>
    /// <param name="questions">The list of questions to update.</param>
    /// <param name="questionNumber">The number of the question to update.</param>
    /// <param name="question">The updated question object.</param>
    /// <returns>The updated list of questions.</returns>
    public static List<T> UpdateQuestion<T>(List<T> questions, int questionNumber, T question) where T : IQuestion
    {
        AssertQuestions(questions);
        int index = GetQuestionNumberIndex(questions, questionNumber);
        if (index != -1)
        {
            questions[index] = question;
        }
        return questions;
    }

    /// <summary>
    /// Ensures the validity of the provided list of questions.
    /// </summary>
    /// <typeparam name="T">The type of question.</typeparam>
    /// <param name="questions">The list of questions to validate.</param>
    private static void AssertQuestions<T>(List<T> questions) where T: IQuestion 
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

    /// <summary>
    /// Deletes a question from the list based on its number and shifts the question numbers.
    /// </summary>
    /// <typeparam name="T">The type of question.</typeparam>
    /// <param name="questions">The list of questions to delete from.</param>
    /// <param name="questionNumber">The number of the question to delete.</param>
    /// <returns>The updated list of questions after deletion and renumbering.</returns>
    public static List<T> DeleteQuestion<T>(List<T> questions, int questionNumber) where T : IQuestion
    {
        AssertQuestions(questions);

        int index = GetQuestionNumberIndex(questions, questionNumber);
        if (index != -1)
        {
            questions.RemoveAt(index);
            questions = ShiftQuestionNumbers(questions);
        }
        return questions;
    }

    /// <summary>
    /// Checks whether the provided string contains invalid characters.
    /// </summary>
    /// <param name="str">The string to check for invalid characters.</param>
    /// <returns>True if the string contains invalid characters; otherwise, false.</returns>
    public  static bool ContatinsInvalidCharacters(string str)
    {
        return str.Contains(ErrorStore.IllegalCharacters);
    }

    /// <summary>
    /// Checks if the provided hertz value is within a valid range.
    /// </summary>
    /// <param name="hz">The hertz value to validate.</param>
    /// <returns>True if the hertz value is within a valid range; otherwise, false.</returns>
    public static bool IsValidHz(int hz)
    {
        return hz >= 125 && hz <= 8000;
    }

    /// <summary>
    /// Checks if the provided decibel value is within a valid range.
    /// </summary>
    /// <param name="decibel">The decibel value to validate.</param>
    /// <returns>True if the decibel value is within a valid range; otherwise, false.</returns>
    public static bool IsValidDecibel(int decibel)
    {
        return decibel <= 0 && decibel > 120;
    }

    /// <summary>
    /// Checks if the provided string is empty or null.
    /// </summary>
    /// <param name="str">The string to check.</param>
    /// <returns>True if the string is empty or null; otherwise, false.</returns>
    public static bool IsEmptyString(string str)
    {
        return string.IsNullOrEmpty(str);
    }
}
