namespace BusinessLogic.Classes;

public class FrequencyMap
{
    public int Frequency { get; }
    public int StartingDecibels { get; }
    public (int Min, int Max) HearingLoss { get; }
    public string FrequencyString { get; }

    public FrequencyMap(int frequency, int startingDecibels, (int, int) hearingLoss, string frequencyString)
    {
        Frequency = frequency;
        StartingDecibels = startingDecibels;
        HearingLoss = hearingLoss;
        FrequencyString = frequencyString;
    }
}
