using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vm.Assembler
{
    class ProgramLoader
    {
        public static byte[] Load(string filename)
        {
            FileStream f = new FileStream(filename, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[f.Length];
            f.Read(buffer, 0, buffer.Length);
            return buffer;
        }
    }
}
