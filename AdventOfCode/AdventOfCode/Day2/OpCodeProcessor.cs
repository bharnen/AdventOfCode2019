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
            List<int> list = new List<int>();
            for (int i=0; i<input.Length; i++)
            {
                if (i > 0 && i % SEGMENT_LENGTH == 0)
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

        //PUZZLE INPUT
        int[] input = new int[] { 1, 12, 2, 3, 1, 1, 2, 3, 1, 3, 4, 3, 1, 5, 0, 3, 2, 6, 1, 19, 1, 19, 9, 23, 1, 23, 9, 27, 1, 10, 27, 31, 1, 13, 31, 35, 1, 35, 10, 39, 2, 39, 9, 43, 1, 43, 13, 47, 1, 5, 47, 51, 1, 6, 51, 55, 1, 13, 55, 59, 1, 59, 6, 63, 1, 63, 10, 67, 2, 67, 6, 71, 1, 71, 5, 75, 2, 75, 10, 79, 1, 79, 6, 83, 1, 83, 5, 87, 1, 87, 6, 91, 1, 91, 13, 95, 1, 95, 6, 99, 2, 99, 10, 103, 1, 103, 6, 107, 2, 6, 107, 111, 1, 13, 111, 115, 2, 115, 10, 119, 1, 119, 5, 123, 2, 10, 123, 127, 2, 127, 9, 131, 1, 5, 131, 135, 2, 10, 135, 139, 2, 139, 9, 143, 1, 143, 2, 147, 1, 5, 147, 0, 99, 2, 0, 14, 0 };
        List<List<int>> opCodeSegments = new List<List<int>>();

        const int SEGMENT_LENGTH = 4;
        const int ADD = 1;
        const int MULTIPLY = 2;
        const int END = 99;

        //Part 1
        public void process()
        {
            var copy = _getSegmentCopy();
            process(copy);
            displaySegments(copy);
        }
        public void process(List<List<int>> segements)
        {
            for (int i = 0; i < segements.Count; i++)
            {
                if (processStep(i, segements) <= 0) return;
            }
        }

        //Part 2
        public void processToNounAndVerb(int desiredResult)
        { 
            int noun = 0;
            int verb = 0;
            while ( (noun < 100) || (verb < 100) )
            {
                var segmentCopy = _getSegmentCopy();
                segmentCopy[0][1] = noun;
                segmentCopy[0][2] = verb;
                process(segmentCopy);
                if (segmentCopy[0][0] == desiredResult)
                {
                    break;
                }
                if (noun == 100)
                {
                    noun = 0;
                    verb++;
                }
                else
                {
                    noun++;
                }
            }
            Console.WriteLine($"Noun: {noun}, Verb: {verb}");
        }

        public int processStep(int step)
        {
            var segment = opCodeSegments.ElementAt(step);
            var operation = segment.ElementAt(0);
            if (operation == END) return 0;

            var position1 = segment.ElementAt(1);
            var position2 = segment.ElementAt(2);
            var position3 = segment.ElementAt(3);

            var firstNumber = opCodeSegments.ElementAt(position1 / SEGMENT_LENGTH).ElementAt(position1 % SEGMENT_LENGTH);
            var secondNumber = opCodeSegments.ElementAt(position2 / SEGMENT_LENGTH).ElementAt(position2 % SEGMENT_LENGTH);

            switch (operation)
            {
                case ADD:
                    opCodeSegments.ElementAt(position3 / SEGMENT_LENGTH)[(position3 % SEGMENT_LENGTH)] = firstNumber + secondNumber;
                    return 1;
                case MULTIPLY:
                    opCodeSegments.ElementAt(position3 / SEGMENT_LENGTH)[(position3 % SEGMENT_LENGTH)] = firstNumber * secondNumber;
                    return 1;
                default:
                    return -1;
            }
        }
        public int processStep(int step, List<List<int>> segments)
        {
            var segment = segments.ElementAt(step);
            var operation = segment.ElementAt(0);
            if (operation == END) return 0;

            var position1 = segment.ElementAt(1);
            var position2 = segment.ElementAt(2);
            var position3 = segment.ElementAt(3);

            var firstNumber = segments.ElementAt(position1 / SEGMENT_LENGTH).ElementAt(position1 % SEGMENT_LENGTH);
            var secondNumber = segments.ElementAt(position2 / SEGMENT_LENGTH).ElementAt(position2 % SEGMENT_LENGTH);

            switch (operation)
            {
                case ADD:
                    segments.ElementAt(position3 / SEGMENT_LENGTH)[(position3 % SEGMENT_LENGTH)] = firstNumber + secondNumber;
                    return 1;
                case MULTIPLY:
                    segments.ElementAt(position3 / SEGMENT_LENGTH)[(position3 % SEGMENT_LENGTH)] = firstNumber * secondNumber;
                    return 1;
                default:
                    return -1;
            }
        }


        public void displaySegments(List<List<int>> segments)
        {
            foreach (List<int> segment in segments)
            {
                segment.ForEach(i => Console.Write($"{i} "));
                Console.WriteLine();
            }
        }

        private List<List<int>> _getSegmentCopy()
        {
            var opCodeSegmentsCopy = new List<List<int>>();
            opCodeSegments.ForEach(list => opCodeSegmentsCopy.Add(new List<int>(list)));
            return opCodeSegmentsCopy;
        }



    }
}
