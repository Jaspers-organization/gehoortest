using BusinessLogic.IModels;
using BusinessLogic.IRepositories;
using BusinessLogic.Models;
using gehoortest_application.Repository;

namespace DataAccess.Repositories;

public class TargetAudienceRepository: ITargetAudienceRepository
{
    private readonly Repository repository;
    public TargetAudienceRepository(Repository repository)=> this.repository = repository;

    public List<ITargetAudience> GetAllAudiences()
    {
        return repository.TargetAudiences.Cast<ITargetAudience>().ToList();
    }
    //omdat we met interfaces werken (ook in ITargetAudienceRepository) verwacht hij dat als return value. We moeten het dus casten anders huilt de interface.
    //Of we moeten een ander manier vinden

    public List<TargetAudience> GetAllAudiences(int id)
    {
        return repository.TargetAudiences.ToList();
    }

}
