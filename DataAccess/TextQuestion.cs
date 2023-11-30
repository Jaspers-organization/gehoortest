using BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class TextQuestion : ITextQuestion
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public int QuestionNumber { get; set; }
        public string Question { get; set; }
        public List<string> Options { get; set; }
        public bool IsMultipleSelect { get; set; }
        public bool HasInputField { get; set; }

        public TextQuestion(int id, int testId, int questionNumber, string question, List<string> options, bool isMultipleSelect, bool hasInputField)
        {
            Id = id;
            TestId = testId;
            QuestionNumber = questionNumber;
            Options = options;
            Question = question;
            IsMultipleSelect = isMultipleSelect;
            HasInputField = hasInputField;
        }
    }
}
