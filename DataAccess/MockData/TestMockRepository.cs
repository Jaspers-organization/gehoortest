using BusinessLogic.Projections;
using BusinessLogic.Models;
using BusinessLogic.Interfaces.Repositories;

namespace DataAccess.MockData;

public class TestMockRepository : ITestRepository
{
    //public List<Test> testDataList = new List<Test>();
    public List<Test> testDataList = new List<Test>
    {
            new Test { Id = new Guid(), TargetAudience = new TargetAudience{ Id = new Guid(), From = 19, To=29, Label="19-29" }, Active = true, Title = "Jong-volwassen test",
                TextQuestions = new List<TextQuestion>{
                    new TextQuestion { Id = new Guid(), Question = "Ga je vaak naar festivals?", QuestionNumber = 1, HasInputField = true, IsMultiSelect = false},
                    new TextQuestion { Id = new Guid(), Question = "Luister je vaak harde muziek?", QuestionNumber = 2, HasInputField = true, IsMultiSelect = false},
                    new TextQuestion { Id = new Guid(), Question = "Wat voor beroep doe je?", QuestionNumber = 3, HasInputField = false, IsMultiSelect = true,
                        Options = new List<TextQuestionOption>() {
                            new TextQuestionOption() { Id = new Guid(), Option = "Kantoorbaan" },
                            new TextQuestionOption() { Id = new Guid(), Option = "Bouw" },
                            new TextQuestionOption() { Id = new Guid(), Option = "Horeca" },
                            new TextQuestionOption() { Id = new Guid(), Option = "Werkenloos" },
                        }
                    }
                },
                ToneAudiometryQuestions = new List<ToneAudiometryQuestion>{
                    new ToneAudiometryQuestion { Id = new Guid(), Frequency = 1000, StartingDecibels = 30, QuestionNumber = 1},
                    //new ToneAudiometryQuestion { Id = new Guid(), Frequency = 2000, StartingDecibels = 30, QuestionNumber = 2},
                    //new ToneAudiometryQuestion { Id = new Guid(), Frequency = 3000, StartingDecibels = 30, QuestionNumber = 3}
                },Employee =  new Employee { Id = new Guid(), EmployeeNumber = "987654321", FirstName = "Sisimaile",  LastName = "Lolohea" }
            },
        };

    public TestMockRepository() { }

    public Test CreateTest() => new Test();

    public TestProjection GetTests(Guid id)
    {
        Test test = testDataList.Where(t => t.Id == id).First();

        return new TestProjection
        {
            Id = test.Id,
            Title = test.Title,
            AmountOfQuestions = test.TextQuestions.Count + test.ToneAudiometryQuestions.Count,
            Active = test.Active,
            EmployeeName = test.Employee.FullName
        };
    }

    public List<TestProjection> GetTestProjectionsByTargetAudienceId(Guid id)
    {
        var tests = testDataList.Where(t => t.TargetAudience.Id == id).ToList();

        if (tests == null || tests.Count == 0)
        {
            return new List<TestProjection>();
        }

        List<TestProjection> projections = tests.Select(test => new TestProjection
        {
            Id = test.Id,
            Title = test.Title,
            AmountOfQuestions = test.TextQuestions.Count + test.ToneAudiometryQuestions.Count,
            Active = test.Active,
            EmployeeName = test.Employee.FullName
        }).ToList();

        return projections;
    }
    public List<Test> GetAllTests() => testDataList;

    public void DeleteTest(Test test) => testDataList.Remove(test);

    public void UpdateTest(Test test)
    {
        int index = testDataList.FindIndex(t => t.Id == test.Id);
        testDataList[index] = test;
    }

    public void SaveTest(Test test) => testDataList.Add(test);
    public List<Test>? GetTestsByTargetAudienceId(Guid id) => testDataList.Where(t => t.TargetAudience.Id == id).ToList();
    public Test? GetTestByTargetAudienceIdAndActive(Guid id) => testDataList.FirstOrDefault(t => t.TargetAudience.Id == id && t.Active);
    public Test? GetTestById(Guid id) => testDataList.FirstOrDefault(t => t.Id == id);

    public void RemoveOptionsWhereId(Guid id)
    {
        throw new NotImplementedException();
    }

    public Test? GetActiveByTargetAudienceId(Guid id)
    {
        throw new NotImplementedException();
    }

    public List<TestProjection>? GetTestProjectionsByNoTargetAudience()
    {
        throw new NotImplementedException();
    }

    public void UpdateTargetAudienceForTests(Guid id)
    {
        throw new NotImplementedException();
    }
}
