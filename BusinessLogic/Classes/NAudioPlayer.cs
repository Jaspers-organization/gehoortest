using BusinessLogic.Enums;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Classes
{
    public class NAudioPlayer
    {
        public NAudioPlayer()
        {
        }
    
        public void PlayFrequency(int frequency, float volume, Ear ear)
        {
            #region part1
            //int sampleRate = 44100; // Sample rate in Hz (standard for most audio)
            //int channels = 1; // Number of channels (1 for mono, 2 for stereo)
            //int frequency = 2400; // Frequency in Hz
            //int duration = 700; // Duration in milliseconds
            //float volume = 0.5f; // Volume level (between 0.0 and 1.0)

            //Console.WriteLine($"Playing beep at {frequency} Hz for {duration} milliseconds with volume {volume * 100}%.");

            //SignalGenerator beepGenerator = new SignalGenerator(sampleRate, channels);
            //WaveOutEvent waveOut = new WaveOutEvent();

            //beepGenerator.Type = SignalGeneratorType.Sin;
            //beepGenerator.Frequency = frequency;
            //beepGenerator.Gain = volume;



            //waveOut.Init(beepGenerator);
            //waveOut.Play();

            //System.Threading.Thread.Sleep(duration);
            //waveOut.Stop();

            //// Dispose the objects manually
            //waveOut.Dispose();
            //// beepGenerator.Dispose();
            #endregion part1

            int sampleRate = 44100; // Sample rate in Hz (standard for most audio)
            int channels = 1; // Number of channels (2 for stereo)

            DirectSoundOut directSoundOut = new DirectSoundOut();
         
            SignalGenerator signalGenerator = new SignalGenerator(sampleRate, channels);

            signalGenerator.Type = SignalGeneratorType.Sin;

            signalGenerator.Frequency = frequency;

            // convert our mono ISampleProvider to stereo
            var stereo = new MonoToStereoSampleProvider(signalGenerator);
            if (ear == Ear.Left)
            {
                stereo.LeftVolume = volume; // silence in left channel
                stereo.RightVolume = 0.0f; // full volume in right channel
            }
            else
            {
                stereo.LeftVolume = 0.0f; // silence in left channel
                stereo.RightVolume = volume; // full volume in right channel
            }

            directSoundOut.Init(stereo);
            directSoundOut.Play();

            Thread.Sleep(500);
            directSoundOut.Stop();

            // Dispose the objects manually
            directSoundOut.Dispose();
        }
    }
}
