using BusinessLogic.DataMappings;
using BusinessLogic.Enums;
using BusinessLogic.IModels;
using BusinessLogic.Services;
namespace Tests;

public class AudioQuestionsTest
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

    [Theory]
    [InlineData(null, QuestionType.TextQuestion, typeof(ArgumentNullException))] 
    public void GetNewHighestQuestionNumber_ThrowsArgumentNullException_WhenTestIsNull()
    {
        // Arrange
        ITest test = null;
        var questionType = QuestionType.TextQuestion;

        // Act and Assert
        Assert.Throws<ArgumentNullException>(() => TestService.GetNewHighestQuestionNumber(test, questionType));
    }
}
