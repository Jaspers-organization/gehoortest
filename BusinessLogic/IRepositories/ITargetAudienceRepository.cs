using BusinessLogic.Models;

namespace BusinessLogic.IRepositories;

public interface ITargetAudienceRepository
{
    public List<TargetAudience> GetAll();
    //public void FillTargetAudiences();
}
