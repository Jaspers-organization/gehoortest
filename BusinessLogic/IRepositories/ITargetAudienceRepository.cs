using BusinessLogic.IModels;
using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.IRepositories;

public interface ITargetAudienceRepository
{
    public List<ITargetAudience> GetAllAudiences();
    public List<TargetAudience> GetAllAudiences(int id);

}
