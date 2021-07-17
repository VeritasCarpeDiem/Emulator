using EmulatorEasyMethod.Layout;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmulatorEasyMethod.Instructions.Memory
{
    public class JMP : BaseInstruction
    {
        public override ILayout Layout => MemoryLayout.Instance;

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
