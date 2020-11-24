using System;
using System.Threading;

namespace Minesweeper
{
    class Program
    {
        static void Main(string[] args)
        {
            new Board(10, 10);
            Console.ReadKey(true);
        }
    }
}
