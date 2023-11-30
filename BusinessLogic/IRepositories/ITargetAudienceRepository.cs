using BusinessLogic.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.IRepositories;

public interface ITargetAudienceRepository
{
    List<ITargetAudience> GetAllAudiences();
    
}
