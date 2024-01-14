namespace BusinessLogic.Classes;

public class FrequencyMapping
{
    public static readonly List<FrequencyMap> Frequencies = new()
    {
        new FrequencyMap(250, 30, (10, 40), "250"),
        new FrequencyMap(500, 30, (20, 50), "500"),
        new FrequencyMap(750, 30, (20, 50), "750"),
        new FrequencyMap(1000, 30, (20, 50), "1k"),
        new FrequencyMap(1500, 30, (20, 50), "1.5k"),
        new FrequencyMap(2000, 30, (20, 50), "2k"),
        new FrequencyMap(3000, 30, (20, 50), "3k"),
        new FrequencyMap(4000, 30, (15, 45), "4k"),
        new FrequencyMap(6000, 30, (10, 40), "6k"),
    };
}
