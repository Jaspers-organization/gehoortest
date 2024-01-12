using BusinessLogic.IRepositories;
using BusinessLogic.Models;
using BusinessLogic.Projections;
using BusinessLogic.Services;
using gehoortest_application.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

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
        public List<TestProjection>? GetTestProjectionsByNoTargetAudience()
        {
            var tests = IncludeTestRelations()
                .Where(test => test.TargetAudienceId == Guid.Empty)
                .ToList();

            if (tests == null || tests.Count == 0)
            {
                return new List<TestProjection>();
            }

            return CreateProjections(tests);
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

            return CreateProjections(tests);
        }

        private List<TestProjection> CreateProjections(List<Test> tests)
        {
            return tests.Select(test => new TestProjection
            {
                Id = test.Id,
                Title = test.Title,
                AmountOfQuestions = test.TextQuestions.Count + test.ToneAudiometryQuestions.Count,
                Active = test.Active,
                EmployeeName = test.Employee.FullName,
                TextQuestions = test.TextQuestions.ToList(),
                ToneAudiometryQuestions = test.ToneAudiometryQuestions.ToList(),
                
            }).ToList();
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
            test.TextQuestions = test.TextQuestions.OrderBy(q => q.QuestionNumber).ToList();

            test.ToneAudiometryQuestions = test.ToneAudiometryQuestions.OrderBy(q => q.QuestionNumber).ToList();


            return test;
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

        public Test? GetActiveByTargetAudienceId(Guid id)
        {
            return repository.Tests.FirstOrDefault(item => item.TargetAudience.Id == id && item.Active == true);
        }

        public List<Test> GetTestsByTargetAudienceId(Guid id)
        {
            return repository.Tests.Where(t => t.TargetAudienceId == id).ToList();
        }
    }
}
