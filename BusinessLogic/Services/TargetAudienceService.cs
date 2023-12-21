using BusinessLogic.IRepositories;
using BusinessLogic.Models;

namespace BusinessLogic.Services;

public class TargetAudienceService
{
    #region dependencies
    private ITargetAudienceRepository targetAudienceRepository;
    private ITestRepository testRepository;
    #endregion

    public TargetAudienceService(
        ITargetAudienceRepository targetAudienceRepository,
        ITestRepository testRepository
    ) {
        this.targetAudienceRepository = targetAudienceRepository;
        this.testRepository = testRepository;
    }
        
    public List<TargetAudience> GetAllTargetAudiences()
    {
        return targetAudienceRepository.GetAll();
    }

    public void Create(TargetAudience targetAudience)
    {
        AssertValidRange(targetAudience);

        targetAudienceRepository.Create(targetAudience);
    }

    public void Update(TargetAudience targetAudience)
    {
        AssertValidRange(targetAudience);
        AssertNotLinked(targetAudience.Id);

        targetAudienceRepository.Update(targetAudience);
    }

    public void Delete(Guid id)
    {
        AssertNotLinked(id);

        targetAudienceRepository.Delete(id);
    }

    private void AssertValidRange(TargetAudience targetAudience) 
    {
        List<TargetAudience> targetAudiences = targetAudienceRepository.GetAll();

        foreach (TargetAudience item in targetAudiences)
        {
            if (item.From <= targetAudience.From && item.To >= targetAudience.To)
            {
                throw new Exception();
            }
        }
    }

    private void AssertNotLinked(Guid id)
    {
        Test? test = testRepository.GetByTargetAudienceId(id);
        if (test != null) 
        {
            throw new Exception();
        }
    }
}
