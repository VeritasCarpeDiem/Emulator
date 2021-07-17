using EmulatorEasyMethod.Layout;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace EmulatorEasyMethod.Instructions
{
    public abstract class BaseInstruction
    {
        public abstract ILayout Layout { get; }

        protected byte[] machineCode;

        public BaseInstruction() { }

        public BaseInstruction(byte[] bytes)
        {
            machineCode = bytes;
        }

        protected RegexOptions regexOptions = RegexOptions.IgnoreCase;

        protected byte padding = 0xFF;

       //protected abstract string originalAssembly { get; set; }
        protected abstract string Pattern { get; }
        protected string start = "^";
        protected string end = "$";
        protected string space = " +";
        
        protected string register = @"[Rr]((?:[012]\d)|(?:3[01])|\d)";
        protected string literalValue = @"(0x)?(([0-9A-Fa-f]{4})|([0-9A-Fa-f]{2}))";
        protected string comment = @"(?:( *);.*)*";

        protected abstract byte OpCode { get; }

        public abstract BaseInstruction Parse(string asm);
        public abstract byte[] Emit();

    }
}
