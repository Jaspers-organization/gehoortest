using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IToneAudiometryQuestion
    {
        public int QuestionNumber { get; set; }
        public int Frequency { get; set; }
        public int StartingDecibels { get; set; }
    }
}
