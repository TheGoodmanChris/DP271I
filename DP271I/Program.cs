using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Challenge from: https://www.reddit.com/r/dailyprogrammer/comments/4o74p3/20160615_challenge_271_intermediate_making_waves/
//You will be given a sample rate in Hz (bytes per second), followed by a duration for each note (milliseconds), and then finally a string of notes represented as the letters A through G (and _ for rest).
//You should output a string of bytes (unsigned 8 bit integers) either as a binary stream, or to a binary file. These bytes should represent the waveforms for the frequencies of the notes.


namespace DP271I
{
    class WaveformMaker
    {

        static void Main(string[] args)
        {
            createWaveform(8000, 300, "ABCDEFG_GFEDCBA", args[0]);
        }

        /// <summary>
        /// Outputs a string of bytes (unsigned 8 bit integers) as a binary file
        /// </summary>
        /// <param name="hz">The sample rate in bytes per second.</param>
        /// <param name="duration">How long each note should be played in milliseconds</param>
        /// <param name="notes">The notes to be played.</param>
        private static void createWaveform(int hz, int duration, string notes, string outputFilePath)
        {
            List<Byte> waveform = new List<byte>();

            foreach (char note in notes)
            {
                int numSamples = duration * hz / 1000;
                //fill out the wave form one sample at a time
                for (int sampleIndex = 0; sampleIndex < numSamples; sampleIndex++)
                {
                    //
                    byte sample = (byte)Math.Floor(127.5 * Math.Sin(2 * Math.PI * sampleIndex * noteToFreq(note) / hz ) + 127.5);
                    waveform.Add(sample);
                }
            }
            System.IO.FileStream fileStream = new System.IO.FileStream(outputFilePath, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            fileStream.Write(waveform.ToArray(), 0, waveform.Count);
        }

        private static float noteToFreq(char note)
        {
            switch (note)
            {
                case 'A':
                    return 440.0f;
                case 'B':
                    return 493.88f;
                case 'C':
                    return 523.25f;
                case 'D':
                    return 587.33f;
                case 'E':
                    return 659.25f;
                case 'F':
                    return 698.46f;
                case 'G':
                    return 783.99f;
                case '_':
                    return 0.0f;
                default:
                    return 0f;
            }
        }
    }
}
