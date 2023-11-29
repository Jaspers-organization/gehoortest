using BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Test : ITest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ITargetAudience TargetAudience { get; set; }
        public bool Active { get; set; }
        public string Title { get; set; }
        public List<ITextQuestion> TextQuestions { get; set; }
        public List<IToneAudiometryQuestion> ToneAudiometryQuestions { get; set; }
    }
}
