using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Day2
{
    class OpCodeProcessor
    {
        public OpCodeProcessor()
        {
            int segmentSize = 4;
            List<int> list = new List<int>();
            for (int i=0; i<input.Length; i++)
            {
                if (i % segmentSize == 0)
                {
                    opCodeSegments.Add(list);
                    list = new List<int>();
                }
                list.Add(input.ElementAt(i));
            }
            if (list.Count > 0)
            {
                opCodeSegments.Add(list);
            }
        }

        int[] input = new int[] { 1, 0, 0, 0, 99 };
        List<List<int>> opCodeSegments = new List<List<int>>();

        const int ADD = 1;
        const int MULTIPLY = 2;
        const int END = 99;


        int processStep(int step)
        {
            var segment = opCodeSegments.ElementAt(step);
            var operation = segment.ElementAt(0);
            if (operation == END) return 0;
            if (operation != ADD && operation != MULTIPLY) return -1;
            var position1 = segment.ElementAt(1);
            var position2 = segment.ElementAt(2);
            var position3 = segment.ElementAt(3);


        }


        public void displaySegments()
        {
            foreach (List<int> segment in opCodeSegments)
            {
                segment.ForEach(i => Console.Write($"{i} "));
                Console.WriteLine();
            }
        }




    }
}
