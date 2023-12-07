using BusinessLogic.DataTransferObjects;
using BusinessLogic.Enums;
using BusinessLogic.IModels;
using BusinessLogic.Models;

namespace BusinessLogic.Mapping;

public static class Mapper
{
    public static Test MapTest(TestDTO entity)
    {
        return new Test
        {
            Id = entity.Id,
            Title = entity.Title,
            Active = entity.Active,
            Employee = MapEmployee(entity.Employee!),
            TargetAudience = MapTargetAudience(entity.TargetAudience!),
            TextQuestions = MapTextQuestions(entity.TextQuestions!),
            ToneAudiometryQuestions = MapToneAudiometryQuestions(entity.ToneAudiometryQuestions!)
        };
    }
    public static IEmployee MapEmployee(EmployeeDTO employee)
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

    public static ITargetAudience MapTargetAudience(TargetAudienceDTO targetAudience)
    {
        return new TargetAudience
        {
            Id = targetAudience.Id,
            From = targetAudience.From,
            Label = targetAudience.Label,
            To = targetAudience.To,
        };
    }
    public static List<ITargetAudience> MapTargetAudiences(ICollection<TargetAudienceDTO> targetAudiences)
    {
        return targetAudiences.Select(targetAudience => new TargetAudience
        {
            Id = targetAudience.Id,
            From = targetAudience.From,
            Label = targetAudience.Label,
            To = targetAudience.To,
        }).ToList<ITargetAudience>();
    }


    private static List<ITextQuestion> MapTextQuestions(ICollection<TextQuestionDTO> textQuestions)
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

    private static List<IToneAudiometryQuestion> MapToneAudiometryQuestions(ICollection<ToneAudiometryQuestionDTO> toneAudiometryQuestions)
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
