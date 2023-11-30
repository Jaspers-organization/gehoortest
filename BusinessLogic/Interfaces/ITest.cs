using BusinessLogic.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface ITest : IModel
    {
        public string Title { get; set; }
        ITargetAudience TargetAudience { get; set; }
        public bool Active { get; set; }
        public List<ITextQuestion> TextQuestions { get; set; }
        public List<IToneAudiometryQuestion> ToneAudiometryQuestions { get; set; }


    }
}
