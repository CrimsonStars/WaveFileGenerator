namespace WaveFileGenerator.Model
{
    public class WaveGeneratorWriterFlags
    {
        // public properties for now
        #region Properties
        public int Frequency; // value in Hz
        public int LenghtInMs; // length in miliseconds
        public int FormatType; // 1 is for PCM
        public int SampleRate;
        public int BytesPerSample;
        public int BytesPerSecond;
        public ChannelsEnum NumOfChannels;
        #endregion


        // Default constructor for 440Hz mono sample.
        // Length: 1 second (1000 ms).
        public WaveGeneratorWriterFlags()
        {
            Frequency = 440; // value in Hz
            LenghtInMs = 100; // length in miliseconds
            FormatType = 1;
            SampleRate = 0;
            BytesPerSample = 0;
            BytesPerSecond = 0;
            NumOfChannels = ChannelsEnum.Monoluar;
        }
    }
}

