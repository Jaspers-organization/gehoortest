using BusinessLogic.Projections;
using DataAccess.Entity.TestData_Management;
using DataAccess.Models.LoginData_Management;
using DataAccess.Models.TestData_Management;
using BusinessLogic.IModels;
using BusinessLogic.IRepositories;
using System.Collections.ObjectModel;

namespace DataAccess.MockData;

public class TestMockRepository : ITestRepository
{
    public List<ITest> testDataList = new List<ITest>
        {
            new Test { Id = 0, TargetAudience = new TargetAudience{ Id = 0, From = 0, To=18, Label="-18" }, Active = false, Title = "Jongeren test",
                TextQuestions = new List<ITextQuestion>{
                    new TextQuestion { Id = 0, Question = "Worden je oren snel gevoelig voor harde geluiden?", Options = new List<string>{"Ja", "Nee", "Niet gemerkt" }, QuestionNumber = 1, HasInputField = false, IsMultiSelect = true},
                    new TextQuestion { Id = 1, Question = "Heb je wel eens last gehad van een piep in je oren na het luisteren naar luide muziek?",Options = new List<string>(), QuestionNumber = 2, HasInputField = true, IsMultiSelect = false},
                },
                ToneAudiometryQuestions = new List<IToneAudiometryQuestion>{
                    new ToneAudiometryQuestion { Id = 0, Frequency = 1000, StartingDecibels = 30, QuestionNumber = 1},
                    new ToneAudiometryQuestion { Id = 1, Frequency = 2000, StartingDecibels = 30, QuestionNumber = 2},
                    new ToneAudiometryQuestion { Id = 2, Frequency = 3000, StartingDecibels = 30, QuestionNumber = 3}
                },Employee =  new Employee { Id = 0, EmployeeNumber = "123456789", FirstName = "Dinny", Infix = "van", LastName = "Huizen" }
            },
            new Test { Id = 1, TargetAudience = new TargetAudience{ Id = 1, From = 19, To=29, Label="19-29" }, Active = true, Title = "Jong-volwassen test",
                TextQuestions = new List<ITextQuestion>{
                    new TextQuestion { Id = 0, Question = "Ga je vaak naar festivals?", QuestionNumber = 1, HasInputField = true, IsMultiSelect = false},
                    new TextQuestion { Id = 1, Question = "Luister je vaak harde muziek?", QuestionNumber = 2, HasInputField = true, IsMultiSelect = false},
                    new TextQuestion { Id = 2, Question = "Wat voor beroep doe je?", QuestionNumber = 3,Options = new List<string>{"Kantoorbaan", "Bouw","Horeca", "Werkenloos"}, HasInputField = false, IsMultiSelect = true}
                },
                ToneAudiometryQuestions = new List<IToneAudiometryQuestion>{
                    new ToneAudiometryQuestion { Id = 0, Frequency = 1000, StartingDecibels = 30, QuestionNumber = 1},
                    new ToneAudiometryQuestion { Id = 1, Frequency = 2000, StartingDecibels = 30, QuestionNumber = 2},
                    new ToneAudiometryQuestion { Id = 2, Frequency = 3000, StartingDecibels = 30, QuestionNumber = 3}
                },Employee =  new Employee { Id = 1, EmployeeNumber = "987654321", FirstName = "Sisimaile",  LastName = "Lolohea" }
            },
            new Test { Id = 2, TargetAudience = new TargetAudience{ Id = 1, From = 19, To=29, Label="19-29" }, Active = false, Title = "Jong-volwassen test #2",
                TextQuestions = new List<ITextQuestion>{
                    new TextQuestion { Id = 0, Question = "Gebruik je gehoorbescherming als je naar een festival gaat?", Options = new List<string>{"Ja", "Nee", "Ik ga nooit naar festivals" }, QuestionNumber = 1, HasInputField = true, IsMultiSelect = true},
                    new TextQuestion { Id = 1, Question = "Heb je moeite met mensen verstaan in je omgeving?",Options = new List<string>{"Ja", "Nee", "Niet gemerkt" },QuestionNumber = 2, HasInputField = false, IsMultiSelect = true},
                    new TextQuestion { Id = 2, Question = "Nog een vraag... ik weet het nog niet.",Options = new List<string>(), QuestionNumber = 3,  HasInputField = true, IsMultiSelect = false}
                },
                ToneAudiometryQuestions = new List<IToneAudiometryQuestion>{
                    new ToneAudiometryQuestion { Id = 0, Frequency = 1000, StartingDecibels = 30, QuestionNumber = 1},
                    new ToneAudiometryQuestion { Id = 1, Frequency = 2000, StartingDecibels = 30, QuestionNumber = 2},
                    new ToneAudiometryQuestion { Id = 2, Frequency = 3000, StartingDecibels = 30, QuestionNumber = 3}
                },Employee =  new Employee { Id = 1, EmployeeNumber = "543216789", FirstName = "Jasper", LastName = "Gräber" }
            },
        };

    public TestMockRepository()
    {
        
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
    public List<ITest> GetAllTests()
    {
        return testDataList;
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
