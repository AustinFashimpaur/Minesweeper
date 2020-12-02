using System;
namespace Minesweeper
{
    /// <summary>
    /// Represents a cell on the minesweeper board.
    /// </summary>
    public class Cell
    {
        public bool Bomb { set; get; }
        public bool Revealed { get; set; }
        public bool Flagged { get; set; }
        public int AdjacentBombs { get; set; }
        public int XPosition { get; }
        public int YPosition { get; }

        public Cell(int x, int y, bool bomb)
        {
            XPosition = x;
            YPosition = y;
            Bomb = bomb;

            AdjacentBombs = 0;
            Revealed = false;
            Flagged = false;
        }
    }
}
