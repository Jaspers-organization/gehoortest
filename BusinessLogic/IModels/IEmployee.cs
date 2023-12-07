namespace BusinessLogic.IModels;

public interface IEmployee: IModel
{
    public string EmployeeNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Infix { get; set; }
    public string Fullname { get; }
}
