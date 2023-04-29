using System;
using System.Collections;

namespace Lab10_IEnumerable
{
    class Node
    {
        public Node(int data = 0, Node left = null, Node right = null)
        {
            Data = data;
            Left = left;
            Right = right;
        }

        public int Data { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }

        public Node Search(int value)
        {
            if (this.Data == value) return this;
            Node foundNode = null;

            if (Left != null) foundNode = Left.Search(value);
            if (foundNode == null && Right != null) foundNode = Right.Search(value);
            else foundNode = null;

            return foundNode;
        }

    }

    // When you implement IEnumerable, you must also implement IEnumerator.
    public class TreeEnum : IEnumerator
    {
        public Node[] TreeNodes;
        int position = -1;

        public TreeEnum(Node[] array)
        {
            TreeNodes = array;
        }

        public bool MoveNext()
        {
            position++;
            return (position < TreeNodes.Length);
        }

        public void Reset()
        {
            position = -1;
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public Node Current
        {
            get
            {
                try
                {
                    return TreeNodes[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }

    class Tree : IEnumerable
     {
        public Node topNode { get; set; } 
        public Tree(Node topNode = null)
        {
            this.topNode = topNode;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public TreeEnum GetEnumerator()
        {
            List<Node> tempNodes = new List<Node>;
            tempNodes.Add(new Node(0, topNode));
            Node currNode;
            int i = 0;
            while (true)
            {
                if (tempNodes[i] == tempNodes.Capacity) break;
                currNode = tempNodes[i];
                if (currNode.Left) != null) tempNodes.Add(currNode.Left);
                if (currNode.Right) != null) tempNodes.Add(currNode.Right);
                i++;
            }
            return new TreeEnum(TreeNodes);
        }

        public void Add(int value)
        {
            Node currentNode = topNode;
            bool hasNotEnded = true;
            while (hasNotEnded)
            {
                if (currentNode.Data < value)
                {
                    if (currentNode.Left != null)
                    {
                        currentNode = currentNode.Left;
                    }
                    else
                    {
                        currentNode.Left = new Node(value);
                        hasNotEnded = false;
                    }
                }
                else
                {
                    if (currentNode.Right != null)
                    {
                        currentNode = currentNode.Right;
                    }
                    else
                    {
                        currentNode.Right = new Node(value);
                        hasNotEnded = false;
                    }
                }
            }
        }
     }


    class Program
    {
        static void Main()
        {
            Node topNode = new Node(13);
            Tree tree = new Tree(topNode);

            // add
            for (int i = 1; i < 15; i++) { tree.Add(new Node(i)); }

            // print
            foreach (Node node in tree) Console.Write($" {node.Data} ");

        }
    }
}
