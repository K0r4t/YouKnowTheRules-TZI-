using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouKnowTheRules
{
    public class Wrapper
    {
        int width = 40;
        int height = 12;
        int[] map = { 1, 2, 3, 4, 5 };
        int start_location = 0;

        Lab1 lab1 = new Lab1();
        Lab2 lab2 = new Lab2();
        Lab3 lab3 = new Lab3();
        Lab4 lab4 = new Lab4();
        Lab5 lab5 = new Lab5();

        JustVisual justVisual = new JustVisual();


        public void center(int lab_number)
        {
            justVisual.ShowUp();
            foreach (int i in map)
            {
                if (i == lab_number) Console.Write(" #lab" + i + "\n");
                else Console.Write("  lab"+i+"\n");
            }
            justVisual.ShowDown(0);
        }

        public void GoToLab(int lab_number)
        {
            Console.Clear();

            switch (lab_number)
            {
                case 1:
                    {
                        lab1.ShowLab1();
                        break;
                    }
                case 2:
                    {
                        lab2.ShowLab2();
                        break;
                    }
                case 3:
                    {
                        lab3.ShowLab3();
                        break;
                    }
                case 4:
                    {
                        lab4.ShowLab4();
                        break;
                    }
                case 5:
                    {
                        lab5.ShowLab5();
                        break;
                    }
            }
        }
    }
}
