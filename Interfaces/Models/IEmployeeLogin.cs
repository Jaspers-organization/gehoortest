using Interfaces.Enums;

namespace Interfaces.Models;

public interface IEmployeeLogin : IModel
{
    public string Mail { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }
}
