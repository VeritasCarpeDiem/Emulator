using EmulatorEasyMethod.Instructions;
using EmulatorEasyMethod.Instructions.Math;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace EmulatorEasyMethod
{
    public static class Assembler
    {
        static ushort BaseAddress = 0xF000; //this is the starting address of our instruction pointer

        public static List<byte> Assemble(string path)
        {
            string dir = Path.GetDirectoryName(path);

            List<string> assemblylines = File.ReadAllLines("assembly.txt").ToList();

            string[] assembly = new[] //test case
            {
                "ADD R0 R1 R2",
                "ADD R2 R1 R0"
            };

            List<BaseInstruction> possibleInstructions = new List<BaseInstruction>() { new ADD() };
            List<BaseInstruction> instructions = new List<BaseInstruction>();
            List<byte> machineCode = new List<byte>();

            string label = "^(.+:)$";
            byte offSet = 0x04; //each register is 4 bytes
            ushort counter = BaseAddress;
            foreach (var line in assemblylines)
            {
                var match = Regex.Match(line, label);
                if (match.Success)
                {
                    Helper.labelMapper.Add(line, counter);
                    counter += offSet; //this is the address the JMP will go to
                }
            }
            foreach (var line in assembly)
            {
                bool isValid = false;

                foreach (var possibleInstruction in possibleInstructions)
                {
                    if (possibleInstruction.Parse(line) is BaseInstruction instruction)
                    {
                        var match = Regex.Match(line, label);
                        if (match.Success) //this means that this line is a label
                        {
                            //do nothing -- we will do this in the emulator
                        }
                        Console.WriteLine($"valid instruction\n");
                        isValid = true;

                        instructions.Add(instruction);
                        var bytes = possibleInstruction.Emit();
                        machineCode.AddRange(bytes);
                    }
                }
                if (!isValid)
                {
                    Console.WriteLine($"Invalid Assembly: {line}");
                    break;
                }
            }
            
            File.WriteAllBytes("assembly.bin", machineCode.ToArray());

            return machineCode;
        }
    }
}
