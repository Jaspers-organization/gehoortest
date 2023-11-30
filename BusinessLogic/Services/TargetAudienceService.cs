using BusinessLogic.Interfaces;
using BusinessLogic.Interfaces.Repositories;
using BusinessLogic.IModels;
using BusinessLogic.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services;

public class TargetAudienceService
{
    private ITargetAudienceRepository targetAudienceRepository;

    public TargetAudienceService(ITargetAudienceRepository targetAudienceRepository)
    {
        this.targetAudienceRepository = targetAudienceRepository;
    }
        
    public List<ITargetAudience> GetTargetAudiences()
    {
            return targetAudienceRepository.Get();
    }

    public List<ITargetAudience> GetAllAudiences()
    {
        return targetAudienceRepository.GetAllAudiences();
    }
}
