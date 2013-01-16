using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Testing_Fields
{
    class Program
    {
        static void Main(string[] args)
        {
            float left = (float)Math.Atan2(0, -1);
            float right = (float)Math.Atan2(0, 1);
            float up = (float)Math.Atan2(1, 0);
            float down = (float)Math.Atan2(-.2, -.8);

            Console.WriteLine(right);
            Console.WriteLine(up);
            Console.WriteLine(left);
            Console.WriteLine(down);
            Console.WriteLine();
            Console.WriteLine(UnitConverter.RadToDegrees(right));
            Console.WriteLine(UnitConverter.RadToDegrees(up));
            Console.WriteLine(UnitConverter.RadToDegrees(left));
            Console.WriteLine(UnitConverter.RadToDegrees(down));
        }

        static String tmp(int num, String tmp = "gorga")
        {
            return tmp;
        }
    }
}
