namespace gehoorttest.application_Service.Classes;

public class AudiometryQuestion : Question
{
    public int Frequency { get; set; }
    public int StartingVolume { get; set; }

    public AudiometryQuestion() { }

    public AudiometryQuestion(int questionNumber, int frequency, int startingVolume) : base(questionNumber)
    {
        Frequency = frequency;
        StartingVolume = startingVolume;
    }
}

