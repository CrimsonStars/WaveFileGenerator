namespace WaveFileGenerator
{
    using WaveFileGenerator.Model;

    internal class Program
    {
        private static string testFileAName = "Mio_Mao_la_la_la_la_la.wav";
        private static string testFileBName = "440hz_second.wav";

        public static class WaveFormatsFlag
        {
            public const int UNSUPPORTED = 0;
            public const int MONO = 1;
            public const int STEREO = 2;
        };

        private static void Main(string[] args)
        {
            // create test file
            var filename = "file.wav";
            var filepath = Path.Combine("d:", filename);
            var waveFlags = new WaveGeneratorWriterFlags()
            {
            };

            ReadSampleWavefile(testFileBName);
        }

        private static void CreateWaveFile(string filename)
        {
            var path = Path.Combine("d:", filename);

            var fileExists = File.Exists(path);
            Console.WriteLine(
                $"FILE '{path}' - {(fileExists ? "is valid" : "is INVALID")}");
        }

        private static void ReadSampleWavefile(string filename)
        {
            var pa = "..\\..\\..\\Resources\\Sample_files";

            var path = Path.Combine(pa, filename);
            Console.Write($"FILE '{path}' - ");
            var debugMode = true;
            var channels = WaveFormatsFlag.UNSUPPORTED;

            if (File.Exists(path))
            {
                Console.WriteLine("OK...");

                short maxValue = 0;
                using (var bs = new BinaryReader(File.OpenRead(path)))
                {
                    var chunkId = bs.ReadBytes(4);
                    var chunkSize = BitConverter.ToInt32(bs.ReadBytes(4)); // in bytes
                    var format = bs.ReadBytes(4);

                    Console.WriteLine($"ChunkId:   {BytesToString(chunkId)}");
                    Console.WriteLine($"ChunkSize: {chunkSize} [bytes] ({Math.Round(chunkSize / 1024.0, 1)} kB -> {Math.Round(chunkSize / 1024.0 / 1024.0, 1)} MB)");
                    Console.WriteLine($"Format:    {BytesToString(format)}");
                    Console.WriteLine();

                    Console.WriteLine("--- SUBCHUNK_1 SECTION ---");
                    var subchunk1id = bs.ReadBytes(4);
                    var subchunk1size = bs.ReadBytes(4);
                    var audioFormat = bs.ReadBytes(2);
                    var numChannels = BitConverter.ToInt16(bs.ReadBytes(2));
                    var sampleRate = bs.ReadBytes(4);
                    var byteRate = bs.ReadBytes(4);
                    var blockAlign = bs.ReadBytes(2);
                    var bitsPerSample = bs.ReadBytes(2);

                    if (numChannels == 1)
                        channels = WaveFormatsFlag.MONO;
                    else if (numChannels == 2)
                        channels = WaveFormatsFlag.STEREO;

                    /* BITS, BYTES, ETC...
                     *      - 1 byte = 4 bit
                     *      - 2 bytes = 1 HEX value (from 00 to FF)
                     *       - 16-bit int is two hex values
                     *      - example: FF -> 2 bytes -> 1111 1111
                     *      - example: FF 0A -> 4 bytes -> 1111 1111 0000 1010
                     *      - 1 MB = 1024 kB
                     *      - 1 kB = 2024 B
                     */

                    if (debugMode) Console.WriteLine($"Stream position: {bs.BaseStream.Position}");
                    Console.WriteLine($"SUBCHUNK1 ID:       {BytesToString(subchunk1id)}");
                    Console.WriteLine($"SUBCHUNK1 SIZE:     {BitConverter.ToInt32(subchunk1size)}");
                    Console.WriteLine($"AUDIO FORMAT:       {BitConverter.ToInt16(audioFormat)} (if 1 then PCM)");
                    Console.WriteLine($"SAMPLE RATE:        {BitConverter.ToInt32(sampleRate)} Hz");
                    Console.WriteLine($"NUM OF CHANNELS:    {channels} (1 - mono, 2 - stereo, etc.)");
                    Console.WriteLine($"BYTE RATE:          {BitConverter.ToInt32(byteRate)}");
                    Console.WriteLine($"BLOCK ALIGN:        {BitConverter.ToInt16(blockAlign)}");
                    Console.WriteLine($"BITS PER SAMPLE:    {BitConverter.ToInt16(bitsPerSample)} bits");
                    Console.WriteLine();

                    if (channels != WaveFormatsFlag.UNSUPPORTED)
                    {
                        Console.WriteLine("--- SUBCHUNK_2 SECTION ---");
                        var subchunk2id = bs.ReadBytes(4);
                        var subchunk2size = bs.ReadBytes(4);//== NumSamples * NumChannels * BitsPerSample/8

                        if (debugMode) Console.WriteLine($"Stream position: {bs.BaseStream.Position}");
                        Console.WriteLine($"SUBCHUNK2 ID:       {BytesToString(subchunk2id)}");
                        Console.WriteLine($"SUBCHUNK2 SIZE:     {BitConverter.ToInt32(subchunk1size)}");
                        Console.WriteLine();

                        // data
                        byte[] data;
                        Console.WriteLine("--- DATA ---");
                        //while ((data = bs.ReadBytes(4)) != null)
                        for (int i = 0; i < 100; i++)
                        {
                            data = bs.ReadBytes(2);
                            var res = BitConverter.ToInt16(data);
                            Console.WriteLine($"{i},\t{res.ToString()}");
                            maxValue = res > maxValue ? res : maxValue;
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("NOT FOUND!");
            }
        }

        private static string BytesToString(byte[] data)
        {
            var result = "'";

            foreach (var b in data)
            {
                result += (char)b;
            }

            return string.Concat(result, "'");
        }
    }
}