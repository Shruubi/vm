using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vm.Thread
{
    class Thread
    {
        public byte[] Program { get; set; }
        public byte[] Mem { get; set; }

        public Thread()
        {
            this.Program = null;
            this.Mem = null;
        }

        public Thread(byte[] Program, byte memSize)
        {
            this.Program = Program;
            this.Mem = new byte[memSize];
        }
    }
}
