using gehoortest_application.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace gehoortest.application_Repository.Models.TestData_Management;

public class TargetAudienceRepository : Repository
{
    public TargetAudienceRepository(string connectionString) : base(connectionString)
    {
    }

    public List<TargetAudience> GetAllAgesBelow(byte amount)
    {
        return Get<TargetAudience>(t => t.From < amount);
    }
}
