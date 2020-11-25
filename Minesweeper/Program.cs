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
            Game myGame = new Game(10, 10);
            
            Console.ReadKey(true);
        }
    }
}
