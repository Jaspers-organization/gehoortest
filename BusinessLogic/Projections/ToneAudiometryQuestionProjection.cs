using BusinessLogic.IModels;
using BusinessLogic.Interfaces;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Projections;

internal struct ToneAudiometryQuestionProjection 
{
    public int QuestionNumber { get;set; }
    public int Frequency { get;set; }
    public int StartingDecibels { get; set; }
}
