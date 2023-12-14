using BusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.IModels;

public interface IQuestion
{
    public int QuestionNumber { get; set; }
    public QuestionType QuestionType { get; set; }
}
