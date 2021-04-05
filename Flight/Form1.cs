using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flight
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        const double C = 0.15;
        const double rho = 1.29;
        const double dt = 0.01;
        Calc n = new Calc();
        
        private void btStart_Click(object sender, EventArgs e)
        {
            n.a = (double)edAngle.Value;
            n.v0 = (double)edSpeed.Value;
            n.y0 = (double)edHeight.Value;
            n.m = (double)edWeight.Value;
            n.S = (double)edSquare.Value;

            var sizes = n.GetSize();
            var size1 = Convert.ToInt32(sizes.Item1) + 1;
            var size2 = Convert.ToInt32(sizes.Item2) + 1;
            chart1.ChartAreas[0].AxisX.Maximum = size1;
            chart1.ChartAreas[0].AxisY.Maximum = size2;

            n.t = 0;
            n.x = 0;
            n.y = n.y0;

            n.initialization();
            n.k = 0.5 * C * n.S * rho / n.m;

            chart1.Series[0].Points.Clear();
            chart1.Series[0].Points.AddXY(n.x, n.y);

            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            n.FindingXY();
            n.t += dt;
            chart1.Series[0].Points.AddXY(n.x, n.y);
            time.Text = "Время: " + Convert.ToString(n.t);
            if (n.y <= 0) timer1.Stop();
        }

        private void stop_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled) timer1.Stop();
            else timer1.Start();
        }
    }
}
