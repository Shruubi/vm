using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vm
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] program;
            System sys = new System();
            switch (args[0])
            {
                case "-c": //compiles code to bytecode and stores in .vmbf
                    program = Assembler.Assembler.assembler(args[1]);
                    Assembler.ProgramGenerator.GenerateByteFile(args[2], program);
                    break;
                case "-r": //runs compiled byte code file
                    program = Assembler.ProgramLoader.Load(args[1]);
                    sys.loadProgram(program, byte.MaxValue);
                    sys.runProgram();
                    break;
                case "-i": //compiles .svmp and runs bytecode, does not generate .vmbf
                    program = Assembler.Assembler.assembler(args[1]);
                    sys.loadProgram(program, byte.MaxValue);
                    sys.runProgram();
                    break;
                default:
                    Console.WriteLine("Invalid command line option, use either -c, -r or -i");
                    break;
            }
        }
    }
}
