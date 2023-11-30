using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic;
using BusinessLogic.Interfaces;
using BusinessLogic.Interfaces.Repositories;

namespace DataAccess.Repositorys;

public class TargetAudienceRepository : ITargetAudienceRepository
{
    public List<ITargetAudience> Get()
    {
        List<ITargetAudience> listTargetAudience = new List<ITargetAudience>
        {
            new TargetAudience(1, 0, 18, "Jongeren"),
            new TargetAudience(2, 19, 29, "QuarterLife Crisis"),
            new TargetAudience(3, 30, 49, "Tsja"),
            new TargetAudience(4, 50, 69, "Midlife crisis"),
            new TargetAudience(5, 70, 79, "Pensionarisen"),
            new TargetAudience(6, 80, 89, "Senioren"),
        };
        return listTargetAudience;
    }
}
