using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Insertion_sort
{
    class Array : DataArray
    {
        double[] data;
        public Array(int n, int seed)
        {
            data = new double[n];
            length = n;
            Random rand = new Random(seed);
            for (int i = 0; i < length; i++)
                data[i] = rand.NextDouble();
        }
        public override double this[int index]
        {
            get { return data[index];}
        }
        public override void Swap(int j, double a, double b)
        {
            data[j - 1] =a;
            data[j] = b;
        }   

}
}
