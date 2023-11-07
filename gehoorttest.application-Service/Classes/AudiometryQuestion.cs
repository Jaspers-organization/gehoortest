namespace gehoorttest.application_Service.Classes;

public class AudiometryQuestion : Question
{
    int Frequency { get; set; }
    int StartingVolume { get; set; }

    public AudiometryQuestion() { }

    public AudiometryQuestion(int questionNumber, int frequency, int startingVolume) : base(questionNumber)
    {
        Frequency = frequency;
        StartingVolume = startingVolume;
    }
}

