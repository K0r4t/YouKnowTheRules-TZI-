using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouKnowTheRules
{
    public class JustVisual
    {
        public void ShowUp()
        {
            Console.WriteLine("########################################");
            Console.WriteLine("#### Made by Pavlish Maksym KN-316 #####\n");
        }

        public void ShowDown(int counter)
        {
            for (int i = 0; i < counter; i++) Console.WriteLine("");
            Console.WriteLine("\n########################################");
            Console.WriteLine("########################################");
        }

    }
}