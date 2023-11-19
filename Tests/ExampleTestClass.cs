namespace Tests;
using BusinessLogic.DataMappings;


public class ExampleTestClass
{
    [Theory]
    [InlineData(3000, 20, 50)]
    [InlineData(500, 20, 50)]
    [InlineData(8000, 0, 0)]
    [InlineData(125, 0, 0)]
    public void GetHearingLossRange_ReturnsValidRange(int frequency, int expectedMin, int expectedMax)
    {
        
        (int min, int max) result = ToneAudiometryMapping.GetHearingLossRange(frequency);

        Assert.Equal((expectedMin, expectedMax), result);
    }
}