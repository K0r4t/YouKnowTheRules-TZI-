﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouKnowTheRules
{
    public class Lab3
    {
        public void Menu()
        {
            Console.WriteLine("Lab3 is not ready");
        }

        public void ShowLab3()
        {
            JustVisual justVisual = new JustVisual();
            bool stopper = false;
            ConsoleKeyInfo input;


            do
            {
                stopper = false;
                justVisual.ShowUp();

                Menu();

                justVisual.ShowDown(4);

                input = Console.ReadKey();


                switch (input.Key)
                {
                    //case ConsoleKey.DownArrow:
                    //    {
                    //        if (pointer < 1) pointer++;
                    //        Menu();
                    //        break;
                    //    }
                    //case ConsoleKey.UpArrow:
                    //    {
                    //        if (pointer > 0) pointer--;
                    //        Menu();
                    //        break;
                    //    }

                    case ConsoleKey.LeftArrow:
                        Console.Clear();
                        stopper = true;
                        break;
                        //case ConsoleKey.RightArrow:
                        //    MenuChecker();
                        //    break;
                }

                if (stopper == true) break;
            } while (true);
        }
    }
}
