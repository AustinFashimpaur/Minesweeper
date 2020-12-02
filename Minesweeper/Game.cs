using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Minesweeper
{
    /// <summary>
    /// Contains all logic for Minesweeper
    /// </summary>
    public class Game
    {
        public static Cell[,] cells;
        private int posX = 0, posY = 0;
        private int totalBombs;
        public static readonly Random rand = new Random();
        private readonly IList<Cell> listCells;

        public bool GameOver { set; get; } = false;
        public static bool Exit { set; get; } = false;
        public static int Revealed { set; get; }
        public int BoardWidth { get; }
        public int BoardHeight { get; }
        public int Difficulty { get; }
        

        public Game(int boardWidth, int boardHeight, int difficulty)
        {
            new Board(boardWidth, boardHeight);

            BoardWidth = boardWidth;
            BoardHeight = boardHeight;
            Difficulty = difficulty;
            Revealed = 0;

            cells = SetUpCells(BoardWidth, BoardHeight, difficulty);

            listCells = new List<Cell>();
            foreach (Cell c in cells)
            {
                listCells.Add(c);
            }

            do
            {
                UserControls();
                CheckWin();
            }
            while (!GameOver && !Exit);
        }

        /// <summary>
        /// Creates a grid of cells and updates the important information for each Cell
        /// </summary>
        /// <param name="x">Column</param>
        /// <param name="y">Row</param>
        /// <param name="difficulty"></param>
        /// <returns>2D array of Cell</returns>
        private Cell[,] SetUpCells(int x, int y, int difficulty)
        {
            Cell[,] cells = new Cell[y, x];
            totalBombs = difficulty * 15;
            int totalBombsLeft = totalBombs;

            //Generate cells with bombs based on difficulty
            for (int b = 0; b < totalBombsLeft; b++)
            {
                int randX, randY;
                //do-while loop to check if cell is already created (no duplicate bomb locations)
                do
                {
                    randX = rand.Next(x);
                    randY = rand.Next(y);
                }
                while (cells[randY, randX] != null);
                cells[randY, randX] = new Cell(randX, randY, true);
            }

            //Generate non-bomb cells to fill the rest of the board && assign AdjacentBomb values
            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    if (cells[i, j] == null)
                    {
                        cells[i, j] = new Cell(j, i,false);
                    }
                }
            }

            //For-loop that iterates through active bomb cells and updates the AdjacentBomb Values for
            //their neighbors
            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    //if bomb is active, iterate through boundry exceptions
                    if (cells[i, j].Bomb == true)
                    {
                        int adjX, adjY;
                        for (int k = -1; k < 2; k++)
                        {
                            for (int g = -1; g < 2; g++)
                            {
                                adjX = j + k;
                                adjY = i + g;
                                if (((adjX != 0) || (adjY != 0)) && ValidSlots(adjX, adjY))
                                {
                                    cells[adjY, adjX].AdjacentBombs++;
                                }
                            }
                        }
                    }
                }
            }
            return cells;
        }

        /// <summary>
        /// Runs the controls for the user (arrows, enter, and space keys)
        /// </summary>
        public void UserControls() {
            var ch = Console.ReadKey(true).Key;
            switch (ch)
            {
                case ConsoleKey.UpArrow:
                    if (posY > 0)
                    {
                        Board.MoveFrame(posX * 2 + 2, posY + 1, true);
                        posY--;
                        Board.MoveFrame(posX * 2 + 2, posY + 1, false);
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (posY < BoardHeight - 1)
                    {
                        Board.MoveFrame(posX * 2 + 2, posY + 1, true);
                        posY++;
                        Board.MoveFrame(posX * 2 + 2, posY + 1, false);
                    }

                    break;
                case ConsoleKey.LeftArrow:
                    if (posX > 0)
                    {
                        Board.MoveFrame(posX * 2 + 2, posY + 1, true);
                        posX--;
                        Board.MoveFrame(posX * 2 + 2, posY + 1, false);
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (posX < BoardWidth - 1)
                    {
                        Board.MoveFrame(posX * 2 + 2, posY + 1, true);
                        posX++;
                        Board.MoveFrame(posX * 2 + 2, posY + 1, false);
                    }
                    break;
                case ConsoleKey.Spacebar:
                    if (!cells[posY, posX].Flagged && !cells[posY, posX].Revealed)
                    {
                        Board.UpdateCell(posX, posY, '@');
                        cells[posY, posX].Flagged = true;
                    }
                    else if(cells[posY, posX].Revealed)
                    {
                    }
                    else
                    {
                        Board.UpdateCell(posX, posY, '#');
                        cells[posY, posX].Flagged = false;
                    }
                    break;
                case ConsoleKey.Enter:
                    if (cells[posY, posX].Bomb && !cells[posY, posX].Flagged)
                    {
                        Board.RevealCell(posX, posY, cells[posY, posX]);
                        Console.Beep();

                        YouLose();
                        RevealBombs();

                        Thread.Sleep(1000);
                        GameOver = true;
                    }
                    else if (!cells[posY, posX].Flagged && !cells[posY, posX].Revealed)
                    {
                        ChainReveal(posX, posY);
                    }
                    break;
                case ConsoleKey.Escape:
                    Exit = true;
                    break;
                default:
                    break;
            }
        }

        private void ChainReveal(int x, int y)
        {
            Board.RevealCell(x, y, cells[y, x]);
            cells[y, x].Revealed = true;
            Revealed++;

            int chainX, chainY;
            if (cells[y, x].AdjacentBombs == 0)
            {
                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        chainX = x + i;
                        chainY = y + j;
                        if (ValidSlots(chainX, chainY) && (!cells[chainY, chainX].Revealed) && (!cells[chainY, chainX].Flagged))
                        {
                            ChainReveal(chainX, chainY);
                        }
                    }
                }
            }
        }

        private void RevealBombs()
        {
            var results =
                from c in listCells
                where c.Bomb == true
                select c;

            foreach (Cell r in results)
            {
                Thread.Sleep(300);
                Board.RevealCell(r.XPosition, r.YPosition, r);
            }

        }

        private void CheckWin()
        {
            if (Revealed == (BoardHeight * BoardWidth) - totalBombs)
            {
                ConsoleColor originalF = Console.ForegroundColor;
                ConsoleColor originalB = Console.BackgroundColor;

                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.Green;

                Console.SetCursorPosition(BoardWidth * 2 + 7, BoardHeight - 1);
                Console.Write("You win!!");

                Thread.Sleep(4000);
                GameOver = true;

                Console.ForegroundColor = originalF;
                Console.BackgroundColor = originalB;
            }
        }

        private void YouLose()
        {
            ConsoleColor originalF = Console.ForegroundColor;
            ConsoleColor originalB = Console.BackgroundColor;

            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Red;

            Console.SetCursorPosition(BoardWidth * 2 + 7, BoardHeight - 1);
            Console.Write("You Lose.");

            Console.ForegroundColor = originalF;
            Console.BackgroundColor = originalB;
        }

        private bool ValidSlots(int x, int y)
        {
            return ((x >= 0) && (y >= 0) && (x < BoardWidth) && (y < BoardHeight));
        }

        public Boolean Done()
        {
            return false; //TODO yes
        }
    }
}