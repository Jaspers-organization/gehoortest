using BusinessLogic.Models;
using Service.Projections;

namespace BusinessLogic.IRepositories;

public interface ITargetAudienceRepository
{
    public List<TargetAudience> GetAll();
    public void Create(TargetAudience targetAudience);
    public void Update(TargetAudience targetAudience);
    public void Delete(Guid id);
    public List<TargetAudienceProjection> GetAllWithTestAmount();
    public TargetAudience Get(Guid id);
    public List<TargetAudience> GetAllActiveWithTest();
}
