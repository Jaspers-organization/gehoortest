using BusinessLogic.IModels;
using BusinessLogic.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Models;

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

    public void Update(TargetAudience targetAudience)
    {
        throw new NotImplementedException();
    }
}
