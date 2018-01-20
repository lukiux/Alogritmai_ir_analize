using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merge_Sort3
{
    class MyDataArray : DataArray
    {
        double[] data;
        public MyDataArray(int n, int seed)
        {
            data = new double[n];
            length = n;
            Random rand = new Random(seed);
            for (int i = 0; i < length; i++)
                data[i] = rand.NextDouble();
        }
        public override double this[int index]
        {
            get { Merge_Sort.opDouct = Merge_Sort.opDouct + 1;  return data[index]; }
        }
        public override void MainMerge(DataArray items, int left, int mid, int mid1, int right)
        {
            int size = (right - left) + 1;            
            double[] temp = new double[length];
            int i;
            i = left;
            Merge_Sort.opDouct = Merge_Sort.opDouct + 2;
            while (left <= mid && mid1 <= right)
            {
                if (items[left] <= items[mid1])
                    temp[i++] = items[left++];
                else
                    temp[i++] = items[mid1++];
                Merge_Sort.opDouct = Merge_Sort.opDouct + 2;
            }
            Merge_Sort.opDouct = Merge_Sort.opDouct + 3;
            while (left <= mid)
            {
                temp[i++] = items[left++];
                Merge_Sort.opDouct = Merge_Sort.opDouct + 1;
            }
            while (mid1 <= right)
            {
                temp[i++] = items[mid1++];
                Merge_Sort.opDouct = Merge_Sort.opDouct + 1;
            }
            for (int j = 0; j < size; j++)
            {                
                data[right] = temp[right];
                right--;
                Merge_Sort.opDouct = Merge_Sort.opDouct + 1;
            }
            Merge_Sort.opDouct = Merge_Sort.opDouct + 3;
        }
        public override void Swap(int j, double right)
        {
        }
    }
    class MyFileArray : DataArray
    {
        public MyFileArray(string filename, int n, int seed)
        {
            double[] data = new double[n];
            length = n;
            Random rand = new Random(seed);
            for (int i = 0; i < length; i++)
                data[i] = rand.NextDouble();
            if (File.Exists(filename)) File.Delete(filename);
            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(filename, FileMode.Create)))
                {
                    for (int j = 0; j < length; j++)
                        writer.Write(data[j]);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
        public FileStream fs { get; set; }
        public override double this[int index]
        {
            get
            {
                Byte[] data = new Byte[8];
                fs.Seek(8 * index, SeekOrigin.Begin);
                fs.Read(data, 0, 8);
                double result = BitConverter.ToDouble(data, 0);
                Merge_Sort.opDouct = Merge_Sort.opDouct + 1;
                return result;
            }
        }
        public override void MainMerge(DataArray items, int left, int mid, int mid1, int right)
        {

            int size = (right - left) + 1;
            double[] temp = new double[length];
            int i;
            i = left;
            Merge_Sort.opDouct = Merge_Sort.opDouct + 2;
            while (left <= mid && mid1 <= right)
            {
                if (items[left] <= items[mid1])
                    temp[i++] = items[left++];
                else
                    temp[i++] = items[mid1++];
                Merge_Sort.opDouct = Merge_Sort.opDouct + 2;
            }
            Merge_Sort.opDouct = Merge_Sort.opDouct + 3;
            while (left <= mid)
            {
                temp[i++] = items[left++];
                Merge_Sort.opDouct = Merge_Sort.opDouct + 1;
            }
            while (mid1 <= right)
            {
                temp[i++] = items[mid1++];
                Merge_Sort.opDouct = Merge_Sort.opDouct + 1;
            }
            for (int j = 0; j < size; j++)
            {
                items.Swap(right, temp[right]);
                right--;
            }
            Merge_Sort.opDouct = Merge_Sort.opDouct + 3;
        }
        public override void Swap(int j, double right)
        {
            Byte[] Data = new Byte[8];
            BitConverter.GetBytes(right).CopyTo(Data, 0);
            fs.Seek(8 * j, SeekOrigin.Begin);
            fs.Write(Data, 0, 8);
            Merge_Sort.opDouct = Merge_Sort.opDouct + 1;
        }
    }
}
