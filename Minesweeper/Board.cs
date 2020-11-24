using System;
namespace Minesweeper
{
    public class Board
    {

        int Rows { get; }
        int Columns { get; }

        public Board(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            drawBoard();
        }

        public void drawBoard() {
            Console.WriteLine();
            Console.Write(" ");
            for (int i = 0; i < Rows; i++) {
                for (int j = 0; j < Columns; j++) {
                    Console.Write("# ");
                }
                Console.WriteLine("\n");
                Console.Write(" ");
            }
        }
    }
}
