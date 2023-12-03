using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace YouKnowTheRules
{
    public class Lab2
    {
        JustVisual justVisual = new JustVisual();
        MD5 md5 = new MD5();

        string[] menu = { "Hash a line", "Hash a file", "Check integrity" };
        int pointer = 0;
        bool isIntegrity = false;

        string testtext;


        //string fileForHash;
        //string fileForHashJustName;

        public void Menu()
        {
            for (int i = 0; i < 3; i++)
            {
                if (i == pointer) Console.Write(" #" + menu[i] + "\n");
                else Console.Write("  " + menu[i] + "\n");
            }
        }

        public void hashText()
        {
            Console.Clear();
            justVisual.ShowUp();
            Console.WriteLine(" Enter text you want to hash:");
            Console.Write(" ");
            string textInput;
            textInput = Console.ReadLine();

            byte[] hash = md5.ComputeHash(Encoding.ASCII.GetBytes(textInput));

            Console.WriteLine("\n Your hash:");
            Console.Write(" ");
            Console.WriteLine(BitConverter.ToString(hash).Replace("-","").ToLower());
            Console.SetWindowSize(40, 13);

            justVisual.ShowDown(0);
            Console.ReadKey();
            Console.SetWindowSize(40, 12);
        }

        public void hashFileIntegrity(string currentDirectory)
        {
            Console.Clear();
            justVisual.ShowUp();
            Console.WriteLine(" Enter text you want to check:");
            Console.Write(" ");
            testtext = Console.ReadLine();
            Console.WriteLine(" Find needed file:");
            Thread.Sleep(1000);
            isIntegrity = true;
            fileNavigator(currentDirectory);
            isIntegrity = false;
        }

        public void hashFileIntegrityLogic(string fileForHash)
        {
            justVisual.ShowUp();
            Console.WriteLine(" Your hash:");
            Console.WriteLine(" " + testtext);

            Console.SetWindowSize(40, 13);
            byte[] fileBytes = File.ReadAllBytes(fileForHash);

            byte[] hash = md5.ComputeHash(fileBytes);

            Console.WriteLine("\n Your hash:");
            Console.WriteLine(" " + BitConverter.ToString(hash).Replace("-", "").ToLower());

            if(testtext != BitConverter.ToString(hash).Replace("-", "").ToLower())
            {
                Console.WriteLine(" Hash isn't the same");
            }
            else
            {
                Console.WriteLine("Everything is fine");
            }

            justVisual.ShowDown(0);

            Console.ReadKey();
        }

        public void hashFile(string fileForHash)
        {
            Console.Clear();
            justVisual.ShowUp();
            Console.WriteLine(" You sure you want to hash this file?");
            Console.Write(" " + Path.GetFileName(fileForHash));

            FileInfo fileInfo = new FileInfo(fileForHash);
            double fileSize = fileInfo.Length;
            double fileSizeInMegabytes = (double)fileSize / (1024 * 1024);
            

            Console.Write("    " + fileSizeInMegabytes.ToString("F6") + "MB" + "?\n");
            Console.WriteLine("\n If you want to hash that file - press \n ENTER. Otherwise press any other button\n");


            ConsoleKeyInfo inputEnter;
            inputEnter = Console.ReadKey();

            switch (inputEnter.Key)
            {
                case ConsoleKey.Enter:
                    {
                        Console.SetWindowSize(40, 17);
                        byte[] fileBytes = File.ReadAllBytes(fileForHash);

                        byte[] hash = md5.ComputeHash(fileBytes);

                        Console.WriteLine("\n Your hash:");
                        Console.WriteLine(" " + BitConverter.ToString(hash).Replace("-", "").ToLower());

                        justVisual.ShowDown(0);

                        Console.ReadKey();
                        break;
                    }
                default:
                    break;
            }
        }

        public void fileNavigator(string currentDirectory)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(currentDirectory);
            int innerPointer = 0; 
            bool stopper = false;
            while (stopper == false)
            {
                justVisual.ShowUp();             

                DirectoryInfo[] directories = directoryInfo.GetDirectories();             
                
                FileInfo[] files = directoryInfo.GetFiles();
                Console.WriteLine(directoryInfo.Name + ":");

                int temp = 0;

                foreach (DirectoryInfo dir in directories) temp++;
                foreach (FileInfo file in files) temp++;

                int width = 8 + temp; //спочатку там 12
                int bufferheight = Console.BufferHeight;
                int bufferWidth = Console.BufferWidth;
                int maxNameLength = directories.Concat<FileSystemInfo>(files).Max(fsi => fsi.Name.Length);
 
                try
                {
                    Console.SetWindowSize(40, width);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WindowHeight = Math.Min(Console.BufferHeight, 40);
                }

                try
                {
                    Console.WindowWidth = Math.Max(Console.WindowWidth, maxNameLength + 5);
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WindowWidth = Console.LargestWindowWidth;
                }



                for (int i = 0; i < directories.Length; i++) 
                {
                    if (i == innerPointer) Console.Write(" #" + directories[i].Name + "\n");
                    else Console.Write("  " + directories[i].Name + "\n");
                }

                for (int i = directories.Length; i < files.Length+directories.Length; i++)
                {
                    //Console.WriteLine(file.Name);
                    if (i == innerPointer) Console.Write(" #" + files[i - directories.Length].Name + "\n");
                    else Console.Write("  " + files[i - directories.Length].Name + "\n");
                    //innerPointer++;
                }

                justVisual.ShowDown(0);



                ConsoleKeyInfo input;
                input = Console.ReadKey();

                switch (input.Key)
                {
                    case ConsoleKey.DownArrow:
                        {
                            if (innerPointer < temp-1) innerPointer++;
                            //Menu();
                            break;
                        }
                    case ConsoleKey.UpArrow:
                        {
                            if (innerPointer > 0) innerPointer--;
                            //Menu();
                            break;
                        }
                    case ConsoleKey.LeftArrow:
                        if (directoryInfo.Parent != null)
                        {
                            fileNavigator(directoryInfo.Parent.FullName);
                            stopper = true;
                            Console.SetWindowSize(40, 12);
                            Console.Clear();
                        }

                        break;
                    case ConsoleKey.RightArrow:
                        if (directories.Length > 0 && innerPointer >= 0 && innerPointer < directories.Length)
                        {

                            string selectedPath = Path.Combine(directoryInfo.FullName, directories[innerPointer].FullName);
                            if (Directory.Exists(selectedPath))
                            {
                                try
                                {
                                    fileNavigator(directories[innerPointer].FullName);
                                }
                                catch (UnauthorizedAccessException)
                                {
                                    Console.WriteLine("");
                                }
                                stopper = true;
                                Console.SetWindowSize(40, 12);
                                Console.Clear();
                            }
                        }
                        else
                        {
                            if (isIntegrity == false)
                            {
                                string fileForHash;
                                fileForHash = Path.Combine(directoryInfo.FullName, files[innerPointer - directories.Length].Name);
                                Console.SetWindowSize(40, 12);
                                hashFile(fileForHash);
                            }
                            else
                            {
                                string fileForHash;
                                fileForHash = Path.Combine(directoryInfo.FullName, files[innerPointer - directories.Length].Name);
                                Console.SetWindowSize(40, 12);
                                hashFileIntegrityLogic(fileForHash);
                            }
                        }

                        break;
                    case ConsoleKey.Backspace or ConsoleKey.Escape:
                        stopper = true;
                        Console.SetWindowSize(40, 12);
                        Console.Clear();
                        break;
                }
            }
        }

        public void ShowLab2()
        {
            bool stopper = false;
            ConsoleKeyInfo input;

            //навігатор:

            string currentDirectory = Directory.GetCurrentDirectory();            


            do
            {
                stopper = false;
                justVisual.ShowUp();

                Menu();

                justVisual.ShowDown(2);

                input = Console.ReadKey();


                switch (input.Key)
                {
                    case ConsoleKey.DownArrow:
                        {
                            if (pointer < 2) pointer++;
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
                        //MenuChecker();
                        if (pointer == 0) hashText();
                        if (pointer == 1) fileNavigator(currentDirectory);
                        if (pointer == 2) hashFileIntegrity(currentDirectory);
                        break;
                }

                if (stopper == true) break;
            } while (true);
        }
    }
}