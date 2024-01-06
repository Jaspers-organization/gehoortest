using BusinessLogic.IRepositories;
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
                .ToList();

            return result;
        }
    }
    public List<TargetAudience> GetAllActiveWithTest()
    {
        using (Repository context = new Repository())
        {
            var query = from ta in context.TargetAudiences
                        join t in context.Tests on ta.Id equals t.TargetAudienceId
                        select ta;
            return query.ToList();
      }
    }

    public void FillTargetAudiences()
    {
        using (Repository context = new Repository())
        {
            context.TargetAudiences.AddRange(new List<TargetAudience>{
                new TargetAudience { Id = new Guid(), From = 0, To = 18, Label = "0-18" },
                new TargetAudience { Id = new Guid(), From = 19, To = 29, Label = "19-29" },
                new TargetAudience { Id = new Guid(), From = 30, To = 49, Label = "30-49" },
                new TargetAudience { Id = new Guid(), From = 50, To = 69, Label = "50-69" },
                new TargetAudience { Id = new Guid(), From = 70, To = 79, Label = "70-79" },
                new TargetAudience { Id = new Guid(), From = 80, To = 89, Label = "80-89" },
            });
            context.SaveChanges();
        }
    }

}
