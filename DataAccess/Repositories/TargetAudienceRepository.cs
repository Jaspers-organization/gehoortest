using BusinessLogic.IRepositories;
using BusinessLogic.Models;
using gehoortest_application.Repository;

namespace DataAccess.Repositories;

public class TargetAudienceRepository : ITargetAudienceRepository
{
    public List<TargetAudience> GetAll()
    {
        using (Repository repository = new Repository())
        {
            return repository.TargetAudiences.OrderBy(audience => audience.From).ToList();
        }
    }

    public void Create(TargetAudience targetAudience)
    {
        using (Repository repository = new Repository())
        {
            repository.TargetAudiences.Add(targetAudience);
            repository.SaveChanges();
        }
    }

    public void Update(TargetAudience targetAudience)
    {
        using (Repository repository = new Repository())
        {
            repository.TargetAudiences.Update(targetAudience);
            repository.SaveChanges();
        }
    }

    public void Delete(Guid id)
    {
        using (Repository repository = new Repository())
        { 
            TargetAudience targetAudience = repository.TargetAudiences.First(item => item.Id == id);
            repository.TargetAudiences.Remove(targetAudience);
            repository.SaveChanges();
        }
    }
}
