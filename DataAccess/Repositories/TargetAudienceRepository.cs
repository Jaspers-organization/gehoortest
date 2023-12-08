using BusinessLogic.IModels;
using BusinessLogic.IRepositories;
using BusinessLogic.Models;
using gehoortest_application.Repository;

namespace DataAccess.Repositories;

public class TargetAudienceRepository : ITargetAudienceRepository
{
    private readonly Repository repository = new Repository();

    public List<TargetAudience> GetAllAudiences()
    {
        using (Repository context = repository)
        {
            
            return context.TargetAudiences.OrderBy(audience => audience.From).ToList();

        }
    }

    public void FillTargetAudiences()
    {
        using (Repository context = repository)
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
