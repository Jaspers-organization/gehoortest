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

        public void UpdateSetting(Settings settings)
        {
            settingsRepository.Update(settings);
        }

        public bool ValidateColor(Classes.Color convertedColor)
        {
            //not empty
            if (convertedColor == null)
                return false;

            if (convertedColor.Alpha <= 0)
                return false;

            return true;
        }

        public Settings GetSetting()
        {
            return settingsRepository.GetSettings();
        }
    }
}
