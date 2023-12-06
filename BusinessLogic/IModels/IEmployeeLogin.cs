using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.IModels;

public interface IEmployeeLogin: IModel
{
    string Email {  get; set; }
    string Password { get; set; }
    bool Active { get; set; }
}
