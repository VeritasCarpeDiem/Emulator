using EmulatorEasyMethod.Layout;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using static EmulatorEasyMethod.Helper;

namespace EmulatorEasyMethod.Instructions.Memory
{
    public class MOV : BaseInstruction
    {
        public MOV(): base()
        {

        }
        public MOV(byte destReg, ushort lowByteVal, ushort highByteVal) 
        {
            this.destReg = destReg;
            this.lowByteVal = lowByteVal;
            this.highByteVal = highByteVal;
        }
        protected string originalAssembly;
        private byte destReg;
        private ushort lowByteVal;
        private ushort highByteVal;

        public override ILayout Layout => MemoryLayout.Instance;

        protected override string Pattern => $"{start}{OpCode}{space}{register}{space}{literalValue}{comment}{end}$";

        protected override byte OpCode => 0x02;

        public override byte[] Emit()
        {
            return new byte[]
            {
                OpCode,
                destReg,
                (byte)(lowByteVal >> 8),
                (byte)highByteVal
            };
        }

        public override BaseInstruction Parse(string asm)
        {
            var match = Regex.Match(asm, Pattern, regexOptions);
            if (!match.Success) return null;

            var instruction = new MOV();

            instruction.originalAssembly = match.Groups[0].Value;
            instruction.destReg = byte.Parse(match.Groups[2].Value);

            int fromBase = match.Groups[3].Value == "0x" ? 16 : 10;
            string number = match.Groups[4].Value;
            instruction.lowByteVal = Convert.ToUInt16(number, fromBase);
            instruction.highByteVal = Convert.ToUInt16(number, fromBase);
            return instruction;
        }
        public ushort Execute()
        {
            ushort value = (ushort)((lowByteVal << 8) | highByteVal);

            return value;
        }
    }
}
