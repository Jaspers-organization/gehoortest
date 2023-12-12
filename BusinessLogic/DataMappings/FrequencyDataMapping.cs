namespace BusinessLogic.DataMappings;

public class FrequencyDataMapping
{
    private struct FrequencyMapping
    {
        internal int Frequency { get; set; }
        internal int StartingDecibels { get; set; }
        internal int HearingLossRangeMin { get; set; }
        internal int HearingLossRangeMax { get; set; }
    }

    private static List<FrequencyMapping> frequencyMappings = new ()
    {
        new FrequencyMapping() { Frequency = 125, StartingDecibels = 30, HearingLossRangeMin = 0, HearingLossRangeMax = 0, },
        new FrequencyMapping() { Frequency = 250, StartingDecibels = 30, HearingLossRangeMin = 10, HearingLossRangeMax = 40, },
        new FrequencyMapping() { Frequency = 500, StartingDecibels = 30, HearingLossRangeMin = 20, HearingLossRangeMax = 50, },
        new FrequencyMapping() { Frequency = 750, StartingDecibels = 30, HearingLossRangeMin = 20, HearingLossRangeMax = 50, },
        new FrequencyMapping() { Frequency = 1000, StartingDecibels = 30, HearingLossRangeMin = 20, HearingLossRangeMax = 50, },
        new FrequencyMapping() { Frequency = 1500, StartingDecibels = 30, HearingLossRangeMin = 20, HearingLossRangeMax = 50, },
        new FrequencyMapping() { Frequency = 2000, StartingDecibels = 30, HearingLossRangeMin = 20, HearingLossRangeMax = 50, },
        new FrequencyMapping() { Frequency = 3000, StartingDecibels = 30, HearingLossRangeMin = 20, HearingLossRangeMax = 50, },
        new FrequencyMapping() { Frequency = 4000, StartingDecibels = 30, HearingLossRangeMin = 15, HearingLossRangeMax = 45, },
        new FrequencyMapping() { Frequency = 6000, StartingDecibels = 30, HearingLossRangeMin = 10, HearingLossRangeMax = 40, },
        new FrequencyMapping() { Frequency = 8000, StartingDecibels = 30, HearingLossRangeMin = 0, HearingLossRangeMax = 0, },
    };

    public static int GetMinHearingLossRange(int frequency)
    {
        return frequencyMappings.Find(x => x.Frequency == frequency).HearingLossRangeMin;
    }

    public static int GetMaxHearingLossRange(int frequency)
    {
        return frequencyMappings.Find(x => x.Frequency == frequency).HearingLossRangeMax;
    }
}
