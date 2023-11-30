using BusinessLogic.Interfaces;
using BusinessLogic.IModels;
using BusinessLogic.IRepositories;

namespace BusinessLogic.Services;

public class TargetAudienceService
{
    private ITargetAudienceRepository targetAudienceRepository;

    public TargetAudienceService(ITargetAudienceRepository targetAudienceRepository)
    {
        this.targetAudienceRepository = targetAudienceRepository;
    }
        
    public List<ITargetAudience> GetAllTargetAudiences()
    {
        return targetAudienceRepository.GetAllAudiences();
    }
    

}
