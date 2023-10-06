using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gehoortest.application_Repository.Models.LoginData_Management
{
    public class Client_Login
    {
        public int id { get; set; }
        public string password_hash { get; set; }
        public string password_salt { get; set; }
        public string email { get; set; }
        public bool is_active { get; set; }
    }
}
