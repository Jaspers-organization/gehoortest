using BusinessLogic.Enums;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace AudioPlayer
{
    public class AudioPlayer
    {
        public int SampleRate;
        public int Channels;
        public SignalGenerator SignalGenerator;
        public MonoToStereoSampleProvider Stereo;
        public AudioPlayer()
        {
            SampleRate = 44100;
            Channels = 1;
            SignalGenerator = new SignalGenerator(SampleRate, Channels);
            SignalGenerator.Type = SignalGeneratorType.Sin;
            Stereo = new MonoToStereoSampleProvider(SignalGenerator);
        }
        //public void PlayFrequency(int frequency, float volume, Ear ear)
        //{
        //    DirectSoundOut directSoundOut = new DirectSoundOut();
        //    SignalGenerator.Frequency = frequency;
        //    SetEarVolume(ear, volume);
        //    directSoundOut.Init(Stereo);
        //    directSoundOut.Play();
        //    Thread.Sleep(500);
        //    directSoundOut.Stop();
        //    directSoundOut.Dispose();
        //}
        public void PlayFrequency(int frequency, int? decibels, Ear ear)
        {
            float decibelFloat = (float)decibels;

            DirectSoundOut directSoundOut = new DirectSoundOut();
            SignalGenerator.Frequency = frequency;
            SetEarDecibel(ear, decibelFloat);
            directSoundOut.Init(Stereo);
            directSoundOut.Play();
            Thread.Sleep(500);

            // Somethings gives unexpected error:
            // System.PlatformNotSupportedException: 'Thread abort is not supported on this platform.'
            directSoundOut.Stop();

            directSoundOut.Dispose();
        }

        private void SetEarDecibel(Ear ear, float decibels)
        {
            if (ear == Ear.Left)
            {
                Stereo.LeftVolume = decibels; // full volume in left channel
                Stereo.RightVolume = 0.0f; // silence in right channel
            }
            else
            {
                Stereo.LeftVolume = 0.0f; // silence in left channel
                Stereo.RightVolume = decibels; // full volume in right channel
            }
        }
    }
}
