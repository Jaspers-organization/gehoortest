using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gehoortest.application_Repository.Models.LoginData_Management
{
    public class Employee_Login
    {
        public int id { get; set; }
        public string fullname { get; set; }
        public string employee_number { get; set; }
        public string password_hash { get; set; }
        public string password_salt { get; set; }
        public string email { get; set; }
        public bool is_active { get; set; }
    }
}
