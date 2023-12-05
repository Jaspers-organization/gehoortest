using BusinessLogic.IModels;
using BusinessLogic.IRepositories;
using DataAccess.Models.TestData_Management;
using gehoortest_application.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    //public List<TargetAudience> GetAllAudiences()
    //{
    //    return repository.TargetAudiences.ToList();
    //}
}
