using EmulatorEasyMethod.Instructions;
using EmulatorEasyMethod.Layout;
using System;
using System.Collections.Generic;
using System.Text; 

namespace AssemblerParser.Instructions.Memory
{
    public class STR : BaseInstruction
    {
        public STR() : base() { }

        public STR( byte memoryAddress,byte sourceReg)
        {
            this.memoryAddress = memoryAddress;
            this.sourceReg = sourceReg;
        }

        private byte sourceReg;
        private byte memoryAddress;

        public override ILayout Layout => MemoryLayout.Instance;

        protected override string Pattern => $"{start}{OpCode}{space}{register}{comment}{end}$";

        protected override byte OpCode => 0x07;

        public override byte[] Emit()
        {
            return new byte[]
                {
                    OpCode,
                    sourceReg
                };
        }

        public override BaseInstruction Parse(string asm)
        {
            throw new NotImplementedException();
        }

        public void Execute(byte[] RAM,ushort value)
        {
            //check if inside RAM range, else throw exception
            // 
            if(memoryAddress < 0xEF00 && memoryAddress>0xF000)
            {
                
               RAM[memoryAddress] = (byte)value;
               
            }
            throw new Exception($"Not a valid memory address: 0x{memoryAddress}");
        }
    }
}
