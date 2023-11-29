using BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Projections
{
    internal struct TestProjection
    {
        public int TargetAudienceId { get; set; }
        public List<ITextQuestion> TextQuestions { get; set; }
        public List<IToneAudiometryQuestion> ToneAudiometryQuestions { get; set; }
    }
}
