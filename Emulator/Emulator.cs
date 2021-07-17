using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using AssemblerParser.Instructions.Memory;
using EmulatorEasyMethod;
using EmulatorEasyMethod.Instructions.Math;
using EmulatorEasyMethod.Instructions.Memory;
using static EmulatorEasyMethod.Helper;

namespace Emulator
{
    public class Emulator
    {
        const int INSTRUCTION_SIZE = 4;
        ushort[] registers = new ushort[32];

        public byte[] RAM = new byte[256];
        static Memory<byte> ProgramSpace;
        static byte IP = 31;

        Span<byte> Fetch()
        {
            var instruction = ProgramSpace.Span.Slice(registers[IP], INSTRUCTION_SIZE);
            registers[IP] += INSTRUCTION_SIZE;

            return instruction;
        }

        public void DecodeAndExecute(Span<byte> bytes)
        {
            byte opCodeASM = 0x00;
            var srcReg = bytes[1];
            ushort value = 0x00;
            byte memoryAddress = 0x00;

            switch ((InstructionType)bytes[0])
            {
                case InstructionType.NOP:
                    //do nothing
                    break;
                case InstructionType.MOV:
                   MOV mov= new MOV(bytes[1], bytes[2], bytes[3]);
                    //ushort value = (ushort)((bytes[2] << 8) | bytes[3]);
                    value = mov.Execute();               
                    break;
                case InstructionType.ADD:
                    ADD add = new ADD(bytes[1], bytes[2], bytes[3]);
                    value = add.Execute(registers);
                    break;
                case InstructionType.STR:
                    STR store = new STR(bytes[1], bytes[2]);
                    value = registers[bytes[3]];
                    store.Execute(RAM, value);
                    value = RAM[memoryAddress];
                    break;
                default:
                    throw new NotImplementedException($"Not a valid instruction 0x{opCodeASM: X}");
            }
            registers[srcReg] = value;
        }

        static void Main(string[] args)
        {
            Emulator emulator = new Emulator();
            emulator.registers[IP] = 0;

            byte programSpaceStart = 0xF0;
            byte programLength = 16;

            byte stackSpaceStart = 0xEF;
            byte stackSize = 64;

            ProgramSpace = emulator.RAM.AsMemory(programSpaceStart, programLength);
            Span<byte> stackSpaceAsBytes = emulator.RAM.AsSpan<byte>(stackSpaceStart - stackSize + 1, stackSize);
            Span<ushort> stackSpace = MemoryMarshal.Cast<byte, ushort>(stackSpaceAsBytes);

            byte SP = (byte)(stackSpace.Length - 1);

            //Simulate loading a program from file
            byte[] programBytes =
            {
                0x02, 0x01, 0xBE, 0xEF,     // MOV R1 0xBE 0xEF
                0x02, 0x02, 0x00, 0x07,     // MOV R2 7
                0x10, 0x00, 0x01, 0x02      // ADD R0 R1 R2
            };

            // Load program into Program Space
            programBytes.CopyTo(ProgramSpace);

            // PUSH something onto the stack
            stackSpace[SP] = 0xBEEF;
            SP--;

            // PUSH something else onto the stack
            stackSpace[SP] = 0xFEED;
            SP--;

            // Start my program
            for (int i = 4; i <programBytes.Length ; i+=4)
            {
                var instruction = emulator.Fetch();
                emulator.DecodeAndExecute(instruction);
            }
        }
    }
}
