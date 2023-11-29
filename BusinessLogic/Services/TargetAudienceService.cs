using BusinessLogic.Interfaces;
using BusinessLogic.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class TargetAudienceService
    {
        public ITargetAudienceRepository targetAudienceRepository { get; set; }

        public TargetAudienceService(ITargetAudienceRepository targetAudienceRepository)
        {
            this.targetAudienceRepository = targetAudienceRepository;
        }

        public List<ITargetAudience> GetTargetAudiences()
        {
            return targetAudienceRepository.Get();
        }
    }
}
