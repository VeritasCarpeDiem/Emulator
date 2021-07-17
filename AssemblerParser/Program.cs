using EmulatorEasyMethod;
using EmulatorEasyMethod.Instructions.Math;
using System;
using System.Collections.Generic;

namespace Disassembler
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = "ADD R0 R1 R2 ;this is a comment";
            ADD add = new ADD();
            //add.Parse(input);

            byte[] machineCode = Assembler.Assemble("assembly.txt").ToArray();

           
        }
    }
}
