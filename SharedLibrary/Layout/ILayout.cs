using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace EmulatorEasyMethod.Layout
{
    public interface ILayout
    {
        public byte[] Parse(Match match);

        public string ParseForDisassembler(byte[] bytes, int counter);

        public string Read4Bytes(int counter, params byte[] bytes);

        //public void CallsRead4Bytes(int counter, byte[] bytes);
    }
}
