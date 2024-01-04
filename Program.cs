using System;
using System.Collections.ObjectModel;

namespace Tetris
{
  class Game
  {
    public static readonly int MatrixX = 10;
    public static readonly int MatrixY = 20;
    public static readonly char Void = '□';
    public static readonly char Fill = '■';
    public static bool quit = false;

    public static Field field = new(new Figure());
    public static void Main()
    {
      Thread gameLoop = new(GameLoop);
      Thread keyController = new(Control.KeyController); 
      gameLoop.Start();
      keyController.Start();
      //GameLoop();
    }

    public static void GameLoop()
    {
      while (true)
      {
        Visual.Print(field);
        Thread.Sleep(500);
        if (!field.Update())
        {
          Console.Write("Вы проиграли! Начать сначало? (y): ");
          if (Console.ReadLine() != "y") quit = true;
          field = new(new Figure());
          field.AddFigure();
        };
      }
    }
  }
}