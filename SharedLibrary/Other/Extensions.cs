using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace AssemblerParser.Other
{
    public static class Extensions
    {
        public static Point OneToTwoD(this int index, int width)
        {
            return new Point(index % width, index / width);
        }
    }
}
