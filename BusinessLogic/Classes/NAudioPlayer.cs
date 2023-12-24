using BusinessLogic.Enums;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace BusinessLogic.Classes
{
    public class NAudioPlayer
    {
        public int SampleRate;
        public int Channels;
        public SignalGenerator SignalGenerator;
        public MonoToStereoSampleProvider Stereo;
        public NAudioPlayer()
        {
            SampleRate = 44100;
            Channels = 1; 
            SignalGenerator = new SignalGenerator(SampleRate, Channels);
            SignalGenerator.Type = SignalGeneratorType.Sin;
            Stereo = new MonoToStereoSampleProvider(SignalGenerator);
        }
        public void PlayFrequency(int frequency, float volume, Ear ear)
        {
            DirectSoundOut directSoundOut = new DirectSoundOut();
            SignalGenerator.Frequency = frequency;
            SetEarVolume(ear, volume);
            directSoundOut.Init(Stereo);
            directSoundOut.Play();
            Thread.Sleep(500);
            directSoundOut.Stop();
            directSoundOut.Dispose();
        }
        public void SetEarVolume(Ear ear, float volume)
        {
            if (ear == Ear.Left)
            {
                Stereo.LeftVolume = volume; // full volume in left channel
                Stereo.RightVolume = 0.0f; // silence in right channel
            }
            else
            {
                Stereo.LeftVolume = 0.0f; // silence in left channel
                Stereo.RightVolume = volume; // full volume in right channel
            }
        }
    }
}
