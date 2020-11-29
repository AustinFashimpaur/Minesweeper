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

        public Cell(bool bomb)
        {
            Bomb = bomb;
            AdjacentBombs = 0;
        }
    }
}
