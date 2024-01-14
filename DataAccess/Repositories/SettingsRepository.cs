using BusinessLogic.Interfaces.Repositories;
using BusinessLogic.Models;
using gehoortest_application.Repository;

namespace DataAccess.Repositories;

public class SettingsRepository : ISettingsRepository
{
    private readonly Repository repository = new Repository();

    public void Create(Settings setting)
    {
        repository.Settings.Add(setting);
        repository.SaveChanges();
    }

    public void Update(Settings settings)
    {
        repository.Settings.Update(settings);
        repository.SaveChanges();
    }

    public Settings GetSettings()
    {
        return repository.Settings.First();
    }
}
