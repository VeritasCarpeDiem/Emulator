using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MMIO
{
    public partial class Form1 : Form
    {
        public int scale = 5;
        Color color;

        public Form1()
        {
            InitializeComponent();
            FillButton.Click += FillButton_Click;
            FillButton.Click += ScaleButton_Click;
        }

        private void FillButton_Click(object sender, EventArgs e)
        {
            Bitmap map = (Bitmap)Screen.Image ?? new Bitmap(Screen.Width, Screen.Height);

            for (int x = 0; x < Screen.Width; x++)
            {
                for (int y = 0; y < Screen.Height; y++)
                {
                    map.SetPixel(x, y, Color.Black);
                }
            }
            Screen.Image = map;

        }

        private void ScaleButton_Click(object sender, EventArgs e)
        {
            Bitmap originalMap = new Bitmap(Screen.Width, Screen.Height);
            Bitmap scaledMap = new Bitmap(Screen.Width * scale, Screen.Height * scale);

            for (int x = 0; x < Screen.Width; x++)
            {
                for (int y = 0; y < Screen.Height; y++)
                {
                    Color colorForThisPixel = originalMap.GetPixel(x, y);
                    for (int scaledX = 0; scaledX < scale; scaledX++)
                    {
                        for (int scaledY = 0; scaledY < scale; scaledY++)
                        {
                            scaledMap.SetPixel(scale* x + scaledX, scale* y + scaledY, colorForThisPixel);
                            //scaledX = scaledX * scale + scaledX;
                            //scaledY = scaledY * scale + scaledY;
                        }
                    }
                }
            }
            PictureBox box = new PictureBox();
            box.Location = new Point();
            box.Image = scaledMap;

            //return scaledMap;

            #region Michael's example
            //Bitmap scaledMap = new Bitmap(Screen.Image.Width * scale, Screen.Image.Height * scale);

            //Bitmap originalMap = new Bitmap(Screen.Image);

            //for (int x = 0; x < Screen.Image.Width; x++)
            //{
            //    for (int y = 0; y < Screen.Image.Height; y++)
            //    {
            //        Color colorForThisBlock = originalMap.GetPixel(x, y);
            //        for (int runningX = 0; runningX < scale; runningX++)
            //        {
            //            for (int runningY = 0; runningY < scale; runningY++)
            //            {
            //                var scaledX = x * scale + runningX;
            //                var scaledY = y * scale + runningY;
            //            }
            //        }
            //    }
            //}

            //PictureBox box = new PictureBox();
            //box.Location = new Point(Screen.Right + 10, Screen.Top);
            //box.Image = scaledMap;
            //box.SizeMode = PictureBoxSizeMode.AutoSize;
            //Controls.Add(box);
            #endregion
        }

        private void ColorDialogPopUpButton_Click(object sender, EventArgs e)
        {
            var result = ColorPicker.ShowDialog();
            if (result == DialogResult.OK)
            {
                color = ColorPicker.Color;
            }
        }

    }
}
