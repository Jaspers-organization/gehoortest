using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.IModels
{
    public interface ITextQuestionOption: IModel
    {
        public string Option { get; set; }

    }
}
