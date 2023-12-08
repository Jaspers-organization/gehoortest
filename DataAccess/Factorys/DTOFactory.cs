using BusinessLogic.IModels;
using BusinessLogic.Models;
using DataAccess.DataTransferObjects;

namespace DataAccess.Factorys;

public static class DTOFactory
{
    public static TestDTO Create(ITest test)
    {
        return new TestDTO
        {
            Id = test.Id,
            Title = test.Title,
            Active = test.Active,
            EmployeeId = test.Employee.Id,
            TargetAudienceId = test.TargetAudience.Id,
            TextQuestions = Create(test.TextQuestions, test.Id),
            ToneAudiometryQuestions = Create(test.ToneAudiometryQuestions, test.Id)
        };
    }
    public static TargetAudienceDTO Create(ITargetAudience targetAudience)
    {
        return new TargetAudienceDTO
        {
            Id = targetAudience.Id,
            From = targetAudience.From,
            To = targetAudience.To,
            Label = targetAudience.Label,
        };
    }

    public static EmployeeDTO Create(IEmployee employee)
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
    public static ICollection<TargetAudienceDTO> Create(List<ITargetAudience> targetAudiences)
    {
        return targetAudiences.Select(targetAudience => new TargetAudienceDTO
        {
            From = targetAudience.From,
            Label = targetAudience.Label,
            To = targetAudience.To,
        }).ToList();
    }
    private static ICollection<TextQuestionDTO> Create(List<ITextQuestion> textQuestions, Guid testId)
    {
        return textQuestions.Select(textQuestion => new TextQuestionDTO
        {
            Id = textQuestion.Id,
            HasInputField = textQuestion.HasInputField,
            IsMultiSelect = textQuestion.IsMultiSelect,
            Options = Create(textQuestion.Options, textQuestion.Id),
            Question = textQuestion.Question,
            QuestionNumber = textQuestion.QuestionNumber,
            TestId = testId
        }).ToList();
    }
    private static ICollection<TextQuestionOptionDTO> Create(List<string> options, Guid textQuestionid)
    {
        var textQuestionOptions = options.Select(option => new TextQuestionOptionDTO
        {
            Id = Guid.NewGuid(),
            Option = option,
            TextQuestionId = textQuestionid
        }).ToList();

        return textQuestionOptions;
    }


    private static ICollection<ToneAudiometryQuestionDTO> Create(List<IToneAudiometryQuestion> audioQuestions, Guid testId)
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
}
