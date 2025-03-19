using WaveFileGenerator.Model;

namespace WaveFileGenerator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // create test file
            var filename = "file.wav";
            var filepath=  Path.Combine("d:", filename);
            var waveFlags = new WaveGeneratorWriterFlags()
            {
            
            };

        }
    }
}
