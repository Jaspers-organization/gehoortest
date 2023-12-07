using BusinessLogic.IModels;
using BusinessLogic.IRepositories;
using BusinessLogic.Models;
using DataAccess.Mapping;
using gehoortest_application.Repository;

namespace DataAccess.Repositories;

public class TargetAudienceRepository : ITargetAudienceRepository
{
    private readonly Repository repository;
    public TargetAudienceRepository(Repository repository) => this.repository = repository;

    public List<ITargetAudience> GetAllAudiences()
    {
        return Mapper.MapToTargetAudiences(repository.TargetAudiences.ToList());
    }

    public void FillTargetAudiences()
    {

        repository.TargetAudiences.AddRange(Mapper.MapToTargetAudiencesDTO(new List<ITargetAudience>
        {
            new TargetAudience { From = 0, To = 18, Label = "0-18" },
            new TargetAudience { From = 19, To = 29, Label = "19-29" },
            new TargetAudience { From = 30, To = 49, Label = "30-49" },
            new TargetAudience { From = 50, To = 69, Label = "50-69" },
            new TargetAudience { From = 70, To = 79, Label = "70-79" },
          new TargetAudience { From = 80, To = 89, Label = "80-89" },
        }));
        repository.SaveChanges();


    }
}
