using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merge_Sort3
{
    class MyDataList : DataList
    {
        class MyLinkedListNode
        {
            public MyLinkedListNode nextNode { get; set; }
            public MyLinkedListNode prevNode { get; set; }
            public double data { get; set; }
            public MyLinkedListNode(double data)
            {
                this.data = data;
            }
        }
        MyLinkedListNode headNode;
        MyLinkedListNode prevNode;
        MyLinkedListNode currentNode;
        public MyDataList(int n, int seed)
        {
            length = n;
            Random rand = new Random(seed);
            headNode = new MyLinkedListNode(rand.NextDouble());
            currentNode = headNode;
            for (int i = 1; i < length; i++)
            {
                prevNode = currentNode;
                currentNode.nextNode = new MyLinkedListNode(rand.NextDouble());
                currentNode = currentNode.nextNode;
                currentNode.prevNode = prevNode;
            }            
            currentNode.nextNode = null;
        }
        public override double Head()
        {            
            currentNode = headNode;
            prevNode = null;
            Merge_Sort.opDouct = Merge_Sort.opDouct + 1;
            return currentNode.data;
        }
        public override double Next()
        {
            prevNode = currentNode;
            currentNode = currentNode.nextNode;
            Merge_Sort.opDouct = Merge_Sort.opDouct + 1;
            return currentNode.data;
        }        
        public double getData(int index)
        {
            int i = 0;
            currentNode = headNode;
            Merge_Sort.opDouct = Merge_Sort.opDouct + 1;
            while (i < index)
            {
                currentNode = currentNode.nextNode;
                i++;
                Merge_Sort.opDouct = Merge_Sort.opDouct + 1;
            }
            return currentNode.data;
        }
        public override void MainMerge(DataList items, int left, int mid, int mid1, int right)
        {
            double[] temp = new double[length];
            int size = (right - left) + 1;
            int i;
            i = left;
            Merge_Sort.opDouct = Merge_Sort.opDouct + 2;
            while (left <= mid && mid1 <= right)
            {
                if (getData(left) <= getData(mid1))
                    temp[i++] = getData(left++);
                else
                    temp[i++] = getData(mid1++);
                Merge_Sort.opDouct = Merge_Sort.opDouct + 2;
            }
            Merge_Sort.opDouct = Merge_Sort.opDouct + 3;
            while (left <= mid)
            {
                temp[i++] = getData(left++);
                Merge_Sort.opDouct = Merge_Sort.opDouct + 1;
            }
            while (mid1 <= right)
            {
                temp[i++] = getData(mid1++);
                Merge_Sort.opDouct = Merge_Sort.opDouct + 1;
            }
            currentNode = headNode;
            Merge_Sort.opDouct = Merge_Sort.opDouct + 1;
            for (int j = 0; j < right; j++)
            {
                currentNode = currentNode.nextNode;
                Merge_Sort.opDouct = Merge_Sort.opDouct + 1;
            }
            for (int j = 0; j < size; j++)
            {
                currentNode.data = temp[right];
                currentNode = currentNode.prevNode;
                right--;
                Merge_Sort.opDouct = Merge_Sort.opDouct + 2;
            }
            Merge_Sort.opDouct = Merge_Sort.opDouct + 4;
        }
    }
    class MyFileList : DataList
    {
        int prevNode;
        int currentNode;
        int nextNode;
        public MyFileList(string filename, int n, int seed)
        {
            length = n;
            Random rand = new Random(seed);
            if (File.Exists(filename)) File.Delete(filename);
            try
            {
                using (BinaryWriter writter = new BinaryWriter(File.Open(filename, FileMode.Create)))
                {
                    writter.Write(4);
                    for (int i = 0; i < length; i++)
                    {
                        writter.Write(rand.NextDouble());
                        writter.Write((i + 1) * 12 + 4);
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public FileStream fs { get; set; }
        public override double Head()
        {
            Byte[] data = new Byte[12];
            fs.Seek(0, SeekOrigin.Begin);
            fs.Read(data, 0, 4);
            currentNode = BitConverter.ToInt32(data, 0);
            prevNode = -1;
            fs.Seek(currentNode, SeekOrigin.Begin);
            fs.Read(data, 0, 12);
            double result = BitConverter.ToDouble(data, 0);
            nextNode = BitConverter.ToInt32(data, 8);
            Merge_Sort.opDouct = Merge_Sort.opDouct + 2;
            return result;
        }
        public override double Next()
        {
            Byte[] data = new Byte[12];
            fs.Seek(nextNode, SeekOrigin.Begin);
            fs.Read(data, 0, 12);
            prevNode = currentNode;
            currentNode = nextNode;
            double result = BitConverter.ToDouble(data, 0);
            nextNode = BitConverter.ToInt32(data, 8);
            Merge_Sort.opDouct = Merge_Sort.opDouct + 1;
            return result;
        }
        public double getData(int index)
        {
            int i = 0;
            double result = Head();
            while (i < index)
            {
                result = Next();
                i++;
            }
            return result;
        }
        public override void MainMerge(DataList items, int left, int mid, int mid1, int right)
        {
            double[] temp = new double[length];
            int size = (right - left) + 1;
            int i;
            i = left;
            Merge_Sort.opDouct = Merge_Sort.opDouct + 2;
            while (left <= mid && mid1 <= right)
            {
                if (getData(left) <= getData(mid1))
                    temp[i++] = getData(left++);
                else
                    temp[i++] = getData(mid1++);
                Merge_Sort.opDouct = Merge_Sort.opDouct + 2;
            }
            Merge_Sort.opDouct = Merge_Sort.opDouct + 3;
            while (left <= mid)
            {
                temp[i++] = getData(left++);
                Merge_Sort.opDouct = Merge_Sort.opDouct + 1;
            }
            while (mid1 <= right)
            {
                temp[i++] = getData(mid1++);
                Merge_Sort.opDouct = Merge_Sort.opDouct + 1;
            }
            Merge_Sort.opDouct = Merge_Sort.opDouct + 2;
            Byte[] data = new Byte[20];
            fs.Seek(0, SeekOrigin.Begin);
            fs.Read(data, 0, 4);
            currentNode = BitConverter.ToInt32(data, 0);
            fs.Seek(currentNode, SeekOrigin.Begin);
            fs.Read(data, 0, 12);
            nextNode = BitConverter.ToInt32(data, 8);
            Merge_Sort.opDouct = Merge_Sort.opDouct + 2;
            Byte[] data1 = null;
            for (int k = 0; k <= right; k++)
            {
                if (temp[k] > 0)
                {
                    for (int j = 0; j < size; j++)
                    {
                        fs.Seek(currentNode, SeekOrigin.Begin);
                        data1 = BitConverter.GetBytes(temp[k]);
                        fs.Write(data1, 0, 8);
                        fs.Seek(nextNode, SeekOrigin.Begin);
                        fs.Read(data, 0, 12);
                        currentNode = nextNode;
                        nextNode = BitConverter.ToInt32(data, 8);
                        k++;
                        Merge_Sort.opDouct = Merge_Sort.opDouct + 3;
                    }
                    Merge_Sort.opDouct = Merge_Sort.opDouct + 1;
                    break;                    
                }
                else
                {
                    fs.Seek(nextNode, SeekOrigin.Begin);
                    fs.Read(data, 0, 12);
                    currentNode = nextNode;
                    nextNode = BitConverter.ToInt32(data, 8);
                    Merge_Sort.opDouct = Merge_Sort.opDouct + 2;
                }
                Merge_Sort.opDouct = Merge_Sort.opDouct + 1;
            }
            Merge_Sort.opDouct = Merge_Sort.opDouct + 2;
        }
    }
}
