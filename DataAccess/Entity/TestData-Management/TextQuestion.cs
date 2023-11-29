using BusinessLogic.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity.TestData_Management
{
    public class TextQuestion : ITextQuestion
    {
        public string Question { get ; set ; }
        public List<string> Options { get; set; }
        public bool IsMultiSelect { get ; set ; }
        public bool HasInputField { get ; set ; }
        public int Id { get ; set ; }
        public int QuestionNumber { get ; set ; }
    }
}
