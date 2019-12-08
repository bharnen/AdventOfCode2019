using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Day2
{
    class OpCodeProcessor
    {
        public OpCodeProcessor()  {  }

        protected const int ADD = 1;
        protected const int MULTIPLY = 2;
        protected const int END = 99;

        //Part 1
        public List<int> processList(List<int> opCodesList)
        {
            var opCodes = new List<int>(opCodesList);
            var instructionPointer = new int[1] { 0 };
            var code = opCodes[instructionPointer[0]];
            while (code != END)
            {
                code = opCodes[instructionPointer[0]];
                _processCode(opCodes, instructionPointer, code);
            }
            return opCodes;
        }

        //Part 2
        public Tuple<int, int> findNounAndVerb(List<int> opCodesList, int desiredResult)
        {
            var noun = 0;
            var verb = 0;
            while (noun < 100 || verb < 100)
            {
                var opCodes = new List<int>(opCodesList);
                opCodes[1] = noun;
                opCodes[2] = verb;
                opCodes = processList(opCodes);
                if (opCodes[0] == desiredResult) break;
                if (noun == 100)
                {
                    noun = 0;
                    verb++;
                }
                noun++;
            }
            return new Tuple<int, int>(noun, verb);
        }

        protected virtual void _processCode(List<int> opCodes, int[] instructionPointer, int code)
        {
            switch (code)
            {
                case ADD:
                    _add(opCodes, instructionPointer[0]);
                    instructionPointer[0] += 4;
                    return;
                case MULTIPLY:
                    _multiply(opCodes, instructionPointer[0]);
                    instructionPointer[0] += 4;
                    return;
                default:
                    return;
            }
        }

        private void _add(List<int> opCodes, int instructionPointer)
        {
            var one = opCodes[opCodes[instructionPointer + 1]];
            var two = opCodes[opCodes[instructionPointer + 2]];
            var resultLocation = opCodes[instructionPointer + 3];
            opCodes[opCodes[instructionPointer + 3]] = one + two;
        }
        private void _multiply(List<int> opCodes, int position)
        {
            var one = opCodes[opCodes[position + 1]];
            var two = opCodes[opCodes[position + 2]];
            var resultLocation = opCodes[position + 3];
            opCodes[opCodes[position + 3]] = one * two;
        }
        private void _displayCodes(List<int> opCodes)
        {
            for (int i=0; i<opCodes.Count; i++)
            {
                Console.Write(opCodes[i]);
                if (i != opCodes.Count - 1)
                {
                    Console.Write(", ");
                }
                if (i>0 && i%10 == 0)
                {
                    Console.WriteLine();
                }
            }
        }
                     
    }
}
