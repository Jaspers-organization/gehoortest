using BusinessLogic.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity.TestData_Management
{
    public class ToneAudiometryQuestion : IToneAudiometryQuestion
    {
        public int Frequency { get ; set ; }
        public int StartingDecibels { get ; set ; }
        public int Id { get ; set ; }
        public int QuestionNumber { get ; set ; }
    }
}
