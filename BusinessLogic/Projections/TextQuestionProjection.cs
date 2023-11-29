using BusinessLogic.Interfaces;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Projections
{
    internal struct TextQuestionProjection : ITestQuestion
    {
        public int QuestionNumber { get; set; }
        public string Question { get; set; }
        public List<string> Options { get; set; }
        public bool IsMultipleSelect { get; set; }
        public bool HasInputField { get; set; }
    }
}
