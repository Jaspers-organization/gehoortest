using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface ITargetAudience
    {
        public int Id { get; set; }
        public int From { get; set; }
        public int To { get; set; }
        public string Label { get; set; }

        
    }
}
