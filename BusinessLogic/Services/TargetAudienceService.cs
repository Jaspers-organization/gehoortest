using BusinessLogic.IRepositories;
using BusinessLogic.Models;
using BusinessLogic.BusinessRules;

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

    #region CRUD
    public List<TargetAudience> GetAllTargetAudiences()
    {
        return targetAudienceRepository.GetAll();
    }

    public void Create(TargetAudience targetAudience)
    {
        TargetAudienceBusinessRules.AssertValidRange(targetAudience, GetAllTargetAudiences());

        UpdateLabel(targetAudience);

        targetAudienceRepository.Create(targetAudience);
    }

    public void Update(TargetAudience targetAudience)
    {
        TargetAudienceBusinessRules.AssertValidRange(targetAudience, GetAllTargetAudiences());
        AssertNotLinked(targetAudience.Id);

        UpdateLabel(targetAudience);

        targetAudienceRepository.Update(targetAudience);
    }

    public void Delete(Guid id)
    {
        AssertNotLinked(id);

        targetAudienceRepository.Delete(id);
    }
    #endregion

    private void UpdateLabel(TargetAudience targetAudience)
    {
        targetAudience.Label = $"{targetAudience.From}-{targetAudience.To}";
    }

    private void AssertNotLinked(Guid id)
    {
        if (testRepository.GetByTargetAudienceId(id) != null)
        {
            throw new Exception("Je kunt deze leeftijdsgroep niet aanpassen, omdat hij is gekoppeld aan een test.");
        }
    }
}
