using System;

namespace Tetris
{
  class Game
  {
    public static readonly int MatrixX = 10;
    public static readonly int MatrixY = 20;
    public static readonly char Void = '□';
    public static readonly char Fill = '■';
    public static readonly int InitSleepTime = 500;
    public static readonly int SpeedUpSleepTime = 25;

    public static int SleepTime = InitSleepTime;
    public static bool quit = false;
    public static bool pause = false;

    public static Field field = new(new Figure());
    public static void Main()
    {
      Thread gameLoop = new(GameLoop);
      Thread keyController = new(Control.KeyController); 
      gameLoop.Start();
      keyController.Start();
    }

    public static void GameLoop()
    {
      while (!quit)
      {
        if (pause) continue;
        Visual.Print(field);
        Thread.Sleep(SleepTime);

        if (!field.Update())
        {
          pause = true;
          Console.Write("Вы проиграли! Начать сначало? (y): ");
        };
      }
    }
  }
}