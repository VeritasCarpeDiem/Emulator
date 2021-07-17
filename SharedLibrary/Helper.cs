using EmulatorEasyMethod.Instructions.Math;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmulatorEasyMethod
{
    public static class Helper
    {
        public enum InstructionType : byte
        {
            NOP = 0x01,
            MOV = 0x02,
            CPY,
            PUS,
            POP,
            LOD,
            STR,
            ADD = 0x10,
            SUB,
            JMP = 0x30,

        }

        public static Dictionary<string, byte> OpCodeToByte = new Dictionary<string, byte>(); //this is not used

        public static Dictionary<string, ushort> labelMapper = new Dictionary<string, ushort>();

        public static Dictionary<InstructionType, string> ByteToOpCode = new Dictionary<InstructionType, string>()
        {

            [InstructionType.NOP] = "NOP",
            [InstructionType.MOV] = "MOV",
            [InstructionType.CPY] = "CPY",
            [InstructionType.ADD] = "ADD",
            [InstructionType.JMP] = "JMP",

        };

        public static Dictionary<byte, Type> ByteToInstructionType = new Dictionary<byte, Type>() //for reflection emulator
        {
            [0x10] = typeof(ADD),
            //[0x11]=typeof(SUB),
        };
    }
}
