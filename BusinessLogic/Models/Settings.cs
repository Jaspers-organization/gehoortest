using BusinessLogic.IModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
    public class Settings : IModel
    {
        public Guid Id { get; set; }
        public string Color { get;set; }
        public string Logo { get; set; }
        public int TestInactiveTime { get;set; }
        public int LoginInactiveTime { get;set; }
    }
}
