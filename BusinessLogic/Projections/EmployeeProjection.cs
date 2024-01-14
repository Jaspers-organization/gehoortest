using BusinessLogic.Enums;

namespace BusinessLogic.Projections;

public class EmployeeProjection
{
    public Guid Id { get; set; }
    public string? EmployeeNumber { get; set; }
    public string? FullName { get; set; }
    public Role Role { get; set; }
    public string RoleString { get; set; }
    public int AmountOfTests { get; set; }
    public bool Active { get; set; }
    public string GetTranslatedRole()
    {
        switch (Role)
        {
            case Role.Administrator:
                return "Administrator";
            case Role.Employee:
                return "Medewerker";
            default:
                return Role.ToString();
        }
    }
}
