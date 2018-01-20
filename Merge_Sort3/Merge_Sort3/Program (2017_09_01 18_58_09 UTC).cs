using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merge_Sort3
{
    class Merge_Sort
    {
        public static ulong opMcout { get; set; }
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
            Console.WriteLine("\n Menu Merge Sort\n");
            Console.WriteLine(">1. Test Array / List");
            Console.WriteLine(">2. Test FILE Array / FILE List");
            Console.WriteLine(">3. Analysis Array / List");
            Console.WriteLine(">4. Analysis FILE Array / FILE List");
            Console.WriteLine(">5. Exit \n");
            Console.Write(">");
        }
        public static void MergeSort(DataArray items, int left, int right)
        {
            int mid;
            if (right > left)
            {
                mid = (right + left) / 2;
                MergeSort(items, left, mid);
                MergeSort(items, (mid + 1), right);
                items.MainMerge(items, left, mid, (mid + 1), right);
                opMcout = opMcout + 3;
            }
            opMcout = opMcout + 1;
        }
        
        public static void MergeSort(DataList items, int left, int right)
        {            
            int mid;
            if (right > left)
            {
                mid = (right + left) / 2;
                MergeSort(items, left, mid);
                MergeSort(items, (mid + 1), right);
                items.MainMerge(items, left, mid, (mid + 1), right);
                opMcout = opMcout + 3;
            }
            opMcout = opMcout + 1;
        }
        public static void Test_Array_List(int seed)
        {
            int n = 12;
            MyDataArray myarray = new MyDataArray(n, seed);
            Console.WriteLine("\n ARRAY \n");
            myarray.Print(n);
            MergeSort(myarray, 0, myarray.Length - 1);
            myarray.Print(n);
            MyDataList mylist = new MyDataList(n, seed);
            Console.WriteLine("\n LIST \n");
            mylist.Print(n);
            MergeSort(mylist, 0, mylist.Length - 1);
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
                MergeSort(myfilearray, 0, myfilearray.Length - 1);
                myfilearray.Print(n);
            }
            filename = @"mydatalist.dat";
            MyFileList myfilelist = new MyFileList(filename, n, seed);
            using (myfilelist.fs = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite))
            {
                Console.WriteLine("\n FILE LIST \n");
                myfilelist.Print(n);
                MergeSort(myfilelist, 0, myfilelist.Length - 1);
                myfilelist.Print(n);
            }
        }
        public static void Analysis_Array_List(int seed)
        {
            opMcout = 0;
            opDouct = 0;
            int n = 100;
            Console.WriteLine("\n ARRAY \n       N       RUN  Time          Op M Count       Op D Count\n");
            for (int i = 0; i < 7; i++)
            {
                MyDataArray myarray = new MyDataArray(n, seed);
                Stopwatch myTimer = new Stopwatch();
                myTimer.Start();
                MergeSort(myarray, 0, myarray.Length - 1);
                myTimer.Stop();
                Console.WriteLine(" {0,6:N0}  {1}    {2,15:N0}  {3,15:N0}", n, myTimer.Elapsed, opMcout, opDouct);
                n = n * 2;
                GC.Collect();
            }
            opMcout = 0;
            opDouct = 0;
            n = 100;
            Console.WriteLine("\n LIST \n       N       RUN  Time          Op M Count       Op D Count\n");
            for (int i = 0; i < 7; i++)
            {
                MyDataList mylist = new MyDataList(n, seed);
                Stopwatch myTimer = new Stopwatch();
                myTimer.Start();
                MergeSort(mylist, 0, mylist.Length - 1);
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
                    opMcout = 0;
                    opDouct = 0;
                    myTimer.Start();
                    MergeSort(myfilearray, 0, myfilearray.Length - 1);
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
                    opMcout = 0;
                    opDouct = 0;
                    myTimer.Start();
                    MergeSort(myfilelist, 0, myfilelist.Length - 1);
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
        public abstract void Swap(int a, double b);
        public abstract void MainMerge(DataArray items, int left, int mid, int mid1, int right);
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
        public abstract void MainMerge(DataList items, int left, int mid, int mid1, int right);
        public void Print(int n)
        {
            Console.Write(" {0:F5}", Head());
            for (int i = 1; i < n; i++)
                Console.Write(" {0:F5}", Next());
            Console.WriteLine();
        }
    }
}