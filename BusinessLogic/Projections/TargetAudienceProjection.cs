using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Projections;

public class TargetAudienceProjection
{
    public Guid Id { get; set; }
    public string Label { get; set; }
    public string From { get; set; }
    public string To { get; set; }
    public int AmountOfTests { get; set; }
}
