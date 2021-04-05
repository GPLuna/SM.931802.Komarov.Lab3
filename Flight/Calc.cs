using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight
{
	class Calc
	{
        const double dt = 0.01;
        const double g = 9.81;
        public double a;
        public double v0;
        public double y0;
        public double S;
        public double m;
        public double k;
        public double t;
        public double x;
        public double y;
        public double vx;
        public double vy;
        
        public void initialization()
        {
            vx = v0 * Math.Cos(a * Math.PI / 180);
            vy = v0 * Math.Sin(a * Math.PI / 180);
        }
        public double[] getWeightHeight()
        {
            double vxx = vx;
            double vyy = vy;
            double xx = x;
            double yy = y;
            double ymax = 0;
            double[] arr = new double[2];

            while (yy >= 0)
            {
                vxx = vxx - k * vxx * Math.Sqrt(vxx * vxx + vyy * vyy) * dt;
                vyy = vyy - (g + k * vyy * Math.Sqrt(vxx * vxx + vyy * vyy)) * dt;

                xx = xx + vxx * dt;
                yy = yy + vyy * dt;

                if (yy > ymax) ymax = yy;
            }
            arr[0] = xx;
            arr[1] = ymax;

            return arr;
        }
        public void FindingXY()
        {
            vx = vx - k * vx * Math.Sqrt(vx * vx + vy * vy) * dt;
            vy = vy - (g + k * vy * Math.Sqrt(vx * vx + vy * vy)) * dt;

            x = x + vx * dt;
            y = y + vy * dt;
        }
        public Tuple<double, double, double> GetSize()
        {
            double sina = Math.Sin(a * Math.PI / 180);
            double ymax = y0 + (v0 * v0 * sina * sina / 2 / g);
            double tpol = (v0 * sina + Math.Sqrt(v0 * v0 * sina * sina + 2 * g * y0)) / g;
            double xmax = v0 * tpol * Math.Cos(a * Math.PI / 180);
            return Tuple.Create(xmax, ymax, tpol);
        }
    }
}
