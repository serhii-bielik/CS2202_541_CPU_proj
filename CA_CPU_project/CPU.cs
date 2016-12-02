using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CA_CPU_project
{
    public class CPU
    {
        private int[] registers;
        private List<string> opcodes;
        private List<string> tableData;
        private string[] input;
        private int clockCycles;
        private int lineCounter;

        public CPU(String[] input)
        {
            this.clockCycles = 0;
            this.registers = new int[] { 0, 0, 0, 0, 0, 0, 0, 0};
            this.opcodes = new List<string>(new string[] { "mov", "add", "sub", "mul", "div" });
            this.input = input;
            this.tableData = new List<string>();
        }

        private void Execute(String data)
        {
            String[] command = data.ToLower().Split(' ');
            StringBuilder sb = new StringBuilder();

            sb.Append(tableData.Count);
            sb.Append("|").Append(command[0]).Append(" ").Append(command[1]).Append(", ").Append(command[2]);
            sb.Append("|");
            int registerId;
            switch (command[0])
            {
                case "mov":                   
                    sb.Append(toBinary(opcodes.IndexOf(command[0]), 5));
                    sb.Append(" ");
                    registerId = Convert.ToInt32(command[1].Substring(1));
                    sb.Append(toBinary(registerId, 3));
                    sb.Append(" ");
                    if (command[2].Substring(0,1) != "r")
                    {
                        int secondParameter = Convert.ToInt32(command[2]);
                        sb.Append(toBinary(secondParameter, 16));
                        registers[registerId] = secondParameter;
                        clockCycles++;
                        sb.Append("|1");
                    }                        
                    else
                    {
                        int secondRegisterId = Convert.ToInt32(command[2].Substring(1));
                        sb.Append(toBinary(secondRegisterId, 3).PadRight(16, '0'));
                        registers[registerId] = registers[secondRegisterId];
                        clockCycles += 2;
                        sb.Append("|2");
                    }                
                    break;
                case "add":
                    sb.Append(toBinary(opcodes.IndexOf(command[0]), 5));
                    sb.Append(" ");
                    registerId = Convert.ToInt32(command[1].Substring(1));
                    sb.Append(toBinary(registerId, 3));
                    sb.Append(" ");
                    if (command[2].Substring(0, 1) != "r")
                    {
                        int secondParameter = Convert.ToInt32(command[2]);
                        sb.Append(toBinary(secondParameter, 16));
                        registers[registerId] += secondParameter;
                        clockCycles += 2;
                        sb.Append("|2");
                    }
                    else
                    {
                        int secondRegisterId = Convert.ToInt32(command[2].Substring(1));
                        sb.Append(toBinary(secondRegisterId, 3).PadRight(16, '0'));
                        registers[registerId] += registers[secondRegisterId];
                        clockCycles += 3;
                        sb.Append("|3");
                    }                    
                    break;
                case "sub":
                    sb.Append(toBinary(opcodes.IndexOf(command[0]), 5));
                    sb.Append(" ");
                    registerId = Convert.ToInt32(command[1].Substring(1));
                    sb.Append(toBinary(registerId, 3));
                    sb.Append(" ");
                    if (command[2].Substring(0, 1) != "r")
                    {
                        int secondParameter = Convert.ToInt32(command[2]);
                        sb.Append(toBinary(secondParameter, 16));
                        registers[registerId] -= secondParameter;
                        clockCycles += 2;
                        sb.Append("|2");
                    }
                    else
                    {
                        int secondRegisterId = Convert.ToInt32(command[2].Substring(1));
                        sb.Append(toBinary(secondRegisterId, 3).PadRight(16, '0'));
                        registers[registerId] -= registers[secondRegisterId];
                        clockCycles += 3;
                        sb.Append("|3");
                    }
                    break;
                case "mul":
                    sb.Append(toBinary(opcodes.IndexOf(command[0]), 5));
                    sb.Append(" ");
                    registerId = Convert.ToInt32(command[1].Substring(1));
                    sb.Append(toBinary(registerId, 3));
                    sb.Append(" ");
                    if (command[2].Substring(0, 1) != "r")
                    {
                        int secondParameter = Convert.ToInt32(command[2]);
                        sb.Append(toBinary(secondParameter, 16));
                        registers[registerId] *= secondParameter;
                        clockCycles += 3;
                        sb.Append("|3");
                    }
                    else
                    {
                        int secondRegisterId = Convert.ToInt32(command[2].Substring(1));
                        sb.Append(toBinary(secondRegisterId, 3).PadRight(16, '0'));
                        registers[registerId] *= registers[secondRegisterId];
                        clockCycles += 5;
                        sb.Append("|5");
                    }
                    break;
                case "div":
                    sb.Append(toBinary(opcodes.IndexOf(command[0]), 5));
                    sb.Append(" ");
                    registerId = Convert.ToInt32(command[1].Substring(1));
                    sb.Append(toBinary(registerId, 3));
                    sb.Append(" ");
                    if (command[2].Substring(0, 1) != "r")
                    {
                        int secondParameter = Convert.ToInt32(command[2]);
                        sb.Append(toBinary(secondParameter, 16));
                        registers[registerId] /= secondParameter;
                        clockCycles += 3;
                        sb.Append("|3");
                    }
                    else
                    {
                        int secondRegisterId = Convert.ToInt32(command[2].Substring(1));
                        sb.Append(toBinary(secondRegisterId, 3).PadRight(16, '0'));
                        registers[registerId] /= registers[secondRegisterId];
                        clockCycles += 5;
                        sb.Append("|5");
                    }
                    break;
                default:
                    throw new Exception("Unknown opcode");
            }
            tableData.Add(sb.ToString());
        }

        internal bool hasCode()
        {
            return lineCounter < input.Length;
        }

        internal string getCPI()
        {
            double cpi = Math.Round((double)clockCycles / lineCounter, 2);
            return cpi.ToString();
        }

        internal void ExecuteAllLines()
        {
            while(lineCounter < input.Length)
                Execute(input[lineCounter++]);
        }

        internal void ExecuteOneLine()
        {
            if(lineCounter < input.Length)
                Execute(input[lineCounter++]);
        }

        internal string[] getRegistersData()
        {
            string[] registersForInterface = new String[8];
            for (int i = 0; i < registers.Length; i++)
            {
                registersForInterface[i] = toBinary(registers[i], 16);
            }
            return registersForInterface;
        }

        internal string[] getTableData()
        {
            return tableData.ToArray();
        }

        private String toBinary(int value, int length)
        {
            return Convert.ToString(value, 2).PadLeft(length, '0');
        }
    }
}
