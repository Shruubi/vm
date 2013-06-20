using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vm.CPU
{
    class CPU
    {
        public Dictionary<string, byte> Registers { get; set; }
        public Thread.Thread CurrentThread { get; set; }

        public CPU()
        {
            this.Registers = new Dictionary<string, byte>();
            
            //general
            this.Registers.Add("A", 0);
            this.Registers.Add("B", 0);
            this.Registers.Add("C", 0);
            this.Registers.Add("D", 0);

            //instruction pointer
            this.Registers.Add("IP", 0);

            //current instruction
            this.Registers.Add("CP", 0);

            //next free memory address pointer
            this.Registers.Add("FP", 0);

            //bool flag, default value is byte.MaxValue which notifies that BF has not been assigned
            this.Registers.Add("BF", byte.MaxValue);

            //CPU should start with no current threads
            this.CurrentThread = null;
        }

        public void spawnThread(Thread.Thread t)
        {
            this.CurrentThread = t;
            this.Registers["FP"] = 0;
            this.Registers["IP"] = 0;
            this.Registers["CP"] = 0;
        }

        public void getNextInstruction()
        {
            this.Registers["CP"] = this.CurrentThread.Program[this.Registers["IP"]];
            this.Registers["IP"]++;
        }

        public byte getAtIP()
        {
            return this.CurrentThread.Program[this.Registers["IP"]];
        }

        public void assignToFP(byte val)
        {
            this.CurrentThread.Mem[this.Registers["FP"]] = val;
            this.Registers["FP"]++;
        }

        public void runThread()
        {
            while (this.Registers["IP"] < this.CurrentThread.Program.Length)
            {
                ExecuteInstruction();
            }
        }

        /*
         * instruction breakdown
         * 0 - Add addrA addrB - adds values in the supplied addresses and stores it in reg A
         * 1 - Sub addrA addrB - same as above
         * 2 - Mul addrA addrB
         * 3 - Div addrA addrB
         * 4 - Mod addrA addrB
         * 5 - LT addrA addrB - evaluates whether value at addrA is less than value at addrB and sets the bool flag (BF)
         * 6 - GT addrA addrB
         * 7 - EQ addrA addrB
         * 8 - LTE addrA addrB
         * 9 - GTE addrA addrB
         * 10 - Str val addr - stores val into the supplied address
         * 11 - StReg addr reg - stores val into reg, reg can only be between 1, 2, 3 or 4
         * 12 - LdReg addr reg - loads the value from reg into specified address, reg must be 1,2,3 or 4
         * 13 - StrNxt flag val - stores val in the next available free block in memory, flag represents what type (1 - addr, 2 - reg, 3 - val) 
         * 14 - Jmp lineT lineF - jumps the instruction pointer to addrT if bool flag is true, otherwise to addrF, resets bool flag after jump
         */
        public void ExecuteInstruction()
        {
            //fetch values
            byte inst = getAtIP();
            getNextInstruction();
            byte p1 = getAtIP();
            getNextInstruction();
            byte p2 = getAtIP();
            getNextInstruction();

            switch (inst)
            {
                case 0:
                    this.Registers["A"] = Convert.ToByte(this.CurrentThread.Mem[p1] + this.CurrentThread.Mem[p2]);
                    break;
                case 1:
                    this.Registers["A"] = Convert.ToByte(this.CurrentThread.Mem[p1] - this.CurrentThread.Mem[p2]);
                    break;
                case 2:
                    this.Registers["A"] = Convert.ToByte(this.CurrentThread.Mem[p1] * this.CurrentThread.Mem[p2]);
                    break;
                case 3:
                    this.Registers["A"] = Convert.ToByte(this.CurrentThread.Mem[p1] / this.CurrentThread.Mem[p2]);
                    break;
                case 4:
                    this.Registers["A"] = Convert.ToByte(this.CurrentThread.Mem[p1] % this.CurrentThread.Mem[p2]);
                    break;
                case 5:
                    if(Convert.ToInt32(this.CurrentThread.Mem[p1]) < Convert.ToInt32(this.CurrentThread.Mem[p2]))
                        this.Registers["BF"] = 1;
                    else
                        this.Registers["BF"] = 0;
                    break;
                case 6:
                    if(Convert.ToInt32(this.CurrentThread.Mem[p1]) > Convert.ToInt32(this.CurrentThread.Mem[p2]))
                        this.Registers["BF"] = 1;
                    else
                        this.Registers["BF"] = 0;
                    break;
                case 7:
                    if(Convert.ToInt32(this.CurrentThread.Mem[p1]) == Convert.ToInt32(this.CurrentThread.Mem[p2]))
                        this.Registers["BF"] = 1;
                    else
                        this.Registers["BF"] = 0;
                    break;
                case 8:
                    if(Convert.ToInt32(this.CurrentThread.Mem[p1]) <= Convert.ToInt32(this.CurrentThread.Mem[p2]))
                        this.Registers["BF"] = 1;
                    else
                        this.Registers["BF"] = 0;
                    break;
                case 9:
                    if(Convert.ToInt32(this.CurrentThread.Mem[p1]) >= Convert.ToInt32(this.CurrentThread.Mem[p2]))
                        this.Registers["BF"] = 1;
                    else
                        this.Registers["BF"] = 0;
                    break;
                case 10:
                    this.CurrentThread.Mem[p2] = p1;
                    break;
                case 11:
                    switch (p2)
                    {
                        case 1:
                            this.Registers["A"] = this.CurrentThread.Mem[p1];
                            break;
                        case 2:
                            this.Registers["B"] = this.CurrentThread.Mem[p1];
                            break;
                        case 3:
                            this.Registers["C"] = this.CurrentThread.Mem[p1];
                            break;
                        case 4:
                            this.Registers["D"] = this.CurrentThread.Mem[p1];
                            break;
                        default:
                            break;
                    }
                    break;
                case 12:
                    switch (p2)
                    {
                        case 1:
                            this.CurrentThread.Mem[p1] = this.Registers["A"];
                            break;
                        case 2:
                            this.CurrentThread.Mem[p1] = this.Registers["B"];
                            break;
                        case 3:
                            this.CurrentThread.Mem[p1] = this.Registers["C"];
                            break;
                        case 4:
                            this.CurrentThread.Mem[p1] = this.Registers["D"];
                            break;
                        default:
                            break;
                    }
                    break;
                case 13:
                    switch (p1)
                    {
                        case 1: //addr
                            this.CurrentThread.Mem[this.Registers["FP"]] = this.CurrentThread.Mem[p2];
                            break;
                        case 2: //reg
                            switch (p2)
                            {
                                case 1:
                                    this.CurrentThread.Mem[this.Registers["FP"]] = this.Registers["A"];
                                    break;
                                case 2:
                                    this.CurrentThread.Mem[this.Registers["FP"]] = this.Registers["B"];
                                    break;
                                case 3:
                                    this.CurrentThread.Mem[this.Registers["FP"]] = this.Registers["C"];
                                    break;
                                case 4:
                                    this.CurrentThread.Mem[this.Registers["FP"]] = this.Registers["D"];
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case 3: //val
                            this.CurrentThread.Mem[this.Registers["FP"]] = p2;
                            break;
                        default:
                            break;
                    }
                    this.Registers["FP"]++;
                    break;
                case 14:
                    if (this.Registers["BF"] == 1)
                        this.Registers["IP"] = Convert.ToByte((p1 - 1) * 3);
                    else if (this.Registers["BF"] == 0)
                        this.Registers["IP"] = Convert.ToByte((p2 - 1) * 3);
                    else
                        throw new Exception("Error: Boolean Flag not assigned.");
                    this.Registers["BF"] = byte.MaxValue;
                    break;
                default:
                    break;
            }
        }
    }
}
