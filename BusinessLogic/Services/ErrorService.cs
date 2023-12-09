using BusinessLogic.IModels;
using BusinessLogic.Models;

namespace UserInterface.Stores;

public class ErrorService
{
    public const string IllegalCharacters = "!@#$%^&*()[]{};:'`|<>";
    public const string ErrorTestName = "Het invoerveld is leeg.\n Geef de test een naam a.u.b.";
    public const string ErrorAudience = "De doelgroep is leeg. \n Kies een doelgroep a.u.b.";
    public const string ErrorTestQuestion = "De vraag is leeg.\n Vul alsjeblieft een vraag in.";
    public const string ErrorOptions = "De doelgroep is leeg. \n Kies een doelgroep a.u.b.";
    public const string ErrorIllegalCharacters = $"Er zijn karakters aanwezig die niet toegestaan zijn. \n Deze karakters zijn: {IllegalCharacters}";
    public const string ErrorFrequencyLimit = "De opgegeven frequentie valt buiten de grenzen die toegestaan is. \n De waarde moet tussen 125 HZ en 8000 HZ vallen.";
    public const string ErrorStartingDecibels = "De opgegeven start volume valt buiten de grenzen die toegestaan is. \n De waarde moet tussen 0 en 120 decibel vallen.";
    public const string ErrorNotValidInteger = "Er mogen alleen hele getallen ingevoerd worden.";
    public const string ErrorEmpty = "Alle velden moeten ingevuld worden.";
    public const string ErrorQuestionAnwserType = "Een van de twee antwoord types moet actief zijn.";
    public const string ErrorMultipleChoiceOptions = "Er moeten minstens 2 opties beschikbaar zijn voor een meerkeuze vraag.";

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
                return ErrorNotValidInteger;
            }

            if (!IsValidDecibel(startingDecibels))
            {
                return ErrorStartingDecibels;
            }

            return string.Empty;
        }
        catch (Exception ex)
        {
            return "Er is wat misgegaan";
        }
    }

    public static string ValidateFrequency(string FrequencyString)
    {
        try
        {
            int frequency = ParseStringToInt(FrequencyString);

            if (frequency == -1)
            {
                return ErrorNotValidInteger;
            }

            if (!IsValidHz(frequency))
            {
                return ErrorFrequencyLimit;
            }

            return string.Empty;
        }
        catch (Exception ex)
        {
            return "Er is wat misgegaan";
        }
    }

    public static string ValidateTestName(string str)
    {
        if (IsEmptyString(str!))
            return ErrorTestName;
        else if (str.Contains(IllegalCharacters))
            return ErrorIllegalCharacters;
        return string.Empty;
    }
    public static string ValidateAudience(TargetAudience targetAudience)
    {
        if (targetAudience == null || IsEmptyString(targetAudience.Label))
            return ErrorAudience;
        return string.Empty;
    }

    public static string ValidateTestQuestion(string question)
    {
        if (IsEmptyString(question))
            return ErrorTestQuestion;
        else if (ContatinsInvalidCharacters(question))
            return ErrorIllegalCharacters;
        return string.Empty;
    }
    public static string ValidateQuestionType(bool inputField, bool multipleChoice, List<string> options)
    {
        if (!inputField && !multipleChoice)
            return ErrorQuestionAnwserType;
        if (multipleChoice && options.Count < 2)
            return ErrorMultipleChoiceOptions;
        return string.Empty;
    }

    public static bool ContatinsInvalidCharacters(string str)
    {
        return str.Contains(IllegalCharacters);
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
