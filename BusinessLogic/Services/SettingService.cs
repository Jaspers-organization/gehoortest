using BusinessLogic.IRepositories;
using BusinessLogic.Models;

namespace BusinessLogic.Services
{
    public class SettingService
    {
        private ISettingsRepository settingsRepository;

        public SettingService(ISettingsRepository settingsRepository)
        {
            this.settingsRepository = settingsRepository;
        }

        public void Create(Settings settings)
        {
            settingsRepository.Create(settings);
        }

        public void Update(Settings settings)
        {
            settingsRepository.Update(settings);
          
            
        }
    }
}
