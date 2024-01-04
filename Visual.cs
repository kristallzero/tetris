using System;

namespace Tetris
{
  class Visual
  {
    public static void Print(Field field)
    {
      Console.Clear();
      for (int i = 0; i < field.Matrix.GetLength(0); i++)
      {
        for (int j = 0; j < field.Matrix.GetLength(1); j++)
        {
          Console.Write(field.Matrix[i, j] ? Game.Fill : Game.Void);
        }
        Console.WriteLine();
      }
    }
  }
}