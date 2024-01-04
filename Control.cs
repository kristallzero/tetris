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
            Game.SleepTime = Game.InitSleepTime;
            Game.field = new(new Figure());
          }
          else Game.quit = true;
        }
        switch (key)
        {
          case ConsoleKey.RightArrow:
            Game.field.MoveFigureRight();
            break;
          case ConsoleKey.LeftArrow:
            Game.field.MoveFigureLeft();
            break;
          case ConsoleKey.UpArrow:
            Game.field.ReverseFigure();
            break;
          case ConsoleKey.DownArrow:
            Game.SleepTime = Game.SpeedUpSleepTime;
            break;
        }
      }
    }
  }
}