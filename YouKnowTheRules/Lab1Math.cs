using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouKnowTheRules
{
    public class Lab1Math
    {
        int period = 0;
        //int m, a, c;
        double firstgenerated;


        public int AllMath(long n, double[] x, int m, int a, int c)
        {
            for (int i = 0; i < n - 1; i++)
            {
                x[i + 1] = (a * x[i] + c) % m;

                if (i == 1) firstgenerated = x[i];

                if (x[i + 1] == firstgenerated) break;
                else period++;
            }
            //firstgenerated = -1;
            //menucheckerresult = period;
            return period;
        }

        public byte[] genIv(int size, int m, int a, int c)
        {
            double[] x = new double[size];
            x[0] = new Random().NextDouble() * m; 

            byte[] iv = new byte[size];
            for (int i = 0; i < size; i++)
            {
                x[i] = (a * x[i] + c) % m;
                iv[i] = (byte)(x[i] % 256);
            }

            return iv;
        }
    }
}
