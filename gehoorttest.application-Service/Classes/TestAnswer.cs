namespace gehoorttest.application_Service.Classes;

public class TestAnswer
{
    public int QuestionNumber { get; set; }
    public string Answer { get; set; }

    public TestAnswer(int questionNumber, string answer)
    {
        QuestionNumber = questionNumber;
        Answer = answer;
    }
}
