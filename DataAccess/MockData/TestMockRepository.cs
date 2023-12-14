using BusinessLogic.Projections;
using BusinessLogic.IRepositories;
using BusinessLogic.Models;

namespace DataAccess.MockData;

public class TestMockRepository : ITestRepository
{
    //public List<Test> testDataList = new List<Test>();
    public List<Test> testDataList = new List<Test>
        {
            new Test { Id = new Guid(), TargetAudience = new TargetAudience{ Id = new Guid(), From = 0, To=18, Label="-18" }, Active = false, Title = "Jongeren test",
                TextQuestions = new List<TextQuestion>{
                    new TextQuestion { Id = new Guid(), Question = "Worden je oren snel gevoelig voor harde geluiden?", QuestionNumber = 1, HasInputField = false, IsMultiSelect = true, 
                        Options = new List<TextQuestionOption>() {
                            new TextQuestionOption() { Id = new Guid(), Option = "Ja" },
                            new TextQuestionOption() { Id = new Guid(), Option = "Nee" },
                            new TextQuestionOption() { Id = new Guid(), Option = "Niet gemerkt" },
                        }
                    },
                    new TextQuestion { Id = new Guid(), Question = "Heb je wel eens last gehad van een piep in je oren na het luisteren naar luide muziek?",Options = new List<TextQuestionOption>(), QuestionNumber = 2, HasInputField = true, IsMultiSelect = false},
                },
                ToneAudiometryQuestions = new List<ToneAudiometryQuestion>{
                    new ToneAudiometryQuestion { Id = new Guid(), Frequency = 1000, StartingDecibels = 30, QuestionNumber = 1},
                    new ToneAudiometryQuestion { Id = new Guid(), Frequency = 2000, StartingDecibels = 30, QuestionNumber = 2},
                    new ToneAudiometryQuestion { Id = new Guid(), Frequency = 3000, StartingDecibels = 30, QuestionNumber = 3}
                },Employee =  new Employee { Id = new Guid(), EmployeeNumber = "123456789", FirstName = "Dinny", Infix = "van", LastName = "Huizen" }
            },
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
                    new ToneAudiometryQuestion { Id = new Guid(), Frequency = 2000, StartingDecibels = 30, QuestionNumber = 2},
                    new ToneAudiometryQuestion { Id = new Guid(), Frequency = 3000, StartingDecibels = 30, QuestionNumber = 3}
                },Employee =  new Employee { Id = new Guid(), EmployeeNumber = "987654321", FirstName = "Sisimaile",  LastName = "Lolohea" }
            },
            new Test { Id = new Guid(), TargetAudience = new TargetAudience{ Id = new Guid(), From = 19, To=29, Label="19-29" }, Active = false, Title = "Jong-volwassen test #2",
                TextQuestions = new List<TextQuestion>{
                    new TextQuestion { Id = new Guid(), Question = "Gebruik je gehoorbescherming als je naar een festival gaat?", QuestionNumber = 1, HasInputField = true, IsMultiSelect = true,
                        Options = new List<TextQuestionOption>() {
                            new TextQuestionOption() { Id = new Guid(), Option = "Ja" },
                            new TextQuestionOption() { Id = new Guid(), Option = "Nee" },
                            new TextQuestionOption() { Id = new Guid(), Option = "Ik ga nooit naar festivals" },
                        }
                    },
                    new TextQuestion { Id = new Guid(), Question = "Heb je moeite met mensen verstaan in je omgeving?",QuestionNumber = 2, HasInputField = false, IsMultiSelect = true,
                        Options = new List<TextQuestionOption>() {
                            new TextQuestionOption() { Id = new Guid(), Option = "Ja" },
                            new TextQuestionOption() { Id = new Guid(), Option = "Nee" },
                            new TextQuestionOption() { Id = new Guid(), Option = "Niet gemerkt" },
                        }
                    },
                    new TextQuestion { Id = new Guid(), Question = "Nog een vraag... ik weet het nog niet.",Options = new List<TextQuestionOption>(), QuestionNumber = 3,  HasInputField = true, IsMultiSelect = false}
                },
                ToneAudiometryQuestions = new List<ToneAudiometryQuestion>{
                    new ToneAudiometryQuestion { Id = new Guid(), Frequency = 1000, StartingDecibels = 30, QuestionNumber = 1},
                    new ToneAudiometryQuestion { Id = new Guid(), Frequency = 2000, StartingDecibels = 30, QuestionNumber = 2},
                    new ToneAudiometryQuestion { Id = new Guid(), Frequency = 3000, StartingDecibels = 30, QuestionNumber = 3}
                },Employee =  new Employee { Id = new Guid(), EmployeeNumber = "543216789", FirstName = "Jasper", LastName = "Gräber" }
            },
            new Test { Id = new Guid(), TargetAudience = new TargetAudience{ Id = new Guid(), From = 19, To=29, Label="19-29" }, Active = false, Title = "Jong-volwassen test #3",
                TextQuestions = new List<TextQuestion>{
                    new TextQuestion { Id = new Guid(), Question = "Gebruik je gehoorbescherming als je naar een festival gaat?", QuestionNumber = 1, HasInputField = true, IsMultiSelect = true,
                        Options = new List<TextQuestionOption>() {
                            new TextQuestionOption() { Id = new Guid(), Option = "Ja" },
                            new TextQuestionOption() { Id = new Guid(), Option = "Nee" },
                            new TextQuestionOption() { Id = new Guid(), Option = "Ik ga nooit naar festivals" },
                        }
                    },
                    new TextQuestion { Id = new Guid(), Question = "Heb je moeite met mensen verstaan in je omgeving?",QuestionNumber = 2, HasInputField = false, IsMultiSelect = true,
                        Options = new List<TextQuestionOption>() {
                            new TextQuestionOption() { Id = new Guid(), Option = "Ja" },
                            new TextQuestionOption() { Id = new Guid(), Option = "Nee" },
                            new TextQuestionOption() { Id = new Guid(), Option = "Niet gemerkt" },
                        }
                    },
                    new TextQuestion { Id = new Guid(), Question = "Nog een vraag... ik weet het nog niet.",Options = new List<TextQuestionOption>(), QuestionNumber = 3,  HasInputField = true, IsMultiSelect = false}
                },
                ToneAudiometryQuestions = new List<ToneAudiometryQuestion>{
                    new ToneAudiometryQuestion { Id = new Guid(), Frequency = 1000, StartingDecibels = 30, QuestionNumber = 1},
                    new ToneAudiometryQuestion { Id = new Guid(), Frequency = 2000, StartingDecibels = 30, QuestionNumber = 2},
                    new ToneAudiometryQuestion { Id = new Guid(), Frequency = 3000, StartingDecibels = 30, QuestionNumber = 3}
                },Employee =  new Employee { Id = new Guid(), EmployeeNumber = "543216789", FirstName = "Jasper", LastName = "Gräber" }
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

}
