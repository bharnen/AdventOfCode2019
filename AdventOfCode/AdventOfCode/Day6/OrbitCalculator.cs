using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Day6
{
    class OrbitCalculator
    {
        private const char ORBIT_INDICATOR = ')';


        public OrbitCalculator(List<string> treeList) 
        {
            var sortedList = sortTreeList(treeList);
            var sortedStringList = new List<string>();
            sortedList.ForEach(val => sortedStringList.Add(val.Item1.ToString() + ")" + val.Item2.ToString()));
            tree = new Tree(sortedStringList);
        }

        private Tree tree;

        //Part 1
        public int getOrbitCount()
        {            
            return tree.countOrbits();
        }

        //Part 2
        public int getShortestDistance(string origin, string destination)
        {
            return tree.getShortestDistance(origin, destination);
        }

        private List<Tuple<string, string>> sortTreeList(List<string> list)
        {
            var sortedValues = new List<Tuple<string, string>>();
            var data = organizeData(list);
            var root = getRootNode(data.Item2, data.Item3);
            var tupleList = data.Item1;

            var processed = new HashSet<string>();
            var keysToAdd = new Queue<string>();
            keysToAdd.Enqueue(root);
            while (keysToAdd.Count > 0)
            {
                var key = keysToAdd.Dequeue();
                var values = tupleList.FindAll( s => s.Item1.Equals(key));
                values.ForEach(val =>
                {
                    if (!processed.Contains(val.Item1))
                    {
                        keysToAdd.Enqueue(val.Item1);
                    }
                    if (!processed.Contains(val.Item2))
                    {
                        keysToAdd.Enqueue(val.Item2);
                    }    
                    if (!sortedValues.Contains(val))
                    {
                        sortedValues.Add(val);
                    }
                });
                processed.Add(key);
            }
            return sortedValues;
        }
        private string getRootNode(HashSet<string> left, HashSet<string> right)
        {
            var root = new HashSet<string>(left);
            root.ExceptWith(right);
            var value = new string[root.Count];
            root.CopyTo(value);
            return value[0];
        }


        private Tuple<List<Tuple<string, string>>, HashSet<string>, HashSet<string>> organizeData(List<string> list)
        {
            var mainList = new List<Tuple<string, string>>();
            var lefts = new HashSet<string>();
            var rights = new HashSet<string>();
            foreach (string s in list)
            {
                var leftAndRight = parseString(s);
                mainList.Add(leftAndRight);
                lefts.Add(leftAndRight.Item1);
                rights.Add(leftAndRight.Item2);
            }
            return new Tuple<List<Tuple<string, string>>, HashSet<string>, HashSet<string>>(mainList, lefts, rights);
        }
        private Tuple<string, string> parseString(string input)
        {
            var separatorIndex = input.IndexOf(ORBIT_INDICATOR);
            var left = input.Substring(0, separatorIndex);
            var right = input.Substring(separatorIndex + 1);
            return new Tuple<string, string>(left, right);
        }









    }

}
