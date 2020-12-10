using System;
namespace Minesweeper
{
    /// <summary>
    /// Prints and adjusts the text of the board for minesweeper
    /// as well as the menu, rules, and engame tables.
    /// </summary>
    public class Board
    {

        int Rows { get; }
        int Columns { get; }

        public Board(int columns, int rows)
        {
            Rows = rows;
            Columns = columns;

            RulesScreen();
            DrawBoard();
            FeedbackBox();
        }

        public static void MenuScreen()
        {
            Console.WriteLine("Welcome to Minesweeper.\n");
            Console.WriteLine("To proceed select a difficulty (1-3) then press enter:");
            Console.WriteLine("1(Easy) 2(Normal) 3(Hard)");
        }

        /// <summary>
        /// Prints small table next to board with the rules and win/lose conditions
        /// </summary>
        public void RulesScreen() {
            Console.SetCursorPosition(Columns * 2 + 4, 0);
            Console.Write("Welcome to a console based version of Minsweeper");
            Console.SetCursorPosition(Columns * 2 + 4, 1);
            Console.Write("************************************************");

            Console.SetCursorPosition(Columns * 2 + 4, 2);
            Console.Write("Begin by pressing the down or right arrow.");

            Console.SetCursorPosition(Columns * 2 + 4, 4);
            Console.Write("Controls:");
            Console.SetCursorPosition(Columns * 2 + 4, 5);
            Console.Write("-To flag/unflag a cell, press space.");
            Console.SetCursorPosition(Columns * 2 + 4, 6);
            Console.Write("-To reveal a cell, press enter.");
            Console.SetCursorPosition(Columns * 2 + 4, 7);
            Console.Write("-To exit the game, press escape.");

            Console.SetCursorPosition(Columns * 2 + 4, 9);
            Console.Write("How to win:");
            Console.SetCursorPosition(Columns * 2 + 4, 10);
            Console.Write("Reveal all cells without hitting any bombs!");

            Console.SetCursorPosition(Columns * 2 + 4, 12);
            Console.Write("Good Luck!");
        }

        public void FeedbackBox()
        {
            Console.SetCursorPosition(Columns * 2 + 4, Rows - 2);
            Console.Write("+--------------------------------+");
            Console.SetCursorPosition(Columns * 2 + 4, Rows - 1);
            Console.Write("|                                |");
            Console.SetCursorPosition(Columns * 2 + 4, Rows);
            Console.Write("|                                |");
            Console.SetCursorPosition(Columns * 2 + 4, Rows + 1);
            Console.Write("+--------------------------------+");
        }

        /// <summary>
        /// Draws a board based off of the Rows and Columns, complete with a border.
        /// </summary>
        public void DrawBoard() {
            Console.SetCursorPosition(0, 0);
            Console.Write("+");
            for (int i = 0; i < (Columns * 2 + 1); i++)
                Console.Write("-");
            Console.Write("+");

            Console.SetCursorPosition(0, 1);
            for (int i = 0; i < Rows; i++) {
                Console.Write("| ");
                for (int j = 0; j < Columns; j++) {
                    Console.Write("# ");
                }
                Console.WriteLine("|");
            }

            Console.Write("+");
            for (int i = 0; i < (Columns * 2 + 1); i++)
                Console.Write("-");
            Console.Write("+");
        }

        /// <summary>
        /// Updates the current cell on row and column with the specified character
        /// </summary>
        /// <param name="x">Column</param>
        /// <param name="y">Row</param>
        /// <param name="c">Character</param>
        public static void UpdateCell(int x, int y, char c)
        {
            Console.SetCursorPosition(x * 2 + 2, y + 1);
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
            Console.SetCursorPosition(x * 2 + 2, y + 1);
            if (c.AdjacentBombs == 0 && !c.Bomb)
            {
                Console.Write(" ");
            }
            else if (c.Bomb)
            {
                ConsoleColor original = Console.ForegroundColor;

                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write("*");

                Console.ForegroundColor = original;
            }
            else
            {
                ConsoleColor original = Console.ForegroundColor;
                switch (c.AdjacentBombs)
                {
                    case 1:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write($"{c.AdjacentBombs}");
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($"{c.AdjacentBombs}");
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write($"{c.AdjacentBombs}");
                        break;
                    case 4:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($"{c.AdjacentBombs}");
                        break;
                    case 5:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write($"{c.AdjacentBombs}");
                        break;
                    case 6:
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write($"{c.AdjacentBombs}");
                        break;
                    case 7:
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.Write($"{c.AdjacentBombs}");
                        break;
                    case 8:
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write($"{c.AdjacentBombs}");
                        break;
                    default:
                        break;
                }
                Console.ForegroundColor = original;
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