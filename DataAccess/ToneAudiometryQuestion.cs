using BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ToneAudiometryQuestion : IToneAudiometryQuestion
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public int QuestionNumber { get; set; }
        public int Frequency { get; set; }
        public int StartingDecibels { get; set; }

        public ToneAudiometryQuestion(int id, int testId, int questionNumber, int frequency, int startingDecibels)
        {
            Id = id;
            TestId = testId;
            QuestionNumber = questionNumber;
            Frequency = frequency;
            StartingDecibels = startingDecibels;
        }
    }


}
