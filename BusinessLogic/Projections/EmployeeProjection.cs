using BusinessLogic.Enums;

namespace BusinessLogic.Projections;

public class EmployeeProjection
{
    public Guid Id { get; set; }
    public string EmployeeNumber { get; set; }
    public string FullName { get; set; }
    public Role Role { get; set; }
}
