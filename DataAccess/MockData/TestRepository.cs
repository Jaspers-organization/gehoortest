using BusinessLogic.Projections;
using DataAccess.Entity.TestData_Management;
using DataAccess.Models.LoginData_Management;
using DataAccess.Models.TestData_Management;
using BusinessLogic.IModels;
using BusinessLogic.IRepositories;
using System.Collections.ObjectModel;

namespace DataAccess.MockData;

public class TestRepository : ITestRepository
{
    public List<ITest> testDataList;

    public TestRepository()
    {
        testDataList = new List<ITest>
        {
            new Test { Id = 0, TargetAudience = new TargetAudience{ Id = 0, From = 0, To=18, Label="-18" }, Active = false, Title = "title van een test",
                TextQuestions = new List<ITextQuestion>{
                    new TextQuestion { Id = 0, Question = "Test test test", Options = new List<string>(), QuestionNumber = 1, HasInputField = false, IsMultiSelect = false},
                    new TextQuestion { Id = 1, Question = "Test 2 ",Options = new List<string>(), QuestionNumber = 2, HasInputField = true, IsMultiSelect = true},
                    new TextQuestion { Id = 2, Question = "Test 3 ", Options = new List<string>(),QuestionNumber = 3,  HasInputField = false, IsMultiSelect = true}
                },
                ToneAudiometryQuestions = new List<IToneAudiometryQuestion>{
                    new ToneAudiometryQuestion { Id = 0, Frequency = 1000, StartingDecibels = 30, QuestionNumber = 1},
                    new ToneAudiometryQuestion { Id = 1, Frequency = 2000, StartingDecibels = 30, QuestionNumber = 2},
                    new ToneAudiometryQuestion { Id = 2, Frequency = 3000, StartingDecibels = 30, QuestionNumber = 3}
                },Employee =  new Employee { Id = 0, EmployeeNumber = "555", FirstName = "Japser", Infix = "is", LastName = " niet grappig" }
            },
            new Test { Id = 1, TargetAudience = new TargetAudience{ Id = 1, From = 19, To=29, Label="19-29" }, Active = true, Title = "ik houd van testen",
                TextQuestions = new List<ITextQuestion>{
                    new TextQuestion { Id = 0, Question = "dsadsadsadsa test test", QuestionNumber = 1, HasInputField = false, IsMultiSelect = false},
                    new TextQuestion { Id = 1, Question = "zvxcvcxz 2 ", QuestionNumber = 2, HasInputField = true, IsMultiSelect = true},
                    new TextQuestion { Id = 2, Question = "vzxcvczxv z 3 ", QuestionNumber = 3,  HasInputField = false, IsMultiSelect = true}
                },
                ToneAudiometryQuestions = new List<IToneAudiometryQuestion>{
                    new ToneAudiometryQuestion { Id = 0, Frequency = 1000, StartingDecibels = 30, QuestionNumber = 1},
                    new ToneAudiometryQuestion { Id = 1, Frequency = 2000, StartingDecibels = 30, QuestionNumber = 2},
                    new ToneAudiometryQuestion { Id = 2, Frequency = 3000, StartingDecibels = 30, QuestionNumber = 3}
                },Employee =  new Employee { Id = 1, EmployeeNumber = "555", FirstName = "Dinny", Infix = "is", LastName = " wel grappig" }
            },
            new Test { Id = 2, TargetAudience = new TargetAudience{ Id = 1, From = 19, To=29, Label="19-29" }, Active = false, Title = "ik houd van testen en corona",
                TextQuestions = new List<ITextQuestion>{
                    new TextQuestion { Id = 0, Question = "dsadsadsadsa test test", Options = new List<string>(), QuestionNumber = 1, HasInputField = false, IsMultiSelect = false},
                    new TextQuestion { Id = 1, Question = "zvxcvcxz 2 ",Options = new List<string>(),QuestionNumber = 2, HasInputField = true, IsMultiSelect = true},
                    new TextQuestion { Id = 2, Question = "vzxcvczxv z 3 ",Options = new List<string>(), QuestionNumber = 3,  HasInputField = false, IsMultiSelect = true}
                },
                ToneAudiometryQuestions = new List<IToneAudiometryQuestion>{
                    new ToneAudiometryQuestion { Id = 0, Frequency = 1000, StartingDecibels = 30, QuestionNumber = 1},
                    new ToneAudiometryQuestion { Id = 1, Frequency = 2000, StartingDecibels = 30, QuestionNumber = 2},
                    new ToneAudiometryQuestion { Id = 2, Frequency = 3000, StartingDecibels = 30, QuestionNumber = 3}
                },Employee =  new Employee { Id = 1, EmployeeNumber = "555", FirstName = "Dinny", Infix = "is", LastName = " wel grappig" }
            },
        };
    }
    public ITest CreateTest()
    {
        return new Test();
    }
    
    public TestProjection GetTests(int id)
    {
        ITest test = testDataList.Where(t => t.Id == id).First();

        return new TestProjection
        {
            Id = test.Id,
            Title = test.Title,
            AmountOfQuestions = test.TextQuestions.Count + test.ToneAudiometryQuestions.Count,
            Active = test.Active,
            EmployeeName = test.Employee.Fullname
        };
    }
    public ITest GetTest(int id) => testDataList.FirstOrDefault(t => t.Id == id);

    public ObservableCollection<TestProjection> GetTestsProjectionForAudience(int id)
    {
        var tests = testDataList.Where(t => t.TargetAudience.Id == id).ToList();

        if (tests == null || tests.Count == 0)
        {
            return new ObservableCollection<TestProjection>();
        }

        List<TestProjection> projections = tests.Select(test => new TestProjection
        {
            Id = test.Id,
            Title = test.Title,
            AmountOfQuestions = test.TextQuestions.Count + test.ToneAudiometryQuestions.Count,
            Active = test.Active,
            EmployeeName = test.Employee.Fullname
        }).ToList();

        return new ObservableCollection<TestProjection>(projections);
    }

    public void DeleteTest(ITest test)
    {
        testDataList.Remove(test);
    }
    public void UpdateTest(ITest test)
    {
        int index = testDataList.FindIndex(t => t.Id == test.Id);
        testDataList[index] = test;
    }

    public void SaveTest(ITest test)
    {
        testDataList.Add(test);
    }
}
