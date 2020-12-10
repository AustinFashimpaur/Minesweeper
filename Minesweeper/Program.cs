using System;
using System.IO;
using System.Threading;

namespace Minesweeper
{
    /// <summary>
    /// Authors: Trevor Colton, Austin Fashimpaur
    /// Minesweeper game for CSIS 2410
    /// </summary>
    class Program
    {
        private static int Width;
        private static int Height;
        static void Main(string[] args)
        {
            //Console.SetWindowSize(104, 24);
            Console.CursorVisible = false;
            try
            {
                // Open the text file using a stream reader.
                using (var sr = new StreamReader("BoardSize.txt"))
                {
                    // Read the stream as a string, and write the string to the console.
                    //Console.WriteLine($"Reading From {Path.GetFullPath("BoardSize.txt")}");
                    while (sr.Peek() >= 0)
                    {
                        Width = Convert.ToInt32(sr.ReadLine()); //width
                        Height = Convert.ToInt32(sr.ReadLine()); //height
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                throw e;
            }

            Board.MenuScreen();

            

            int difficulty = int.Parse(Console.ReadLine());

            while (difficulty > 3 || difficulty < 1)
            {
                Console.WriteLine("Please enter a number between 1(easy) and 3(hard):");
                difficulty = int.Parse(Console.ReadLine());
            }
            Console.Clear();

            

            do
            {
                new Game(Width, Height, difficulty);
            }
            while (!Game.Exit);

            Console.Clear();
            Console.WriteLine("Thanks for playing!");
            if(Game.ResultFile != null)
            {
                Console.WriteLine($"You last game results were written to:");
                Console.WriteLine(Game.ResultFile);
            }   
            Thread.Sleep(5000);
            Console.Clear();

        }
    }
}