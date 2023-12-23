using BusinessLogic.IModels;
using BusinessLogic.Models;
using UserInterface.Stores;

namespace BusinessLogic.BusinessRules;

public class TestBusinessRules
{

    public static void ValidateTestValues(string testname, TargetAudience targetAudience)
    {
        AssertTestName(testname);
        AssertTargetAudience(targetAudience);
    }
    public static void ValidateToneaudiometryValues(string decibel, string frequency)
    {
        AssertDecibels(decibel);
        AssertFrequency(frequency);
    }
    public static void ValidateTextValues(string testQuestion, bool inputField, bool multipleChoice,List<string> options)
    {
        AssertQuestionType(inputField, multipleChoice,options);
        AssertTestQuestion(testQuestion);
    }

    public static void AssertQuestions<T>(List<T> questions) where T : IQuestion
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
   
    public static int ParseStringToInt(string str)
    {
        int parsedInt;
        bool success = int.TryParse(str, out parsedInt);

        if (success)
            return parsedInt;
        else
            return -1;

    }

    public static void AssertDecibels(string StartingDecibelsString)
    {
        int startingDecibels = ParseStringToInt(StartingDecibelsString);

        if (startingDecibels == -1)
        {
            throw new Exception(ErrorMessageStore.ErrorNotValidInteger);
        }

        if (!IsValidDecibel(startingDecibels))
        {
            throw new Exception(ErrorMessageStore.ErrorStartingDecibels);
        }
    }

    public static void AssertFrequency(string FrequencyString)
    {
        int frequency = ParseStringToInt(FrequencyString);

        if (frequency == -1)
        {
            throw new Exception(ErrorMessageStore.ErrorNotValidInteger);
        }

        if (!IsValidHz(frequency))
        {
            throw new Exception(ErrorMessageStore.ErrorFrequencyLimit);
        }
    }

    public static void AssertTestName(string str)
    {
        if (IsEmptyString(str!))
            throw new Exception(ErrorMessageStore.ErrorTestName);
        else if (ContainsAnyCharacter(str))
            throw new Exception(ErrorMessageStore.ErrorIllegalCharacters);
    }
    public static void AssertTargetAudience(TargetAudience targetAudience)
    {
        if (targetAudience == null || IsEmptyString(targetAudience.Label))
            throw new Exception(ErrorMessageStore.ErrorAudience);

    }

    public static void AssertTestQuestion(string question)
    {
        if (IsEmptyString(question))
            throw new Exception(ErrorMessageStore.ErrorTestQuestion);
        else if (ContainsAnyCharacter(question))
            throw new Exception(ErrorMessageStore.ErrorIllegalCharacters);
    }

    public static void AssertQuestionType(bool inputField, bool multipleChoice, List<string> options)
    {
        if (!inputField && !multipleChoice)
            throw new Exception(ErrorMessageStore.ErrorQuestionAnwserType);
        if (multipleChoice && options.Count < 2)
            throw new Exception(ErrorMessageStore.ErrorMultipleChoiceOptions);
    }

    public static bool ContainsAnyCharacter(string str)
    {
        foreach (char c in str)
        {
            if (ErrorMessageStore.IllegalCharacters.Contains(c))
            {
                return true;
            }
        }
        return false;
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
