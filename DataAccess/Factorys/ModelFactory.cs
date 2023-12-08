using BusinessLogic.Enums;
using BusinessLogic.IModels;
using BusinessLogic.Models;
using DataAccess.DataTransferObjects;
using System.Data;

namespace DataAccess.Mapping;

public static class ModelFactory
{
    public static ITest Create(TestDTO entity)
    {
        return new Test
        {
            Id = entity.Id,
            Title = entity.Title,
            Active = entity.Active,
            Employee = Create(entity.Employee!),
            TargetAudience = Create(entity.TargetAudience!),
            TextQuestions = Create(entity.TextQuestions!),
            ToneAudiometryQuestions = Create(entity.ToneAudiometryQuestions!)
        };
    }
    public static IEmployee Create(EmployeeDTO employee)
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
    public static ITargetAudience Create(TargetAudienceDTO targetAudience)
    {
        return new TargetAudience
        {
            Id = targetAudience.Id,
            From = targetAudience.From,
            Label = targetAudience.Label,
            To = targetAudience.To,
        };
    }
    public static List<ITargetAudience> Create(ICollection<TargetAudienceDTO> targetAudiences)
    {
        return targetAudiences.Select(targetAudience => new TargetAudience
        {
            Id = targetAudience.Id,
            From = targetAudience.From,
            Label = targetAudience.Label,
            To = targetAudience.To,
        }).ToList<ITargetAudience>();
    }
    private static List<ITextQuestion> Create(ICollection<TextQuestionDTO> textQuestions)
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
    private static List<IToneAudiometryQuestion> Create(ICollection<ToneAudiometryQuestionDTO> toneAudiometryQuestions)
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
