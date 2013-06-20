using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vm
{
    class System
    {
        public CPU.CPU cpu { get; set; }

        public System()
        {
            this.cpu = new CPU.CPU();
        }

        public void loadProgram(byte[] program, byte memsize)
        {
            this.cpu.spawnThread(new Thread.Thread(program, memsize));
        }

        public void runProgram()
        {
            this.cpu.runThread();
        }
    }
}
