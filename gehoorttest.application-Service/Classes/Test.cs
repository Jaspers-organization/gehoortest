namespace gehoorttest.application_Service.Classes;

public class Test
{
    public List<AudiometryQuestion> AudiometryQuestions = new();
    public List<TextQuestion> TextQuestions = new();

    public Test() { }


    public void AddTextQuestion(TextQuestion question)
    {
        TextQuestions.Add(question);
    }

    public void AddAudiometryQuestion(AudiometryQuestion question)
    {
        AudiometryQuestions.Add(question);
    }

    public int GetQuestionsCount()
    {
        return AudiometryQuestions.Count + TextQuestions.Count;
    }
}
