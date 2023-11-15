namespace BusinessLogic.Classes;

public class TextQuestion : Question
{
    public string Question { get; set; }
    public List<string> Options { get; set; }
    public bool IsMultipleSelect { get; set; }

    public bool HasInputField { get; set; }

    public TextQuestion() { }

    public TextQuestion(int questionNumber, string question, List<string> options, bool isMultipleSelect, bool hasInputField) : base(questionNumber)
    {
        Question = question;
        Options = options;
        IsMultipleSelect = isMultipleSelect;
        HasInputField = hasInputField;
    }
}

