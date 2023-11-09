using DataAccess.Entities;
using Interfaces.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Repositories;

public class TargetAudienceRepository : Repository
{
    public TargetAudienceRepository(string connectionString) : base(connectionString)
    {
    }

    public List<ITargetAudience> GetAllAgesBelow(byte amount)
    {
        return Get<ITargetAudience>(t => t.From < amount);
    }
}
