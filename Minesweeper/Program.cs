using System;
using System.Threading;

namespace Minesweeper
{
    /// <summary>
    /// Authors: Trevor Colton, Austin Fashimpaur
    /// Minesweeper game for CSIS 2410
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;

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
                new Game(13, 20, difficulty);
            }
            while (!Game.Exit);

            Console.Clear();
            Console.WriteLine("Thanks for playing!");
            Thread.Sleep(2000);
            Console.Clear();

        }
    }
}