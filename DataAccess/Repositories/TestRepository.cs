using BusinessLogic.IRepositories;
using BusinessLogic.Models;
using BusinessLogic.Projections;
using gehoortest_application.Repository;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataAccess.Repositories
{
    public class TestRepository : ITestRepository
    {
        private readonly Repository repository = new Repository();

        public Test CreateTest() => new Test();

        public void DeleteTest(Test test)
        {
            var testWithQuestions = repository.Tests
                .Include(t => t.TextQuestions)
                    .ThenInclude(tq => tq.Options)
                .FirstOrDefault(t => t.Id == test.Id);

            if (testWithQuestions == null)
                return;

            foreach (var textQuestion in testWithQuestions.TextQuestions.ToList())
            {
                repository.TextQuestionsOptions.RemoveRange(textQuestion.Options.ToList());
                repository.TextQuestions.Remove(textQuestion);
            }
            repository.Tests.Remove(testWithQuestions);
            repository.SaveChanges();
        }

        private IQueryable<Test> IncludeTestRelations()
        {
            return repository.Tests
                .Include(test => test.TextQuestions)
                    .ThenInclude(text => text.Options)
                .Include(test => test.ToneAudiometryQuestions)
                .Include(test => test.Employee)
                .Include(test => test.TargetAudience);
        }

        public List<Test> GetAllTests()
        {
            var tests = IncludeTestRelations().ToList();
            if (tests.Count == 0 || tests == null)
                return new List<Test>();

            return tests;
        }

        public Test? GetTestById(Guid id)
        {
            var test = IncludeTestRelations()
               .Where(test => test.Id == id)
               .FirstOrDefault();

            if (test == null)
            {
                return null; //error handeling 
            }
            return test;
        }

        public Test? GetTestByTargetAudienceIdAndActive(Guid id)
        {
            var test = IncludeTestRelations()
               .Where(test => test.TargetAudienceId == id && test.Active)
               .FirstOrDefault();

            if (test == null)
            {
                return null; //error handeling 
            }
            return test;
        }

        public List<TestProjection>? GetTestProjectionsByTargetAudienceId(Guid id)
        {
            var tests = IncludeTestRelations()
                .Where(test => test.TargetAudienceId == id)
                .ToList();

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

        public void SaveTest(Test test)
        {
            repository.Attach(test.TargetAudience);
            repository.Attach(test.Employee);
            repository.Tests.Add(test);
            repository.SaveChanges();
        }

        public void UpdateTest(Test updatedTest)
        {
            var existingTest = repository.Tests
                .Include(t => t.TextQuestions)
                    .ThenInclude(tq => tq.Options)
                .Include(t => t.ToneAudiometryQuestions)
                .FirstOrDefault(t => t.Id == updatedTest.Id);

            if (existingTest != null)
            {
                repository.Entry(existingTest).CurrentValues.SetValues(updatedTest);

                existingTest.TextQuestions = updatedTest.TextQuestions;
                existingTest.ToneAudiometryQuestions = updatedTest.ToneAudiometryQuestions;

                repository.Tests.Update(existingTest);
                repository.SaveChanges();
            }
        }

        public Test? GetByTargetAudienceId(Guid id)
        {
            return repository.Tests.FirstOrDefault(item => item.TargetAudience.Id == id);
        }
    }
}
