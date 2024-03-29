﻿using BusinessLogic.Models;
using Service.Projections;
using BusinessLogic.Interfaces.Repositories;

namespace DataAccess.MockData;

public class TargetAudienceMockRepository : ITargetAudienceRepository
{
    public void Create(TargetAudience targetAudience)
    {
        throw new NotImplementedException();
    }

    public void Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public void FillTargetAudiences()
    {
        throw new NotImplementedException();
    }

    public TargetAudience Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public List<TargetAudience> GetAll()
    {

        List<TargetAudience> audienceList = new List<TargetAudience>
        {
            new TargetAudience { Id = new Guid(), From = 0, To = 18, Label = "0-18" },
            new TargetAudience { Id = new Guid(), From = 19, To = 29, Label = "19-29" },
            new TargetAudience { Id = new Guid(), From = 30, To = 49, Label = "30-49" },
            new TargetAudience { Id = new Guid(), From = 50, To = 69, Label = "50-69" },
            new TargetAudience { Id = new Guid(), From = 70, To = 79, Label = "70-79" },
            new TargetAudience { Id = new Guid(), From = 80, To = 89, Label = "80-89" },
        };

        return audienceList;
    }

    public List<TargetAudience> GetAllAudiences(int id)
    {
        throw new NotImplementedException();
    }

    public List<TargetAudienceProjection> GetAllWithTestAmount()
    {
        throw new NotImplementedException();
    }

    public void Update(TargetAudience targetAudience)
    {
        throw new NotImplementedException();
    }

    public List<TargetAudience> GetAllActiveWithTest()
    {
        throw new NotImplementedException();
    }
}
