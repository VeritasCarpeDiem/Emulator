using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using static EmulatorEasyMethod.Helper;

namespace EmulatorEasyMethod.Layout
{
    public sealed class MemoryLayout : ILayout
    {
        private MemoryLayout() { }

        public static MemoryLayout Instance { get; } = new MemoryLayout();

        public byte[] Parse(Match match) //string to bytes
        {
            byte[] bytes = new byte[4];

            bytes[0] = Helper.OpCodeToByte[match.Groups[1].Value];

            for (int i = 2; i < 4; i++)
            {
                bytes[i - 1] = byte.Parse(match.Groups[i].Value);
            }

            return bytes;
        }

        public string Read4BytesForMOV(int counter, params byte[] bytes)
        {
            string opCodeASM = Helper.ByteToOpCode[(InstructionType)bytes[counter]];
            string line = "";
            line += opCodeASM;
            line += $"R{bytes[counter + 1]}";
            line += $"{bytes[counter + 2]}";
            return line;
        }

        public string Read4BytesForSTR(int counter, params byte[] bytes)
        {
            string opCodeASM = ByteToOpCode[(InstructionType)bytes[counter]];
            string line = "";
            line = $"{opCodeASM} 0x{bytes[counter+1]} R{bytes[counter+2]}";
            return line;
        }
        //public void CallsRead4Bytes(int counter,byte[] bytes)
        //{
        //    Read4Bytes(counter, bytes[counter], bytes[counter + 1], bytes[counter + 2]);
        //}

        public string ParseForDisassembler(byte[] bytes, int counter)
        {
            string opCodeASM= Helper.ByteToOpCode[(InstructionType)bytes[counter]];
            string assembly = $"{opCodeASM} R{bytes[counter+1]} {bytes[counter+2]}"; 
           
            return assembly; 
        }

        public string Read4Bytes(int counter, params byte[] bytes)
        {
            return ((ILayout)Instance).Read4Bytes(counter, bytes);
        }
    }
}
