using System;

namespace Tetris
{
  class Control
  {
    public static void KeyController()
    {
      while (!Game.quit)
      {
        ConsoleKey key = Console.ReadKey().Key;
        if (Game.pause)
        {
          if (key == ConsoleKey.Y)
          {
            Game.pause = false;
            Game.field = new(new Figure());
          }
          else
          {
            Game.quit = true;
          }
        }
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
          case ConsoleKey.Q:
            Game.SpeedUp();
            break;
        }
      }
    }
  }
}