using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckerBoard
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ButtonGenerator();
        }

        private void ButtonGenerator()
        {
            Button[,] buttons = new Button[8, 8];
            int top = 0, left = 0;
            const int width = 50, height = 50;
            for (int i = 0; i < buttons.GetUpperBound(0); i++)
            {
                for (int j = 0; j < buttons.GetUpperBound(1); j++)
                {
                    buttons[i, j] = new Button
                    {
                        Width = width,
                        Height = height,
                        Left = left,
                        Top = top
                    };
                    left += width;

                    if ((i + j) % 2 == 0)
                        buttons[i, j].BackColor = Color.Black;
                    else
                        buttons[i, j].BackColor = Color.White;

                    this.Controls.Add(buttons[i, j]);
                }
                left = 0;
                top += height;
            }
        }
    }
}
