using EmulatorEasyMethod.Instructions;
using EmulatorEasyMethod.Layout;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmulatorEasyMethod.Other
{
    public class Label : BaseInstruction
    {
        public override ILayout Layout => null;

        protected override string Pattern => @"^(.+:)$";

        protected override byte OpCode => 0xFF; //?

        public override byte[] Emit()
        {
            return new byte[]
            {
                
            };
        }

        public override BaseInstruction Parse(string asm)
        {
            return null;
        }
    }
}
