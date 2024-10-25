using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveFileGenerator.Model
{
    public class WaveGeneratorWriterFlags
    {
        public int Frequency;
        public int LenghtInMs;
        public string FileName;
        public ChannelsEnum NumOfChannels;
    }
}
