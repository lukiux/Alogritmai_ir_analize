using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace Insertion_sort3
{
    class Insertion_Sort
    {
        public static ulong opMcout;
        public static ulong opDouct;
        static void Main(string[] args)
        {
            int seed = (int)DateTime.Now.Ticks & 0x0000FFFF;
            ConsoleKeyInfo cki;
            bool exit = false;
            do
            {
                DisplayMenu();
                cki = Console.ReadKey();
                Console.Clear();
                switch (cki.KeyChar.ToString())
                {
                    case "1":
                        Console.WriteLine(">1. Test");
                        Test_Array_List(seed);
                        break;
                    case "2":
                        Console.WriteLine(">2. Test");
                        Test_File_Array_List(seed);
                        break;
                    case "3":
                        Console.WriteLine(">3. Analysis");
                        Analysis_Array_List(seed);
                        break;
                    case "4":
                        Console.WriteLine(">4. Analysis");
                        Analysis_File_Array_List(seed);
                        break;
                    case "5":
                        exit = true;
                        break;
                }
            } while (exit == false);
            }
        static public void DisplayMenu()
        {
            Console.WriteLine("\n Menu Booble Sort\n");
            Console.WriteLine(">1. Test Array / List");
            Console.WriteLine(">2. Test FILE Array / FILE List");
            Console.WriteLine(">3. Analysis Array / List");
            Console.WriteLine(">4. Analysis FILE Array / FILE List");
            Console.WriteLine(">5. Exit \n");
            Console.Write(">");
        }
        public static void InsertionSort(DataArray items)
        {
            opMcout = 0;
            opDouct = 0;
            for (int i = 0; i < items.Length - 1; i++)
            {
                for (int j = i + 1; j > 0; j--)
                {
                    if (items[j - 1] > items[j])
                    {
                        double temp = items[j - 1];
                        items.Swap(j, items[j], temp);
                        opMcout = opMcout + 1;
                    }
                    opMcout = opMcout + 4;
                }
                opMcout = opMcout + 2;
            }
        }
        public static void InsertionSort(DataList items)
        {
            opMcout = 0;
            opDouct = 0;
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
                        items.Swap(currentdata, prevdata);
                        currentdata = prevdata;
                        opMcout = opMcout + 1;
                    }
                    opMcout = opMcout + 4;
                }
                opMcout = opMcout + 2;
            }
        }
        public static void Test_Array_List(int seed)
        {
            int n = 12;
            MyDataArray myarray = new MyDataArray(n, seed);
            Console.WriteLine("\n ARRAY \n");
            myarray.Print(n);
            InsertionSort(myarray);
            myarray.Print(n);

            MyDataList mylist = new MyDataList(n, seed);
            Console.WriteLine("\n LIST \n");
            mylist.Print(n);
            InsertionSort(mylist);
            mylist.Print(n);
        }
        public static void Test_File_Array_List(int seed)
        {
            int n = 12;
            string filename;
            filename = @"mydataarray.dat";
            MyFileArray myfilearray = new MyFileArray(filename, n, seed);
            using (myfilearray.fs = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite))
            {
                Console.WriteLine("\n FILE ARRAY \n");
                myfilearray.Print(n);
                InsertionSort(myfilearray);
                myfilearray.Print(n);
            }
            filename = @"mydatalist.dat";
            MyFileList myfilelist = new MyFileList(filename, n, seed);
            using (myfilelist.fs = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite))
            {
                Console.WriteLine("\n FILE LIST \n");
                myfilelist.Print(n);
                InsertionSort(myfilelist);
                myfilelist.Print(n);
            }
        }
        public static void Analysis_Array_List(int seed)
        {
            int n = 100;
            Console.WriteLine("\n ARRAY \n       N       RUN  Time          Op M Count       Op D Count\n");
            for (int i = 0; i < 7; i++)
            {
                MyDataArray myarray = new MyDataArray(n, seed);
                Stopwatch myTimer = new Stopwatch();
                myTimer.Start();
                InsertionSort(myarray);
                myTimer.Stop();
                Console.WriteLine(" {0,6:N0}  {1}    {2,15:N0}  {3,15:N0}", n, myTimer.Elapsed, opMcout, opDouct);
                n = n * 2;
                GC.Collect();
            }
            n = 100;
            Console.WriteLine("\n LIST \n       N       RUN  Time          Op M Count       Op D Count\n");
            for (int i = 0; i < 7; i++)
            {
                MyDataList mylist = new MyDataList(n, seed);
                Stopwatch myTimer = new Stopwatch();
                myTimer.Start();
                InsertionSort(mylist);
                myTimer.Stop();
                Console.WriteLine(" {0,6:N0}   {1}   {2,15:N0}  {3,15:N0}", n, myTimer.Elapsed, opMcout, opDouct);
                n = n * 2;
                GC.Collect();
            }
        }
        public static void Analysis_File_Array_List(int seed)
        {
            string filename;
            filename = @"mydataarray.dat";
            int n = 100;
            Console.WriteLine("\n FILE ARRAY \n       N       RUN  Time          Op M Count       Op D Count\n");
            for (int i = 0; i < 7; i++)
            {
                MyFileArray myfilearray = new MyFileArray(filename, n, seed);
                Stopwatch myTimer = new Stopwatch();
                using (myfilearray.fs = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite))
                {
                    myTimer.Start();
                    InsertionSort(myfilearray);
                    myTimer.Stop();
                }
                Console.WriteLine(" {0,6:N0}   {1}   {2,15:N0}  {3,15:N0}", n, myTimer.Elapsed, opMcout, opDouct);
                n = n * 2;
                GC.Collect();
            }
            filename = @"mydatalist.data";
            n = 100;
            Console.WriteLine("\n FILE LIST \n       N       RUN  Time          Op M Count       Op D Count\n");
            for (int i = 0; i < 7; i++)
            {
                MyFileList myfilelist = new MyFileList(filename, n, seed);
                Stopwatch myTimer = new Stopwatch();
                using (myfilelist.fs = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite))
                {
                    myTimer.Start();
                    InsertionSort(myfilelist);
                    myTimer.Stop();
                }
                Console.WriteLine("  {0,6:N0}   {1}   {2,15:N0}   {3,15:N0}", n, myTimer.Elapsed, opMcout, opDouct);
                n = n * 2;
                GC.Collect();
            }
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
