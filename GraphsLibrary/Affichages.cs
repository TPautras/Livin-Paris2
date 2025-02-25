using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public class Affichages
    {
        /// <summary>
        /// génère une bannière ASCII pour l'affichage de l'exercice 1
        /// </summary>
        /// <returns>
        /// Une chaine de caractères contenant la bannière ASCII
        /// </returns>
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
        /// <summary>
        /// Affiche un menu déroulant interactif permettant à l'utilisateur de naviguer et de faire un choix d'option
        /// </summary>
        /// <param name="prompt"></param> le message affiché en haut du menu pour guider l'utilisateur
        /// <param name="options"></param> Un tableau de chaine de caractère représentant les différentes options disponnibles dans le menu
        /// <returns>
        /// L'index de l'option sélectionnéee par l'utilisateur par ses mouvements sur son pavé directionnel et l'utilisation de sa touche entrée
        /// </returns>
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
        /// <summary>
        /// Affiche le menu avec plusieurs options et met en avant l'option selectionnée 
        /// </summary>
        /// <param name="prompt"></param> le message affiché au-dessus du menu 
        /// <param name="options"></param>un tableau de caractère qui contient toutes les options de l'utilisateur
        /// <param name="selectedOption"></param>l'option selectionnée par l'utilisateur
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
