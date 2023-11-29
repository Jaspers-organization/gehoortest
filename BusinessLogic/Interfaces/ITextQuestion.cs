using BusinessLogic.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface ITextQuestion : IModel
    {
        public int QuestionNumber { get; }
        public string Question {  get; }
        public List<string> Options { get; }
        public bool IsMultipleSelect { get; set; }
        public bool HasInputField { get; set; }

    }
}
