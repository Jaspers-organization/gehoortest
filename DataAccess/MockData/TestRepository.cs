using BusinessLogic.Projections;
using DataAccess.Entity.TestData_Management;
using DataAccess.Models.LoginData_Management;
using DataAccess.Models.TestData_Management;
using BusinessLogic.IModels;
using BusinessLogic.IRepositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.MockData
{
    public class TestRepository : ITestRepository
    {
        public string Title { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<ITest> testDataList;

        public TestRepository()
        {
            testDataList = new List<ITest>
            {
                new Test { Id = 0, TargetAudience = new TargetAudience{ Id = 0, From = 0, To=18, Label="-18" }, Active = true, Title = "title van een test",
                    TextQuestions = new List<ITextQuestion>{
                        new TextQuestion { Id = 1, Question = "Test test test", Options = new List<string>(), QuestionNumber = 1, HasInputField = false, IsMultiSelect = false},
                        new TextQuestion { Id = 2, Question = "Test 2 ",Options = new List<string>(), QuestionNumber = 2, HasInputField = true, IsMultiSelect = true},
                        new TextQuestion { Id = 3, Question = "Test 3 ", Options = new List<string>(),QuestionNumber = 3,  HasInputField = false, IsMultiSelect = true}
                    },
                    ToneAudiometryQuestions = new List<IToneAudiometryQuestion>{
                        new ToneAudiometryQuestion { Id = 1, Frequency = 1000, StartingDecibels = 30, QuestionNumber = 1},
                        new ToneAudiometryQuestion { Id = 2, Frequency = 2000, StartingDecibels = 30, QuestionNumber = 2},
                        new ToneAudiometryQuestion { Id = 3, Frequency = 3000, StartingDecibels = 30, QuestionNumber = 3}
                    },Employee =  new Employee { Id = 1, EmployeeNumber = "555", FirstName = "Japser", Infix = "is", LastName = " niet grappig" }
                },
                new Test { Id = 1, TargetAudience = new TargetAudience{ Id = 1, From = 19, To=29, Label="19-29" }, Active = true, Title = "ik houd van testen",
                    TextQuestions = new List<ITextQuestion>{
                        new TextQuestion { Id = 1, Question = "dsadsadsadsa test test", QuestionNumber = 1, HasInputField = false, IsMultiSelect = false},
                        new TextQuestion { Id = 2, Question = "zvxcvcxz 2 ", QuestionNumber = 2, HasInputField = true, IsMultiSelect = true},
                        new TextQuestion { Id = 3, Question = "vzxcvczxv z 3 ", QuestionNumber = 3,  HasInputField = false, IsMultiSelect = true}
                    },
                    ToneAudiometryQuestions = new List<IToneAudiometryQuestion>{
                        new ToneAudiometryQuestion { Id = 1, Frequency = 1000, StartingDecibels = 30, QuestionNumber = 1},
                        new ToneAudiometryQuestion { Id = 2, Frequency = 2000, StartingDecibels = 30, QuestionNumber = 2},
                        new ToneAudiometryQuestion { Id = 3, Frequency = 3000, StartingDecibels = 30, QuestionNumber = 3}
                    },Employee =  new Employee { Id = 1, EmployeeNumber = "555", FirstName = "Dinny", Infix = "is", LastName = " wel grappig" }
                },
                new Test { Id = 2, TargetAudience = new TargetAudience{ Id = 1, From = 19, To=29, Label="19-29" }, Active = true, Title = "ik houd van testen en corona",
                    TextQuestions = new List<ITextQuestion>{
                        new TextQuestion { Id = 1, Question = "dsadsadsadsa test test", Options = new List<string>(), QuestionNumber = 1, HasInputField = false, IsMultiSelect = false},
                        new TextQuestion { Id = 2, Question = "zvxcvcxz 2 ",Options = new List<string>(),QuestionNumber = 2, HasInputField = true, IsMultiSelect = true},
                        new TextQuestion { Id = 3, Question = "vzxcvczxv z 3 ",Options = new List<string>(), QuestionNumber = 3,  HasInputField = false, IsMultiSelect = true}
                    },
                    ToneAudiometryQuestions = new List<IToneAudiometryQuestion>{
                        new ToneAudiometryQuestion { Id = 1, Frequency = 1000, StartingDecibels = 30, QuestionNumber = 1},
                        new ToneAudiometryQuestion { Id = 2, Frequency = 2000, StartingDecibels = 30, QuestionNumber = 2},
                        new ToneAudiometryQuestion { Id = 3, Frequency = 3000, StartingDecibels = 30, QuestionNumber = 3}
                    },Employee =  new Employee { Id = 1, EmployeeNumber = "555", FirstName = "Dinny", Infix = "is", LastName = " wel grappig" }
                },
            };
        }
        public void Create(ITest test)
        {
            throw new NotImplementedException();
        }

        public ITest Get(int id)
        {
            throw new NotImplementedException();
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

        public void Update(ITest test)
        {
            throw new NotImplementedException();
        }
    }
}
