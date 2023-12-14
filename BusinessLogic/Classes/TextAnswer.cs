namespace BusinessLogic.Classes;

public class TextAnswer
{
    public int Question { get; set; }
    public List<string> Options { get;set; }
    public List<string> Answers { get; set; }

    public TextAnswer(int question, List<string> options, List<string> answers)
    {
        Question = question;
        Options = options;
        Answers = answers;
    }
}
