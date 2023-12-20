using BusinessLogic.IRepositories;
using BusinessLogic.Models;
using gehoortest_application.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly Repository repository = new();

    public Employee GetEmployeeById(Guid id)
    {
        return repository.Employees.FirstOrDefault(e => e.Id == id);
    }
    //public void Insert()
    //{
    //    using (var context = repository)
    //    {
    //        repository.Employees.Add(new Employee { Id = new Guid(), EmployeeNumber = "333", FirstName = "Dinny", Infix = "van", LastName = "Huizen" });
    //        repository.SaveChanges();
    //    }
    //}
}
