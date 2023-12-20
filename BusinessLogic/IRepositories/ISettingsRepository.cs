using BusinessLogic.Models;

namespace BusinessLogic.IRepositories
{
    public interface ISettingsRepository
    {
        public void Create(Settings setting);
        public void Update(Settings setting);
       
    }
}
