using BusinessLogic.Interfaces.Models;

namespace BusinessLogic.Models;

public class Settings : IModel
{
    public Guid Id { get; set; }
    public string Color { get;set; }
}
