using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vm.Assembler
{
    class ProgramGenerator
    {
        public static void GenerateByteFile(string filename, byte[] program)
        {
            FileStream f = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write);
            f.Write(program, 0, program.Length);
            f.Close();
        }
    }
}
