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
      Console.WriteLine("Управление:");
      Console.WriteLine("Стрелка влево (←): передвижение фигуры налево");
      Console.WriteLine("Стрелка вправо (→): передвижение фигуры направо");
      Console.WriteLine("Стрелка вверх (↑): поворот фигуры");
      Console.WriteLine("Стрелка вниз (↓): ускорение падения фигуры");
      Console.WriteLine("Для начала игры нажмите любую кнопку");
      Console.ReadKey();
      
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