using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTree
{
    class Program
    {
        static Random random;
        static void Main(string[] args)
        {
            List<string> nameList = new List<string>();
            BinarySearchTree strTree = new BinarySearchTree();
            int seed = (int)DateTime.Now.Ticks & 0x0000FFFF;
            random = new Random(seed);

            int n = 15;
            for (int i = 0; i < n; i++)
            {
                string s = RandomName(10);
                nameList.Add(s);
                strTree.Insert(s);
            }
            nameList.Add(RandomName(10));
            Console.WriteLine(" Binary Search Tree \n");
            strTree.Print();
            Console.WriteLine("\n Search Test \n");
            foreach (var s in nameList)
                Console.Write(strTree.Contains(s).ToString() + " ");
            Console.WriteLine("\n");
        }
        static string RandomName(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
            builder.Append(ch);
            for (int i = 1; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 97)));
                builder.Append(ch);
            }
            return builder.ToString();
        }
    }
    class TreeNode
    {
        public string Element { get; set; }
        public TreeNode Left { get; set; }
        public TreeNode Right { get; set; }
        public int ElementNum { get; set; }
        public TreeNode(string element, int num)
        {
            this.Element = element;
            this.ElementNum = num;
        }
    }
    class BinarySearchTree
    {
        public TreeNode Root { get; set; }
        int count;
        public BinarySearchTree()
        {
            this.Root = null;
            count = 0;
        }
        public void Insert(string x)
        {
            this.Root = Insert(x, this.Root);
        }
        public bool Contains(string x)
        {
            return Contains(x, this.Root);
        }
        public void Print()
        {
            Print(this.Root);
        }
        private bool Contains(string x, TreeNode t)
        {
            while (t != null)
            {
                if ((x as IComparable).CompareTo(t.Element) < 0)
                    t = t.Left;
                else if ((x as IComparable).CompareTo(t.Element) > 0)
                    t = t.Right;
                else
                    return true;
            }
            return false;
        }
        protected TreeNode Insert(string x, TreeNode t)
        {
            if (t == null)
                t = new TreeNode(x, count++);
            else if ((x as IComparable).CompareTo(t.Element) < 0)
                t.Left = Insert(x, t.Left);
            else if ((x as IComparable).CompareTo(t.Element) > 0)
                t.Right = Insert(x, t.Right);
            else
            {
            }
            return t;
        }
        private void Print(TreeNode t)
        {
            if (t == null)
                return;
            else
            {
                Print(t.Left);
                if (t.Left != null) Console.Write("{0,3:N0} <<-", t.Left.ElementNum); else Console.Write("    ");
                Console.Write("{0,3:N0} {1} ", t.ElementNum, t.Element);
                if (t.Right != null) Console.WriteLine("    ");
                Print(t.Right);
            }
        }
    }
}
