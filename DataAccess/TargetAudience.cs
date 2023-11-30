using BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class TargetAudience : ITargetAudience
    {
        public int Id { get; set; }
        public int From { get; set; }
        public int To { get; set; }
        public string Label { get; set; }

        public TargetAudience()
        {
        }

        public TargetAudience(int id, int from, int to, string label)
        {
            Id = id;
            From = from;
            To = to;
            Label = label;
        }


    }
}
