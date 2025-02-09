using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public class Affichages
    {
        public static string Banner()
        {
            return (@"
            /$$$$$$$$                                         /$$                             /$$  
            | $$_____/                                        |__/                           /$$$$  
            | $$       /$$   /$$  /$$$$$$   /$$$$$$   /$$$$$$$ /$$  /$$$$$$$  /$$$$$$       |_  $$  
            | $$$$$   |  $$ /$$/ /$$__  $$ /$$__  $$ /$$_____/| $$ /$$_____/ /$$__  $$        | $$  
            | $$__/    \  $$$$/ | $$$$$$$$| $$  \__/| $$      | $$| $$      | $$$$$$$$        | $$  
            | $$        >$$  $$ | $$_____/| $$      | $$      | $$| $$      | $$_____/        | $$  
            | $$$$$$$$ /$$/\  $$|  $$$$$$$| $$      |  $$$$$$$| $$|  $$$$$$$|  $$$$$$$       /$$$$$$
            |________/|__/  \__/ \_______/|__/       \_______/|__/ \_______/ \_______/      |______/
            ");
        }

        public static int MenuSelect(string prompt, string[] options)
        {
            ConsoleKey keyPressed;
            int res = 0;
            do
            {
                Console.Clear();
                DisplayOptions(prompt, options, res);
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keyPressed = keyInfo.Key;
                if (keyPressed == ConsoleKey.UpArrow)
                {
                    res--;
                    if (res == -1)
                    {
                        res = options.Length - 1;
                    }
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    res++;
                    if (res == options.Length)
                    {
                        res = 0;
                    }
                }
            } while (keyPressed != ConsoleKey.Enter);
            return res;
        }

        public static void DisplayOptions(string prompt, string[] options, int selectedOption)
        {
            Console.WriteLine(prompt);
            for (int i = 0; i < options.Length; i++)
            {
                if (i == selectedOption)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.WriteLine($">> {options[i]}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine($"   {options[i]}");
                }
            }
            Console.ResetColor();
        }
    }
}
