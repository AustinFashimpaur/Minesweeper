using System;
namespace Minesweeper
{
    /// <summary>
    /// Contains all logic for Minesweeper
    /// </summary>
    public class Game
    {
        private Cell[,] cells;
        private int arrayPosX = 0, arrayPosY = 0;
        private Random rand;
        public bool GameOver { set; get; } = false;
        public int BoardWidth { get; }
        public int BoardHeight { get; }
        public int Difficulty { get; }
        

        public Game(int boardWidth, int boardHeight, int difficulty)
        {
            new Board(boardWidth, boardHeight);

            BoardWidth = boardWidth;
            BoardHeight = boardHeight;
            Difficulty = difficulty;

            cells = SetUpCells(BoardWidth, BoardHeight, difficulty);
        }

        /// <summary>
        /// Creates a grid of cells and updates the important information for each Cell
        /// </summary>
        /// <param name="x">Column</param>
        /// <param name="y">Row</param>
        /// <param name="difficulty"></param>
        /// <returns>2D array of Cell</returns>
        public Cell[,] SetUpCells(int x, int y, int difficulty)
        {
            Cell[,] cells = new Cell[y, x];
            int totalBombsLeft = difficulty * 7;
            rand = new Random();

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
                cells[randY, randX] = new Cell(true);
            }

            //Generate non-bomb cells to fill the rest of the board && assign AdjacentBomb values
            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    if (cells[i, j] == null)
                    {
                        cells[i, j] = new Cell(false);
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
                    if (arrayPosY > 0)
                    {
                        Board.MoveFrame(arrayPosX * 2 + 1, arrayPosY * 2 + 1, true);
                        arrayPosY--;
                        Board.MoveFrame(arrayPosX * 2 + 1, arrayPosY * 2 + 1, false);
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (arrayPosY < BoardHeight - 1)
                    {
                        Board.MoveFrame(arrayPosX * 2 + 1, arrayPosY * 2 + 1, true);
                        arrayPosY++;
                        Board.MoveFrame(arrayPosX * 2 + 1, arrayPosY * 2 + 1, false);
                    }

                    break;
                case ConsoleKey.LeftArrow:
                    if (arrayPosX > 0)
                    {
                        Board.MoveFrame(arrayPosX * 2 + 1, arrayPosY * 2 + 1, true);
                        arrayPosX--;
                        Board.MoveFrame(arrayPosX * 2 + 1, arrayPosY * 2 + 1, false);
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (arrayPosX < BoardWidth - 1)
                    {
                        Board.MoveFrame(arrayPosX * 2 + 1, arrayPosY * 2 + 1, true);
                        arrayPosX++;
                        Board.MoveFrame(arrayPosX * 2 + 1, arrayPosY * 2 + 1, false);
                    }
                    break;
                case ConsoleKey.Spacebar:
                    if (!cells[arrayPosY, arrayPosX].Flagged && !cells[arrayPosY, arrayPosX].Revealed)
                    {
                        Board.UpdateCell(arrayPosX * 2 + 1, arrayPosY * 2 + 1, '@');
                        cells[arrayPosY, arrayPosX].Flagged = true;
                    }
                    else if(cells[arrayPosY, arrayPosX].Revealed)
                    {
                    }
                    else
                    {
                        Board.UpdateCell(arrayPosX * 2 + 1, arrayPosY * 2 + 1, '#');
                        cells[arrayPosY, arrayPosX].Flagged = false;
                    }
                    break;
                case ConsoleKey.Enter:
                    if (cells[arrayPosY, arrayPosX].Bomb && !cells[arrayPosY, arrayPosX].Flagged)
                    {
                        Board.RevealCell(arrayPosX * 2 + 1, arrayPosY * 2 + 1, cells[arrayPosY, arrayPosX]);
                        cells[arrayPosY, arrayPosX].Revealed = true;
                        //GameOver = true;
                    }
                    else if (!cells[arrayPosY, arrayPosX].Flagged && !cells[arrayPosY, arrayPosX].Revealed)
                    {
                        ChainReveal(arrayPosX, arrayPosY);
                        //Board.RevealCell(arrayPosX * 2 + 1, arrayPosY * 2 + 1, cells[arrayPosY, arrayPosX]);
                        //cells[arrayPosY, arrayPosX].Revealed = true;
                    }
                    break;
                default:
                    break;
            }
        }

        public void ChainReveal(int x, int y)
        {
            Board.RevealCell(x * 2 + 1, y * 2 + 1, cells[y, x]);
            cells[y, x].Revealed = true;
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