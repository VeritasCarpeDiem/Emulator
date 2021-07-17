using EmulatorEasyMethod.Instructions.Math;
using EmulatorEasyMethod.Instructions.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using static EmulatorEasyMethod.Helper;

namespace Disassembler
{
    public static class Disassembler
    {
        public static List<string> Disassemble(byte[] bytes)
        {
            //bytes = Assembler.Assemble("assembly.txt").ToArray();

            //test case:
            bytes = new byte[]
            { 
                //MOV R0 0 
                0x02,
                00,
                0000,
                0xFF,
                //ADD R0 R1 1
                0x10,
                00,
                00,
                01
            };

            List<string> lines = new List<string>(bytes.Length);
            string line;

            for (int i = 0; i < bytes.Length; i+=4) //each line is 4 bytes
            {
                byte opCodeASM= bytes[i];
                
                switch ((InstructionType)opCodeASM)
                {
                    case InstructionType.MOV:
                       line= new MOV().Layout.Read4Bytes(i,bytes[i], bytes[i+1], bytes[i+2]);
                        break;
                    case InstructionType.ADD:
                        line= new ADD().Layout.Read4Bytes(i,bytes[i], bytes[i+1], bytes[i+2], bytes[i+3]);
                        break;
                    default:
                        throw new NotImplementedException($"Not a valid instruction: 0x{opCodeASM: X}");
                        break;
                }
                lines.Add(line);
            }

            return lines;
        }

    }
}
