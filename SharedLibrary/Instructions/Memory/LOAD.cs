using EmulatorEasyMethod.Instructions;
using EmulatorEasyMethod.Layout;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblerParser.Instructions.Memory
{
    public class LOAD : BaseInstruction
    {
        public override ILayout Layout => throw new NotImplementedException();

        protected override string Pattern => throw new NotImplementedException();

        protected override byte OpCode => throw new NotImplementedException();

        public override byte[] Emit()
        {
            throw new NotImplementedException();
        }

        public override BaseInstruction Parse(string asm)
        {
            throw new NotImplementedException();
        }
    }
}
