namespace gehoorttest.application_Service.Classes;

public class TestProgressData
{
    public Test Test { get; set; }
    public List<TestAnswer> TestAnswers { get; set; }
    public int CurrentQuestion { get; set; }
    public int TextTestProgress { get; set; }
    public int AudimertryTestProgress { get; set; }

    public TestProgressData(Test test)
    {
        Test = test;
        TestAnswers = new List<TestAnswer>();
        CurrentQuestion = 0;
        TextTestProgress = 0;
        AudimertryTestProgress = 0;
    }

    public Question GetNextQuestion()
    {
        int countAll = Test.GetQuestionsCount();
        int countText = Test.TextQuestions.Count;
        int countAudiometry = Test.AudiometryQuestions.Count;


        if (CurrentQuestion < countAll)
        {
            if (TextTestProgress < countText)
            {
                TextTestProgress++;
                CurrentQuestion++;
                return Test.TextQuestions[TextTestProgress - 1];
            }
            else if (AudimertryTestProgress < countAudiometry)
            {
                AudimertryTestProgress++;
                CurrentQuestion++;
                return Test.AudiometryQuestions[AudimertryTestProgress - 1];
            }
            else
            {
                throw new Exception("No more questions");
            }
        }
        else
        {
            throw new Exception("No more questions");
        }
    }


}

