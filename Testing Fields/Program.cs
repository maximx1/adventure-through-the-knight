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
            Console.WriteLine(tmp(1, null));
        }

        static String tmp(int num, String tmp = "gorga")
        {
            Console.WriteLine(tmp);
            if (tmp == null)
                tmp ="null";
            return tmp;
        }
    }
}
