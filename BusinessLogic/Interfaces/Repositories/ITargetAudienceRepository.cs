using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface ITargetAudienceRepository
    {
        public List<ITargetAudience> Get();
    }
}
