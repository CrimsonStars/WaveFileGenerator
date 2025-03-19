using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveFileGenerator.Model
{
    internal static class WaveGeneratorHelper
    {
        public static void GenerateFile(string filepath, WaveGeneratorWriterFlags flags)
        {
            WriteHeaderSection();
            WriteFmtSection();
            WriteDataSection();
        }

        private static void WriteDataSection()
        {

        }

        private static void WriteHeaderSection()
        {

        }

        private static void WriteFmtSection()
        {

        }
    }
}