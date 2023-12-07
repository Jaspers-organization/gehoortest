using BusinessLogic.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models;

public class TextQuestionOption : ITextQuestionOption
{
    public int Id { get ; set ; }

    public string Option { get ; set ; }

    public int TextQuestionId {  get ; set ; }
    public virtual TextQuestion? TextQuestion { get ; set ; }

}
