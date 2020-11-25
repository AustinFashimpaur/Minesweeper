using System;
namespace Minesweeper
{
    public class Game
    {
        int cursorX = 1;
        int cursorY = 1;
        int boardWidth;
        int boardHeight;
        public Game(int boardWidth, int boardHeight)
        {
            new Board(boardWidth, boardHeight);
            this.boardWidth = boardWidth;
            this.boardHeight = boardHeight;
            //y*2-1
            while (!Done())
            {
                var ch = Console.ReadKey(false).Key;
                switch (ch)
                {
                    case ConsoleKey.UpArrow:
                        if(cursorY > 2)
                        {
                            Board.UpdateCell(cursorX, cursorY, '#', ConsoleColor.Black);
                            cursorY -= 2;
                            Board.UpdateCell(cursorX, cursorY, '#', ConsoleColor.Blue);
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        //Console.WriteLine("down arrow");
                        if (cursorY < (boardHeight*2-1))
                        {
                            Board.UpdateCell(cursorX, cursorY, '#', ConsoleColor.Black);
                            cursorY += 2;
                            Board.UpdateCell(cursorX, cursorY, '#', ConsoleColor.Blue);
                        }

                        break;
                    case ConsoleKey.LeftArrow:
                        if (cursorX > 2)
                        {
                            Board.UpdateCell(cursorX, cursorY, '#', ConsoleColor.Black);
                            cursorX -= 2;
                            Board.UpdateCell(cursorX, cursorY, '#', ConsoleColor.Blue);
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        //Console.WriteLine("right arrow");
                        if (cursorX < (boardWidth * 2 - 1))
                        {
                            Board.UpdateCell(cursorX, cursorY, '#', ConsoleColor.Black);
                            cursorX += 2;
                            Board.UpdateCell(cursorX, cursorY, '#', ConsoleColor.Blue);
                        }
                        break;
                    case ConsoleKey.Spacebar:
                        Board.UpdateCell(cursorX, cursorY, 'F', ConsoleColor.Black);
                        cursorX = 1;
                        cursorY = 1;
                        Console.SetCursorPosition(cursorX, cursorY);
                        break;
                }
            }
        }

        public Boolean Done()
        {
            return false; //TODO
        }
    }
}
