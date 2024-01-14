using BusinessLogic.Interfaces.Repositories;
using BusinessLogic.Models;
using gehoortest_application.Repository;
using Service.Projections;

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

    public TargetAudience Get(Guid id)
    {
        using (Repository repository = new Repository())
        {
            return repository.TargetAudiences.FirstOrDefault(item => item.Id == id);
        }
    }

    public List<TargetAudienceProjection> GetAllWithTestAmount()
    {
        using (Repository repository = new Repository())
        {
            var result = repository.TargetAudiences.Where(ta => ta.Id != Guid.Empty)
                .Select(ta => new TargetAudienceProjection
                {
                    Id = ta.Id,
                    Label = ta.Label,
                    From = ta.From.ToString(),
                    To = ta.To.ToString(),
                    AmountOfTests = ta.Tests.Count()
                })
                .OrderBy(ta => ta.From)
                .ToList();

            return result;
        }
    }

    public List<TargetAudience> GetAllActiveWithTest()
    {
        using (Repository context = new Repository())
        {
            return context.TargetAudiences
                .Join(context.Tests,
                    ta => ta.Id,
                    t => t.TargetAudienceId,
                    (ta, t) => new { TargetAudience = ta, Test = t })
                .Where(joinResult => joinResult.Test.Active)
                .Select(joinResult => joinResult.TargetAudience)
                .Distinct().ToList();
        }
    }
}
