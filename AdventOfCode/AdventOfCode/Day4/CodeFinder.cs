using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Day4
{
    class CodeFinder
    {
        public CodeFinder(int min, int max)
        {
            this.min = min;
            this.max = max;
        }
        private int min;
        private int max;

        public int possibleCodes()
        {
            var possibilities = 0;
            for (int i=min; i<max; i++)
            {
                if (testCode(i))
                {
                    possibilities++;
                }
            }
            return possibilities;
        }
        
        public bool testCode(int code)
        {
            if (code.ToString().Length != 6)
            {
                return false;
            }
            if (min > code || max < code)
            {
                return false;
            }
            //if (!_atleastTwoAdjacent(code))
            if (!_exactlyTwoAdjacent(code))
            {
                return false;
            }
            if (!_onlyIncreases(code))
            {
                return false;
            }
            return true;
        }

        //Part 1
        private bool _atleastTwoAdjacent(int code)
        {
            var vals = code.ToString().ToCharArray();
            for (int i=0; i<vals.Length; i++)
            {
                if (i + 1 == vals.Length) break;
                if (vals[i] == vals[i + 1]) return true;
            }
            return false;
        }
        //Part 2
        public bool _exactlyTwoAdjacent(int code)
        {
            var vals = code.ToString().ToCharArray();
            char prev = vals[0];
            int previousMatches = 0;
            for (int i = 1; i < vals.Length; i++)
            {
                var c = vals[i];
                if (prev == c) previousMatches++;
                else
                {
                    if (previousMatches == 1) return true;
                    else
                    {
                        previousMatches = 0;
                    }
                }
                prev = c;
            }
            if (previousMatches == 1) return true;
            return false;
        }
        private bool _onlyIncreases(int code)
        {
            var vals = code.ToString().ToCharArray();
            for (int i=1; i<vals.Length; i++)
            {
                var prev = Int32.Parse(vals[i-1].ToString());
                var cur = Int32.Parse(vals[i].ToString());
                if (prev > cur) return false;
            }
            return true;
        }
    }
}
