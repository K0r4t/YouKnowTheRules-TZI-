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
    }
}
