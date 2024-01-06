using BusinessLogic.IRepositories;
using BusinessLogic.Models;
using BusinessLogic.BusinessRules;
using BusinessLogic.Stores;
using Service.Projections;

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
    )
    {
        this.targetAudienceRepository = targetAudienceRepository;
        this.testRepository = testRepository;
    }

    #region CRUD
    public List<TargetAudience> GetAllTargetAudiences() => targetAudienceRepository.GetAll().ToList();


    public List<TargetAudienceProjection> GetAllTargetAudienceProjections()
    {
        return targetAudienceRepository.GetAllWithTestAmount();
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

        UpdateLabel(targetAudience);

        targetAudienceRepository.Update(targetAudience);
    }
    public TargetAudience Get(Guid id)
    {
        return targetAudienceRepository.Get(id);
    }
    public void Delete(Guid id)
    {
        var tests = testRepository.GetTestsByTargetAudienceId(id);
        foreach (var test in tests)
        {
            test.TargetAudienceId = Guid.Empty;
            test.Active = false;
            testRepository.UpdateTest(test);
        }
        targetAudienceRepository.Delete(id);
    }
    #endregion

    private void UpdateLabel(TargetAudience targetAudience)
    {
        targetAudience.Label = $"{targetAudience.From}-{targetAudience.To}";
    }

}
