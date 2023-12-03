using System;
using System.Windows.Input;
using System.Threading;

namespace YouKnowTheRules
{
    class Program
    {
        static void Main()
        {
            Console.SetWindowSize(40, 12);

            Wrapper wrapper = new Wrapper();
            int[] lab_selector = { 1, 2, 3, 4, 5 };
            int lab_number = 1;

            ConsoleKeyInfo input;

            while (true)
            {
                wrapper.center(lab_number);
                input = Console.ReadKey();

                switch (input.Key)
                {
                    case ConsoleKey.DownArrow:
                        {
                            if (lab_number < 5) lab_number++;
                            wrapper.center(lab_number);
                            break;
                        }
                    case ConsoleKey.UpArrow:
                        {
                            if (lab_number > 1) lab_number--;
                            wrapper.center(lab_number);
                            break;
                        }
                    case ConsoleKey.RightArrow:
                        {
                            wrapper.GoToLab(lab_number);
                            break;
                        }
                }
            }
        }
    }
}