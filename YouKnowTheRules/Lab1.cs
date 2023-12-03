using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace YouKnowTheRules
{
    public class Lab1
    {
        JustVisual justVisual = new JustVisual();
        Lab1Math math1 = new Lab1Math();

        int[] map = { 1, 2 };
        int pointer = 0;
        int menucheckerresult = 0;


        string[] menu = { "Output here", "Save in file" };


        ConsoleKeyInfo input;
        bool stopper = false;
        long n = 10000000;
        //int m = 134217727;
        //int a = 2744;
        //int c = 2584;
        int m, a, c;
        int period = 0;
        double firstgenerated;
        int max_element;

        double[] x = new double[10000000];

        public Lab1()
        {
            //x[0] = 17;
        }

        public int MaxElement()
        {
            string maxElem;

            Console.WriteLine(" How many first numbers do you want to see? Enter needed number:");
            Console.Write(" ");

            do
            {
                maxElem = Console.ReadLine();
                char[] ch = new char[maxElem.Length];
                bool checker = true;

                for (int i = 0; i < maxElem.Length; i++) ch[i] = maxElem[i];
                for (int i = 0; i < maxElem.Length; i++) if (!Char.IsDigit(ch[i])) checker = false;

                if (checker == false) Console.WriteLine("Can't identify a number. Please try again:");
                else break;
            } while (true);
            int maxE = Int32.Parse(maxElem);

            menucheckerresult = math1.AllMath(n, x, m, a, c);

            if (menucheckerresult < maxE)
            {
                Console.WriteLine("Our period is smaller than wanted sequence size. You will see all sequence");
                Thread.Sleep(2000);
                maxE = menucheckerresult;
            }

            Console.Clear();
            justVisual.ShowUp();
            return maxE;
        }

        public void OutputHere()
        {
            Console.Clear();
            Console.SetWindowSize(120, 32);
            justVisual.ShowUp();

            max_element = MaxElement();  //////

            Console.WriteLine(" Parameters:");
            Console.WriteLine(" m: " + m + ", a: " + a + ", c: " + c + ", X1: " + x[0]);
            menucheckerresult = math1.AllMath(max_element, x, m, a, c);
            Console.WriteLine(" Period: " + menucheckerresult);
            Console.WriteLine(" Our first " + max_element + " elements of a sequence:\n" + " ");
            for (int i = 0; i < max_element; i++)
            {
                if (i != max_element - 1) Console.Write(x[i] + ", ");
                else Console.Write(x[i]);
            }



            justVisual.ShowDown(2);
            input = Console.ReadKey();
            Console.SetWindowSize(40, 12);
        }

        public void ShowInFile()
        {
            //max_element = MaxElement();
            menucheckerresult = math1.AllMath(n, x, m, a, c);
            string filePath = "result.txt";

            using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 8192))
            {
                StreamWriter writer = new StreamWriter(fileStream);

                writer.WriteLine(" Parameters:");
                writer.WriteLine(" m: " + m.ToString() + ", a: " + a.ToString() + ", c: " + c.ToString() + ", X1: " + x[0].ToString());
                writer.WriteLine(" Period: " + menucheckerresult.ToString());
                writer.WriteLine(" Our sequence:\n" + " ");

                for (int i = 0; i < menucheckerresult; i++)
                {
                    writer.Write(x[i].ToString() + " ");
                }
            }
        }

        public void MenuChecker()
        {
            if (pointer == 0) OutputHere();
            else ShowInFile();
        }

        public void Menu()
        {
            for (int i = 0; i<2; i++)
            {
                if (i == pointer) Console.Write(" #" + menu[i] + "\n");
                else Console.Write("  " + menu[i] + "\n");
            }
        }

        public void fileinicialization()
        {
            do
            {
                string fileforread = "config.txt";

                if (File.Exists(fileforread))
                {
                    using (StreamReader reader = new StreamReader(fileforread))
                    {
                        string checkline;
                        char[] ch;
                        string[] lines = File.ReadAllLines(fileforread);
                        int startPosition = 5;
                        bool checker = true;
                        bool megachecker = false;


                        //////////
                        for(int i = 0; i < 4; i++)
                        {
                            checkline = lines[0].Substring(startPosition - 1);
                            ch = new char[checkline.Length];
                            checker = true;
                            for (int j = 0; j < checkline.Length; j++) ch[j] = checkline[j];
                            for (int j = 0; j < checkline.Length; j++) if (!Char.IsDigit(ch[j])) checker = false;

                            switch (i)
                            {
                                case 0:
                                    if (checker == false)
                                    {
                                        Console.WriteLine("Can't identify a number. M will be a default: 33554431");
                                        m = 33554431;
                                        megachecker = true;
                                    }
                                    else m = int.Parse(lines[0].Substring(startPosition - 1));  /////////////

                                    break;
                                case 1:
                                    if (checker == false)
                                    {
                                        Console.WriteLine("Can't identify a number. A will be a default: 1728");
                                        a = 1728;
                                        megachecker = true;
                                    }
                                    else a = int.Parse(lines[1].Substring(startPosition - 1));  /////////////

                                    break;
                                case 2:
                                    if (checker == false)
                                    {
                                        Console.WriteLine("Can't identify a number. C will be a default: 987");
                                        c = 987;
                                        megachecker = true;
                                    }
                                    else c = int.Parse(lines[2].Substring(startPosition - 1));  /////////////

                                    break;
                                case 3:
                                    if (checker == false)
                                    {
                                        Console.WriteLine("Can't identify a number. X1 will be a default: 11");
                                        x[0] = 11;
                                        megachecker = true;
                                    }
                                    else x[0] = int.Parse(lines[3].Substring(startPosition - 1));  /////////////

                                    break;
                            }
                            checker = true;
                        }
                        //////////


                        if (megachecker == true) Thread.Sleep(5000);

                        //Console.WriteLine("m: " + m);
                        //Console.WriteLine("a: " + a);
                        //Console.WriteLine("c: " + c);
                        //Console.WriteLine("x1: " + x[0]);

                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Program can't open config file. Program will make it own using default parameters");
                    Thread.Sleep(5000);
                    using (FileStream fileStream = new FileStream(fileforread, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 8192))
                    {
                        using (StreamWriter writer = new StreamWriter(fileStream))
                        {
                            writer.WriteLine("M = 134217727");
                            writer.WriteLine("A = 2744");
                            writer.WriteLine("C = 2584");
                            writer.WriteLine("X1 = 17");
                        }
                    }
                }
            } while (true);
        }

        public void ShowLab1()
        {
            fileinicialization();

            do
            {
                stopper = false;
                justVisual.ShowUp();

                Menu();

                justVisual.ShowDown(3);

                input = Console.ReadKey();


                switch (input.Key)
                {
                    case ConsoleKey.DownArrow:
                        {
                            if (pointer < 1) pointer++;
                            Menu();
                            break;
                        }
                    case ConsoleKey.UpArrow:
                        {
                            if (pointer > 0) pointer--;
                            Menu();
                            break;
                        }

                    case ConsoleKey.LeftArrow:
                        Console.Clear();
                        stopper = true;
                        break;
                    case ConsoleKey.RightArrow:
                        MenuChecker();
                        break;
                }

                if (stopper == true) break;
            } while (true);
        }
    }
}