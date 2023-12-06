using BusinessLogic.DataMappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests;

public class TextQuestionTests
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
