using BusinessLogic.Enums;
using BusinessLogic.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
    public class ToneAudiometryQuestion : IToneAudiometryQuestion
    {
        public int Id { get; set; }
        public int Frequency { get; set; }
        public int StartingDecibels { get; set; }
        public int QuestionNumber { get; set; }
        public QuestionType QuestionType { get; set; }

        //Database
        public int testId { get; set; }
        public virtual Test? Test { get; set; }
    }
}
