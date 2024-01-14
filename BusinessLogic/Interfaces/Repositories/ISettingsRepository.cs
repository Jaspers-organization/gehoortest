using BusinessLogic.Models;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface ISettingsRepository
    {
        public void Create(Settings setting);
        public void Update(Settings setting);
        public Settings GetSettings();

    }
}
