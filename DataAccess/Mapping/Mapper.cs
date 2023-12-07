using BusinessLogic.Enums;
using BusinessLogic.IModels;
using BusinessLogic.Models;
using DataAccess.DataTransferObjects;
using System.Data;

namespace DataAccess.Mapping;

public static class Mapper
{
    public static ITest MapToTest(TestDTO entity)
    {
        return new Test
        {
            Id = entity.Id,
            Title = entity.Title,
            Active = entity.Active,
            Employee = MapToEmployee(entity.Employee!),
            TargetAudience = MapToTargetAudience(entity.TargetAudience!),
            TextQuestions = MapToTextQuestions(entity.TextQuestions!),
            ToneAudiometryQuestions = MapToToneAudiometryQuestions(entity.ToneAudiometryQuestions!)
        };
    }
    public static IEmployee MapToEmployee(EmployeeDTO employee)
    {
        string fullName;
        if (string.IsNullOrEmpty(employee.Infix))
        {
            fullName = $"{employee.FirstName} {employee.LastName}";
        }
        else
        {
            fullName = $"{employee.FirstName} {employee.Infix} {employee.LastName}";
        }

        return new Employee
        {
            Id = employee.Id,
            EmployeeNumber = employee.EmployeeNumber,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Infix = employee.Infix,
            Fullname = fullName
        };
    }

    public static TestDTO MapToTestDTO(ITest test)
    {
        return new TestDTO
        {
            Id = test.Id,
            Title = test.Title,
            Active = test.Active,
            Employee = MapToEmployeeDTO(test.Employee),
            EmployeeId = test.Employee.Id,
            TargetAudience = MapToTargetAudienceDTO(test.TargetAudience),
            TargetAudienceId = test.TargetAudience.Id,
            TextQuestions = MapToTextQuestionsDTO(test.TextQuestions, test.Id),
            ToneAudiometryQuestions = MapToToneAudiometryQuestionsDTO(test.ToneAudiometryQuestions, test.Id)
        };
    }
    public static EmployeeDTO MapToEmployeeDTO(IEmployee employee)
    {
        return new EmployeeDTO
        {
            Id = employee.Id,
            EmployeeNumber = employee.EmployeeNumber,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Infix = employee.Infix
        };
    }
    public static TargetAudienceDTO MapToTargetAudienceDTO(ITargetAudience targetAudience)
    {
        return new TargetAudienceDTO
        {
            Id = targetAudience.Id,
            From = targetAudience.From,
            To = targetAudience.To,
            Label = targetAudience.Label,
        };
    }

    public static ICollection<ToneAudiometryQuestionDTO> MapToToneAudiometryQuestionsDTO(List<IToneAudiometryQuestion> audioQuestions, int testId)
    {
        return audioQuestions.Select(audioQuestion => new ToneAudiometryQuestionDTO
        {
            Id = audioQuestion.Id,
            Frequency = audioQuestion.Frequency,
            StartingDecibels = audioQuestion.StartingDecibels,
            QuestionNumber = audioQuestion.QuestionNumber,
            TestId = testId
        }).ToList();
    }

    public static ICollection<TextQuestionDTO> MapToTextQuestionsDTO(List<ITextQuestion> textQuestions, int testId)
    {
        return textQuestions.Select(textQuestion => new TextQuestionDTO
        {
            Id = textQuestion.Id,
            HasInputField = textQuestion.HasInputField,
            IsMultiSelect = textQuestion.IsMultiSelect,
            Options = MapToTextQuestionOptionDTO(textQuestion.Options, textQuestion.Id),
            Question = textQuestion.Question,
            QuestionNumber = textQuestion.QuestionNumber,
            TestId = testId
        }).ToList();
    }

    public static ICollection<TextQuestionOptionDTO> MapToTextQuestionOptionDTO(List<string> options, int questionId)
    {
        return Enumerable.Range(1, options.Count)
                         .Select(i => new TextQuestionOptionDTO
                         {
                             Id = i,
                             Option = options[i - 1],
                             TextQuestionId = questionId
                         })
                         .ToList();
    }

    public static ITargetAudience MapToTargetAudience(TargetAudienceDTO targetAudience)
    {
        return new TargetAudience
        {
            Id = targetAudience.Id,
            From = targetAudience.From,
            Label = targetAudience.Label,
            To = targetAudience.To,
        };
    }

    public static List<ITargetAudience> MapToTargetAudiences(ICollection<TargetAudienceDTO> targetAudiences)
    {
        return targetAudiences.Select(targetAudience => new TargetAudience
        {
            Id = targetAudience.Id,
            From = targetAudience.From,
            Label = targetAudience.Label,
            To = targetAudience.To,
        }).ToList<ITargetAudience>();
    }

    public static ICollection<TargetAudienceDTO> MapToTargetAudiencesDTO(List<ITargetAudience> targetAudiences)
    {
        return targetAudiences.Select(targetAudience => new TargetAudienceDTO
        {
            From = targetAudience.From,
            Label = targetAudience.Label,
            To = targetAudience.To,
        }).ToList();
    }
    private static List<ITextQuestion> MapToTextQuestions(ICollection<TextQuestionDTO> textQuestions)
    {
        return textQuestions.Select(q => new TextQuestion
        {
            Id = q.Id,
            HasInputField = q.HasInputField,
            IsMultiSelect = q.IsMultiSelect,
            Options = q.Options!.Select(o => o.Option).ToList(),
            Question = q.Question,
            QuestionNumber = q.QuestionNumber,
            QuestionType = QuestionType.TextQuestion
        }).ToList<ITextQuestion>();
    }

    private static List<IToneAudiometryQuestion> MapToToneAudiometryQuestions(ICollection<ToneAudiometryQuestionDTO> toneAudiometryQuestions)
    {
        return toneAudiometryQuestions.Select(q => new ToneAudiometryQuestion
        {
            Id = q.Id,
            QuestionNumber = q.QuestionNumber,
            QuestionType = QuestionType.AudioQuestion,
            StartingDecibels = q.StartingDecibels,
            Frequency = q.Frequency,
        }).ToList<IToneAudiometryQuestion>();
    }
}
