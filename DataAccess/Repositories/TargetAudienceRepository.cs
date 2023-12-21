using BusinessLogic.IRepositories;
using BusinessLogic.Models;
using gehoortest_application.Repository;

namespace DataAccess.Repositories;

public class TargetAudienceRepository : ITargetAudienceRepository
{
    private readonly Repository repository = new Repository();

    public List<TargetAudience> GetAll()
    {
        return repository.TargetAudiences.OrderBy(audience => audience.From).ToList();
    }

    public void Create(TargetAudience targetAudience)
    {
        repository.TargetAudiences.Add(targetAudience);
        repository.SaveChanges();
    }

    public void Update(TargetAudience targetAudience)
    {
        repository.TargetAudiences.Update(targetAudience);
        repository.SaveChanges();
    }

    public void Delete(Guid id)
    {
        TargetAudience targetAudience = repository.TargetAudiences.First(item => item.Id == id);
        repository.TargetAudiences.Remove(targetAudience);
        repository.SaveChanges();
    }
}
