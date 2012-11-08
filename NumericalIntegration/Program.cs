using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumericalIntegration
{
    class Program
    {
        static void Main(string[] args)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter("D:\\Coding\\PHYS2320_Computing_2\\NumericalIntegration\\task2.csv");
            file.WriteLine("t,Trapezoid 10, Simpson 10, Trapezoid 50,Simpson 50,Trapezoid 100,Simpson 100");
            double start = 0;
            double end = 10;
            double[] points = new double[]{ 1, 2, 3, 4, 5 };
            foreach (var endpoint in points)
            {
                int[] meshpoints = new int[]{ 10, 50, 100 };
                file.Write(endpoint + ",");
                foreach (var item in meshpoints)
                {
                    double[] values = ValuesToIntegrateOver(start, endpoint, item, SinSquaredStuff);
                    double trapezoid = TrapezoidRule(start, endpoint, item, values);
                    double simpsons = SimpsonsRule(start, endpoint, item, values);
                    Console.WriteLine("At T = " + endpoint + "; For " + item + " meshpoints: \tTrapezeoid = " + trapezoid + " Simpsons = " + simpsons);
                    file.Write(trapezoid + "," + simpsons + ",");
                }
                file.WriteLine();
            }
            file.Close();

        }

        static double XSquared(double x)
        {
            return Math.Pow(x, 2);
        }

        static double SinSquaredStuff(double t)
        {
           double retval = 0.5 * Math.Pow(Math.Sin(t / Math.PI), 2);
           return retval;
        }

        static double[] ValuesToIntegrateOver(double start, double end, int meshpoints, Func<double, double> Function)
        {
            /// <summary>
            /// Evaluates a function between 2 limits, and returns a meshpoint-long array of their values.
            /// <summary>
            /// 
            
            double interval = (end - start) / meshpoints;
            double[] values = new double[meshpoints + 1];
            int count = 0;
            for (double x = start ; x <= end ; x+=interval)
            {
                values[count] = Function(x);
                count++;
            }
            return values;
        }

        static double TrapezoidRule(double start, double end, int meshpoints, double[] values)
        {
            double interval = (end - start) / meshpoints;
            double sum = 0.5 * (values[0] + values[meshpoints]);

            for (int i = 1; i < meshpoints; i++)
            {
                sum += values[i];
            }
            sum = interval * sum;
            return sum;
        }

        static double SimpsonsRule(double start, double end, int meshpoints, double[] values)
        {
            double interval = (end - start) / meshpoints;

            double sum =  values[0] + values[meshpoints];

            for (int i = 1; i < meshpoints; i++)
            {
                if (i % 2 == 0)
                    sum += 2 * values[i];

                else
                    sum += 4 * values[i];
            }
            sum = (interval / 3) * sum;
            return sum;
        }

    }
}
