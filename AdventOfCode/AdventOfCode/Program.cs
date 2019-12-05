using AdventOfCode.Day1;
using AdventOfCode.Day2;
using System;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            //Day 1
            Console.WriteLine(daySeparator(1));
            FuelCalculator calc = new FuelCalculator();
            Console.WriteLine("Fuel needed: {0}", calc.getNeededFuel());
            Console.WriteLine("Total Fuel: {0}", calc.getTotalFuel());

            //Day 2
            Console.WriteLine(daySeparator(2));
            OpCodeProcessor opCodeProcessor = new OpCodeProcessor();
            opCodeProcessor.displaySegments();
            opCodeProcessor.process();
            opCodeProcessor.displaySegments();



        }

        static string daySeparator(int day)
        {
            var separator = "";
            var length = 20;
            

            for (int i=0; i<length; i++)
            {
                separator += "=";
            }
            separator += "\n";
            for (int i = 0; i < length/2 - day.ToString().Length ; i++)
            {
                separator += " ";
            }
            separator += $"{day}\n";
            for (int i = 0; i < length; i++)
            {
                separator += "=";
            }
            separator += "\n\n";
            return separator;
        }
    }
}
