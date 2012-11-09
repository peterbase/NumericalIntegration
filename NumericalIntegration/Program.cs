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
            // part A

            double start = 0;
            double end = 10;
            int[] meshpoints = new int[]{ 10, 50, 100 };

            Console.WriteLine("Integrating Xsquared between 0 and 10");
            foreach (var item in meshpoints)
            {
                double[] values = ValuesToIntegrateOver(start, end, item, XSquared);
                double trapezoid = TrapezoidRule(start, end, item, values);
                double simpsons = SimpsonsRule(start, end, item, values);
                Console.WriteLine("At T = " + end + "; For " + item + " meshpoints: \tTrapezeoid = " + trapezoid + " Simpsons = " + simpsons);
            }

            // part B

            System.IO.StreamWriter file = new System.IO.StreamWriter("D:\\Coding\\PHYS2320_Computing_2\\NumericalIntegration\\task2_fixed.csv");
            file.WriteLine("t,Trapezoid 10, Simpson 10, Trapezoid 50,Simpson 50,Trapezoid 100,Simpson 100");
            double[] points = new double[]{ 1, 2, 3, 4, 5 };
            foreach (var endpoint in points)
            {
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

            // part C

            double[] xvalues = ValuesToIntegrateOver(start, 2, 100, XSquared);
            double[] yvalues = ValuesToIntegrateOver(start, 2, 100, Y);
            double result = SimpsonsRule(start, 2, 100, xvalues) * SimpsonsRule(start, 2, 100, yvalues) * 30 * 2;
            Console.WriteLine("Integrating 30x^2y with 100 meshpoints using simpson's rule: " + result);

        }

        static double XSquared(double x)
        {
            return Math.Pow(x, 2);
        }

        static double SinSquaredStuff(double t)
        {
           return 0.5 * Math.Pow(Math.Sin(t / Math.PI), 2);
        }

        static double Y(double y)
        {
            return y;
        }
        static double[] ValuesToIntegrateOver(double start, double end, int meshpoints, Func<double, double> Function)
        {
            /// <summary>
            /// Evaluates a function between 2 limits, and returns a meshpoint-long array of their values.
            /// <summary>
            /// 
            
            double interval = (end - start) / meshpoints;
            double[] values = new double[meshpoints + 1];
            double x = start;
            for (int i = 0; i <= meshpoints; i++)
            {
                values[i] = Function(x);
                x += interval;
                
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
