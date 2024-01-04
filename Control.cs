using System;

namespace Tetris
{
  class Control
  {
    public static void KeyController()
    {
      while (true)
      {
        ConsoleKey key = Console.ReadKey().Key;
        switch (key)
        {
          case ConsoleKey.RightArrow:
            Game.field.MoveFigureRight();
            break;
          case ConsoleKey.LeftArrow:
            Game.field.MoveFigureLeft();
            break;
          case ConsoleKey.R:
            Game.field.ReverseFigure();
            break;
        }
      }
    }
  }
}