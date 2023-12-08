﻿using DataAccess.DataTransferObjects;
using BusinessLogic.IModels;
using BusinessLogic.IRepositories;
using DataAccess.Mapping;
using BusinessLogic.Models;
using BusinessLogic.Projections;
using gehoortest_application.Repository;
using Microsoft.EntityFrameworkCore;
using DataAccess.Factorys;

namespace DataAccess.Repositories
{
    public class TestRepository : ITestRepository
    {
        private readonly Repository repository;
        public TestRepository(Repository repository) => this.repository = repository;

        public ITest CreateTest() => new Test();


        public void DeleteTest(ITest test)
        {
            throw new NotImplementedException();
        }

        public ITest? GetActiveTest()
        {
            throw new NotImplementedException();
        }

        public List<ITest> GetAllTests()
        {
            throw new NotImplementedException();
        }

        public ITest? GetTestById(Guid id)
        {
            throw new NotImplementedException();
        }

        public ITest? GetTestByTargetAudienceId(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<TestProjection>? GetTestProjectionsByTargetAudienceId(Guid id)
        {
            throw new NotImplementedException();
        }

        public void SaveTest(ITest test)
        {
            repository.Tests.Add(DTOFactory.Create(test));
            repository.SaveChanges();
            //repository.SaveChangesWithIdentityInsert<TestDTO>();

        }

        public void UpdateTest(ITest test)
        {
            throw new NotImplementedException();
        }
    }
}
