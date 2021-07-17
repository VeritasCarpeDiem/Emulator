using EmulatorEasyMethod.Layout;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using static EmulatorEasyMethod.Helper;

namespace EmulatorEasyMethod.Instructions.Math
{
    public class ADD : BaseInstruction
    {
        public ADD(): base()
        { }

        public ADD(byte destReg, byte sourceReg1, byte sourceReg2) : base(new byte[] { byte.Parse(ByteToOpCode[InstructionType.ADD]), destReg, sourceReg1, sourceReg2 })
        {

        }

        private string originalAssembly;
        private byte destReg;
        private ushort val;
        private string instr;
        private byte sourceReg1;
        private byte sourceReg2;

        protected override string Pattern => $"{start}(ADD){space}{register}{space}{register}{space}{register}{comment}{end}$";

        protected override byte OpCode => 0x20;

        public override ILayout Layout => MathLayout.Instance;

        public override byte[] Emit()
        {
            return new byte[]
            {
                OpCode,
                destReg,
                (byte)(val >> 8),
                (byte)val
            };
        }

        public override BaseInstruction Parse(string asm)
        {
            var match = Regex.Match(asm, Pattern);
            if (match.Success)
            {
                var instruction = new ADD();

                instruction.originalAssembly = match.Groups[0].Value;
                instruction.instr = match.Groups[1].Value;
                instruction.destReg=byte.Parse(match.Groups[2].Value);
                instruction.sourceReg1 = byte.Parse(match.Groups[3].Value);
                instruction.sourceReg2 = byte.Parse(match.Groups[4].Value);

                #region
                //Console.WriteLine($"Original assembly:{match.Groups[0]}");
                //Console.WriteLine($"Instruction: {match.Groups[1]}");
                //Console.WriteLine($"Dest. Reg:{match.Groups[2].Value}");
                //Console.WriteLine($"Source Reg1:{match.Groups[3].Value}");
                //Console.WriteLine($"Source Reg2:{match.Groups[4].Value}");
                #endregion

                return instruction;
            }

            return null;
        }

        public ushort Execute(ushort[] registers)
        {
            var srcReg1Value = registers[sourceReg1];
            var srcReg2Value = registers[sourceReg2];

            var result = srcReg1Value + srcReg2Value;

            return (ushort)result;

        }
    }
}
