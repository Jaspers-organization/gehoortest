﻿using BusinessLogic.Models;
using BusinessLogic.Projections;

namespace BusinessLogic.IRepositories;

public interface IEmployeeRepository
{
    public Employee GetEmployeeById(Guid id);
    public List<EmployeeProjection> GetAllEmployeeProjections();
    public int AmountOfTestsPerEmployee(Guid id);
    public void SaveEmployee(Employee employee);
    public void DeleteEmployee(Employee employee);
    public void UpdateEmployee(Employee employee);
}
