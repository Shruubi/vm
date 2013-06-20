using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vm.Assembler
{
    //this class is simply so I don't have to write byte code for every program
    class Assembler
    {
        public static byte[] assembler(string filename)
        {
            FileStream f = new FileStream(filename, FileMode.Open, FileAccess.Read);
            StreamReader r = new StreamReader(f);
            List<string> ops = new List<string>();
            List<byte> retlist = new List<byte>();

            while (!r.EndOfStream)
            {
                var line = r.ReadLine();
                string[] split = line.Split(' ');
                foreach (string s in split)
                {
                    ops.Add(s);
                }
            }

            r.Close();
            f.Close();

            for (int i = 0; i < ops.Count; i++)
            {
                switch (ops[i])
                {
                    case "add":
                        retlist.Add(0);
                        break;
                    case "sub":
                        retlist.Add(1);
                        break;
                    case "mul":
                        retlist.Add(2);
                        break;
                    case "div":
                        retlist.Add(3);
                        break;
                    case "mod":
                        retlist.Add(4);
                        break;
                    case "clt":
                        retlist.Add(5);
                        break;
                    case "cgt":
                        retlist.Add(6);
                        break;
                    case "ceq":
                        retlist.Add(7);
                        break;
                    case "cle":
                        retlist.Add(8);
                        break;
                    case "cge":
                        retlist.Add(9);
                        break;
                    case "str":
                        retlist.Add(10);
                        break;
                    case "streg":
                        retlist.Add(11);
                        break;
                    case "ldreg":
                        retlist.Add(12);
                        break;
                    case "stnxt":
                        retlist.Add(13);
                        break;
                    case "jmp":
                        retlist.Add(14);
                        break;
                    default:
                        retlist.Add(Convert.ToByte(ops[i]));
                        break;
                }
            }

            return retlist.ToArray();
        }
    }
}
