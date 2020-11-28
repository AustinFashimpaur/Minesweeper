using System;
namespace Minesweeper
{
    /// <summary>
    /// Prints and adjusts the text of the board for minesweeper
    /// </summary>
    public class Board
    {

        int Rows { get; }
        int Columns { get; }

        public Board(int columns, int rows)
        {
            Rows = rows;
            Columns = columns;
            MenuScreen();
            DrawBoard();
        }

        /// <summary>
        /// Prints small menu with the rules and win/lose conditions
        /// </summary>
        public void MenuScreen() {
            Console.SetCursorPosition(Columns * 2 + 2, 0);
            Console.Write("Welcome to a console based version of Minsweeper");
            Console.SetCursorPosition(Columns * 2 + 2, 1);
            Console.Write("************************************************");
            //Console.SetCursorPosition(Columns * 2 + 3, 1);
        }

        /// <summary>
        /// Draws a board based off of the Rows and Columns
        /// </summary>
        public void DrawBoard() {
            Console.SetCursorPosition(0,1);
            Console.Write(" ");
            for (int i = 0; i < Rows; i++) {
                for (int j = 0; j < Columns; j++) {
                    Console.Write("# ");
                }
                Console.WriteLine("\n");
                Console.Write(" ");
            }
        }

        /// <summary>
        /// Updates the current cell on row and column with the specified character
        /// </summary>
        /// <param name="x">Column</param>
        /// <param name="y">Row</param>
        /// <param name="c">Character</param>
        public static void UpdateCell(int x, int y, char c)
        {
            Console.SetCursorPosition(x, y);
            Console.Write($"{c}");
        }

        /// <summary>
        /// Reveals if the cell is a bomb or how many bombs its touching
        /// </summary>
        /// <param name="x">Column</param>
        /// <param name="y">Row</param>
        /// <param name="c">Cell</param>
        public static void RevealCell(int x, int y, Cell c)
        {
            Console.SetCursorPosition(x, y);
            if (c.AdjacentBombs == 0 && !c.Bomb)
            {
                Console.Write(" ");
            }
            else if (c.Bomb)
            {
                Console.Write("*");
            }
            else
            {
                Console.Write($"{c.AdjacentBombs}");
            }
        }

        /// <summary>
        /// Moves the user frame depending on arrow key pressed
        /// </summary>
        /// <param name="x">Column</param>
        /// <param name="y">Row</param>
        /// <param name="delete"></param>
        public static void MoveFrame(int x, int y, bool delete)
        {
            if (delete)
            {
                Console.SetCursorPosition(x - 1, y);
                Console.Write(" ");
                Console.SetCursorPosition(x + 1, y);
                Console.Write(" ");
            }
            else
            {
                Console.SetCursorPosition(x - 1, y);
                Console.Write("<");
                Console.SetCursorPosition(x + 1, y);
                Console.Write(">");
            }
        }
    }
}