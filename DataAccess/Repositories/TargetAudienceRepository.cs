using BusinessLogic.IModels;
using BusinessLogic.IRepositories;
using BusinessLogic.Mapping;
using gehoortest_application.Repository;

namespace DataAccess.Repositories;

public class TargetAudienceRepository: ITargetAudienceRepository
{
    private readonly Repository repository;
    public TargetAudienceRepository(Repository repository)=> this.repository = repository;

    public List<ITargetAudience> GetAllAudiences()
    {
        return Mapper.MapToTargetAudiences(repository.TargetAudiences.ToList());
    }
 }
