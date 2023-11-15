namespace gehoorttest.application_Service.DataMappings;

internal class ToneAudiometryMapping
{
    private struct FrequencyMapping
    {
        internal int Frequency { get; set; }
        internal int Decibels { get; set; }
    }

    private struct HearingLossRange
    {
        internal int Frequency { get; set; }
        internal int Min { get; set; }
        internal int Max { get; set; }
    }

    private static List<FrequencyMapping> frequencyMappings = new ()
    {
        new FrequencyMapping() { Frequency = 125, Decibels = 30 },
        new FrequencyMapping() { Frequency = 250, Decibels = 30 },
        new FrequencyMapping() { Frequency = 500, Decibels = 30 },
        new FrequencyMapping() { Frequency = 750, Decibels = 30 },
        new FrequencyMapping() { Frequency = 1000, Decibels = 30 },
        new FrequencyMapping() { Frequency = 1500, Decibels = 30 },
        new FrequencyMapping() { Frequency = 2000, Decibels = 30 },
        new FrequencyMapping() { Frequency = 3000, Decibels = 30 },
        new FrequencyMapping() { Frequency = 4000, Decibels = 30 },
        new FrequencyMapping() { Frequency = 6000, Decibels = 30 },
        new FrequencyMapping() { Frequency = 8000, Decibels = 30 },
    };

    private static List<HearingLossRange> hearingLossRanges = new ()
    {
        new HearingLossRange() { Frequency = 125, Min = 0, Max = 0 },
        new HearingLossRange() { Frequency = 250, Min = 10, Max = 40 },
        new HearingLossRange() { Frequency = 500, Min = 20, Max = 50 },
        new HearingLossRange() { Frequency = 750, Min = 20, Max = 50 },
        new HearingLossRange() { Frequency = 1000, Min = 20, Max = 50 },
        new HearingLossRange() { Frequency = 1500, Min = 20, Max = 50 },
        new HearingLossRange() { Frequency = 2000, Min = 20, Max = 50 },
        new HearingLossRange() { Frequency = 3000, Min = 20, Max = 50 },
        new HearingLossRange() { Frequency = 4000, Min = 15, Max = 45 },
        new HearingLossRange() { Frequency = 6000, Min = 10, Max = 40 },
        new HearingLossRange() { Frequency = 8000, Min = 0, Max = 0 },
    };

    internal static int GetStartingDecibels(int frequency)
    {
        AssertValidFrequency(frequency);

        FrequencyMapping mapping = frequencyMappings.Find(x => x.Frequency == frequency);
        return mapping.Decibels;
    }

    internal static (int min, int max) GetHearingLossRange(int frequency)
    {
        AssertValidFrequency(frequency);

        HearingLossRange range = hearingLossRanges.Find(x => x.Frequency == frequency);
        return (range.Min, range.Max);
    }

    private static void AssertValidFrequency(int frequency)
    {
        if (frequency < 125 || frequency > 8000) throw new ArgumentException();
    }
}
