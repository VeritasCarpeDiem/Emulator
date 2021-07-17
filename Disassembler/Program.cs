using EmulatorEasyMethod;
using System;
using System.Collections.Generic;

namespace Disassembler
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] machineCode = Assembler.Assemble("assembly.txt").ToArray();
            
          
            List<string> assembly = Disassembler.Disassemble(machineCode);

            foreach (var line in assembly)
            {
                Console.WriteLine(line);
            }
        }
    }
}
