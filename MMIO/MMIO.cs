using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MMIO
{
    public class MMIO
    {
        static MMIO()
        {
            var startingAddress = 0xF0;
            var endingAddress = 0xFE;

            var programLength = endingAddress - startingAddress + 1;
            var side = (int)Math.Sqrt(programLength);

            Bitmap MMIO = new Bitmap(side, side);

            for (int i = startingAddress; i < endingAddress; i++)
            {
                //var twoD = (i - startingAddress).
                //MMIO.SetPixel();

            }
        }
    }
}
