using BusinessLogic.Interfaces;
using BusinessLogic.IModels;
using BusinessLogic.IRepositories;
using BusinessLogic.Models;

namespace BusinessLogic.Services;

public class TargetAudienceService
{
    private ITargetAudienceRepository targetAudienceRepository;

    public TargetAudienceService(ITargetAudienceRepository targetAudienceRepository)
    {
        this.targetAudienceRepository = targetAudienceRepository;
    }
        
    public List<TargetAudience> GetAllTargetAudiences()
    {
        return targetAudienceRepository.GetAll();
    }
    

}
