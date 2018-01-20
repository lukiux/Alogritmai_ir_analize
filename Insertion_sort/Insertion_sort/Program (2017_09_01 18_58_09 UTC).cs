using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Insertion_sort
{
    class Program
    {
        static void Main(string[] args)
        {
            int seed = (int)DateTime.Now.Ticks & 0x0000FFFF;

            Test_Array_list(seed);
        }
        public static void InsertionSort(DataArray items)
        {
            for (int i = 0; i < items.Length - 1; i++)
            {
                for (int j = i + 1; j > 0; j--)
                {
                    if (items[j - 1] > items[j])
                    {
                        double temp = items[j - 1];
                        items.Swap(j, items[j], temp);
                    }
                }
            }
        }
        public static void InsertionSort(DataList items)
        {
            double prevdata, currentdata;
            for (int i = 0; i < items.Length - 1; i++)
            {
                
                for (int j = i + 1; j > 0; j--)
                {
                    currentdata = items.Head();
                    for (int k = 1; k < j; k++)
                        currentdata = items.Next();
                    prevdata = currentdata;
                    currentdata = items.Next();
                    if (prevdata > currentdata)
                    {
                        double temp = prevdata;
                        items.Swap(currentdata, temp);
                        currentdata = prevdata;
                    }
                }
            }
        }
        public static void Test_Array_list(int seed)
        {
            int n = 12;
            Array myarray = new Array(n, seed);
            Console.WriteLine("\n ARRAY \n");
            myarray.Print(n);
            InsertionSort(myarray);
            myarray.Print(n);

            List mylist = new List(n, seed);
            Console.WriteLine("\n LIST \n");
            mylist.Print(n);
            InsertionSort(mylist);
            mylist.Print(n);
        }        
    }
    abstract class DataArray
    {
        protected int length;
        public int Length { get { return length; } }
        public abstract double this[int index] { get; }
        public abstract void Swap(int j, double a, double b);
        public void Print(int n)
        {
            for (int i = 0; i < n; i++)
                Console.Write(" {0:F5}", this[i]);
            Console.WriteLine();
        }
    }
    abstract class DataList
    {
        protected int length;
        public int Length { get { return length; } }
        public abstract double Head();
        public abstract double Next();
        public abstract void Swap(double a, double b);
        public void Print(int n)
        {
            Console.Write(" {0:F5}", Head());
            for (int i = 1; i < n; i++)
                Console.Write(" {0:F5}", Next());
            Console.WriteLine();
        }
    }
}
