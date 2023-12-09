using BusinessLogic.Models;

namespace BusinessLogic.IRepositories;

public interface ITargetAudienceRepository
{
    public List<TargetAudience> GetAllAudiences();
    //public void FillTargetAudiences();
}
