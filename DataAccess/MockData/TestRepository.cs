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
        private Employee _employee = new Employee { Id = 1, EmployeeNumber = "555", FirstName = "Japser", Infix = "is", LastName = " niet grappig" };
        public TestRepository(ITargetAudience targetAudience)
        {
            testDataList = new List<ITest>
            {
                new Test { Id = 1, TargetAudience = targetAudience, Active = true, Title = "title van een test", 
                    TextQuestions = new List<ITextQuestion>{
                        new TextQuestion { Id = 1, Question = "Test test test", QuestionNumber = 1, HasInputField = false, IsMultiSelect = false},
                        new TextQuestion { Id = 2, Question = "Test 2 ", QuestionNumber = 2, HasInputField = true, IsMultiSelect = true},
                        new TextQuestion { Id = 3, Question = "Test 3 ", QuestionNumber = 3, HasInputField = false, IsMultiSelect = true}
                    },
                    ToneAudiometryQuestions = new List<IToneAudiometryQuestion>{
                        new ToneAudiometryQuestion { Id = 1, Frequency = 1000, QuestionNumber = 1},
                        new ToneAudiometryQuestion { Id = 2, Frequency = 2000, QuestionNumber = 2},
                        new ToneAudiometryQuestion { Id = 3, Frequency = 3000, QuestionNumber = 3}
                    },Employee =  _employee
                },
            };
        }
        public void Create(ITest test)
        {
            throw new NotImplementedException();
        }

        public void Get(int id)
        {
            throw new NotImplementedException();
        }
        public TestProjection GetTests(int id)
        {
            ITest test = testDataList.Where(t => t.Id == id).First();

            return new TestProjection
            {
                Title = test.Title,
                AmountOfTextQuestions = test.TextQuestions.Count,
                AmountOfToneAudiometryQuestions = test.ToneAudiometryQuestions.Count,
                Active = test.Active,
                EmployeeName = test.Employee.Fullname
            };
        }
        public ITest GetTest(int id) => testDataList.FirstOrDefault(t => t.Id == id);

        public ObservableCollection<TestProjection> GetTestsProjectionForAudience(int id)
        {
            ITest test = testDataList.FirstOrDefault(t => t.Id == id);

            if (test == null)
            {
                return new ObservableCollection<TestProjection>();
            }

            TestProjection testProjection = new TestProjection
            {
                Id = test.Id,
                Title = test.Title,
                AmountOfTextQuestions = test.TextQuestions.Count,
                AmountOfToneAudiometryQuestions = test.ToneAudiometryQuestions.Count,
                Active = test.Active,
                EmployeeName = test.Employee.Fullname
            };
            TestProjection testProjectionn = new TestProjection
            {
                Id = test.Id,
                Title = test.Title,
                AmountOfTextQuestions = test.TextQuestions.Count,
                AmountOfToneAudiometryQuestions = test.ToneAudiometryQuestions.Count,
                Active = test.Active,
                EmployeeName = test.Employee.Fullname
            };

            return new ObservableCollection<TestProjection> { testProjection, testProjectionn };
        }

        public void Update(ITest test)
        {
            throw new NotImplementedException();
        }
    }
}
