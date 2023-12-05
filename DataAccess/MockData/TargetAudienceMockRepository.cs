using DataAccess.Models.TestData_Management;
using BusinessLogic.IModels;
using BusinessLogic.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.MockData;

public class TargetAudienceMockRepository : ITargetAudienceRepository
{
    public List<ITargetAudience> GetAllAudiences()
    {

        List<ITargetAudience> audienceList = new List<ITargetAudience>
        {
            new TargetAudience { Id = 0, From = 0, To = 18, Label = "0-18" },
            new TargetAudience { Id = 1, From = 19, To = 29, Label = "19-29" },
            new TargetAudience { Id = 2, From = 30, To = 49, Label = "30-49" },
            new TargetAudience { Id = 3, From = 50, To = 69, Label = "50-69" },
            new TargetAudience { Id = 4, From = 70, To = 79, Label = "70-79" },
            new TargetAudience { Id = 5, From = 80, To = 89, Label = "80-89" },
        };
      
        return audienceList;
    }
}
