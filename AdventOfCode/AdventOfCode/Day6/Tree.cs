using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Day6
{


    public class Tree
    {
        private const char ORBIT_INDICATOR = ')';

        public Tree(List<string> input)
        {
            root = generateTree(input);
        }

        private Node<string> root = null;
        private HashSet<Node<string>> nodeSet = new HashSet<Node<string>>();

        public int countOrbits()
        {
            List<Node<string>> nodeList = getNodeList();
            int orbits = 0;
            foreach (Node<string> node in nodeList)
            {
                var curNode = node;
                int nodeOrbits = 0;
                while (curNode.Parent != null)
                {
                    nodeOrbits++;
                    curNode = curNode.Parent;
                }
                orbits += nodeOrbits;
            }
            return orbits;
        }

        public int getShortestDistance(string origin, string destination)
        {
            var originParents = getParents(origin);
            var destinationParents = getParents(destination);
            int steps = 0;
            string closestSharedParent = "";
            foreach (string s in originParents)
            {
                steps++;
                if (destinationParents.Contains(s))
                {
                    closestSharedParent = s;
                    break;
                }
            }
            var destinationNode = findNodeInTree(destination);
            while (destinationNode.Data != closestSharedParent)
            {
                destinationNode = destinationNode.Parent;
                steps++;
            }
            return steps -= 2; //account for origin & destination orbiting their first steps
        }

        public List<string> getParents(string nodeData)
        {
            List<string> parents = new List<string>();
            Node<string> node = findNodeInTree(nodeData);
            while (node.Parent != null)
            {
                node = node.Parent;
                parents.Add(node.Data);
            }
            return parents;
        }


        public Node<string> findNodeInTree(string data)
        {
            return getNodeList().Find(node => node.Data.Equals(data));
        }
        public Node<string> findNodeInTree(Node<string> data)
        {
            return getNodeList().Find(node => node.Equals(data));
        }

        protected Node<string> generateTree(List<string> input)
        {
            root = null;
            nodeSet = new HashSet<Node<string>>();
            foreach (string s in input)
            {
                Node<string> nodeWithChild = convertInputToNode(s);

                if (root != null)
                {
                    Node<string> nodeInTree = findNodeInTree(nodeWithChild);
                    nodeWithChild.Children.ForEach(child => {
                        child.Parent = nodeInTree;
                        nodeInTree.Children.Add(child);
                    });
                }
                else
                {
                    root = nodeWithChild;
                }
                nodeSet.Add(nodeWithChild);
                nodeWithChild.Children.ForEach(n => nodeSet.Add(n));
            }
            return root;
        }


        private Node<string> convertInputToNode(string input)
        {
            var separator = input.IndexOf(ORBIT_INDICATOR);
            var nodeData1 = input.Substring(0, separator);
            var nodeData2 = input.Substring(separator + 1);
            Node<string> nodeOne = new Node<string>(nodeData1);
            Node<string> nodeTwo = new Node<string>(nodeData2);
            nodeTwo.Parent = nodeOne;
            nodeOne.Children.Add(nodeTwo);
            return nodeOne;
        }

        private List<Node<string>> getNodeList()
        {
            Node<string>[] nodes = new Node<string>[nodeSet.Count];
            nodeSet.CopyTo(nodes);
            return new List<Node<string>>(nodes);
        }




        public class Node<T>
        {
            public Node()
            {
                this._children = new List<Node<T>>();
            }
            public Node(T data) : this()
            {
                this._data = data;
            }

            private Node<T> _parent;
            public Node<T> Parent
            {
                get => _parent;
                set => _parent = value;
            }

            private T _data;
            public T Data
            {
                get => _data;
                set => _data = value;
            }

            private List<Node<T>> _children;
            public List<Node<T>> Children
            {
                get => _children;
                set => _children = value;
            }
            public override bool Equals(object obj)
            {
                try
                {
                    Node<T> node = (Node<T>)obj;
                    return this.EqualTo(node);
                }
                catch (Exception e) { }
                return _data.Equals(obj);
            }
            public override int GetHashCode()
            {
                return 13 * _data.GetHashCode();
            }
            public bool EqualTo(Node<T> obj)
            {
                return _data.Equals(obj.Data);
            }
        }

    }
}
