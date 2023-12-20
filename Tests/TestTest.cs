using BusinessLogic.Classes;
using BusinessLogic.Enums;

namespace Tests
{
    public class TestTest
    {
        [Fact]
        public void It_SetsEarVolume()
        {
            NAudioPlayer nAudioPlayer = new NAudioPlayer();
            nAudioPlayer.SetEarVolume(Ear.Left, 40);

            Assert.Equal(40, nAudioPlayer.Stereo.LeftVolume);
            Assert.Equal(0.0f, nAudioPlayer.Stereo.RightVolume);
        }

        [Fact]
        public void It_Determines_Next_Lower_Decibel()
        {
            // Arrange
            Decibel decibelCalculator = new Decibel();
            decibelCalculator.PlayDecibel = 50;
           
            // Act
            decibelCalculator.DetermineNextDecibel("true");

            // Assert
            Assert.Equal(40, decibelCalculator.PlayDecibel); 
        }

        [Fact]
        public void It_Determines_Next_Higher_Decibel()
        {
            // Arrange
            Decibel decibelCalculator = new Decibel();
            decibelCalculator.PlayDecibel = 50;

            // Act
            decibelCalculator.DetermineNextDecibel("false");

            // Assert
            Assert.Equal(55, decibelCalculator.PlayDecibel); 
            Assert.Equal(55, decibelCalculator.LowestDecibel); 
            Assert.True(decibelCalculator.FinalDecibelToPlay);
        }

    }
}
