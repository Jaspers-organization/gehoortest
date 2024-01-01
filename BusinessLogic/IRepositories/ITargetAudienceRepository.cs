using BusinessLogic.Models;

namespace BusinessLogic.IRepositories;

public interface ITargetAudienceRepository
{
    public List<TargetAudience> GetAll();
    public void Create(TargetAudience targetAudience);
    public void Update(TargetAudience targetAudience);
    public void Delete(Guid id);
}
