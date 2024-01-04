using System;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;

namespace Tetris
{
  public readonly struct InitCoords
  {
    public static readonly int[,] Line = new int[4, 2]
      {
        {-1, 4},
        {-2, 4},
        {-3, 4},
        {-4, 4}
      };
    public static readonly int[,] Cube = new int[4, 2]
      {
        {-1, 5},
        {-1, 4},
        {-2, 5},
        {-2, 4}
      };
    public static readonly int[,] L = new int[4, 2]
    {
      {-1, 5},
      {-1, 4},
      {-2, 4},
      {-3, 4}
    };
    public static readonly int[,] ReversedL = new int[4, 2]
    {
      {-1, 5},
      {-1, 4},
      {-2, 5},
      {-3, 5}
    };
    public static readonly int[,] Four = new int[4, 2]
    {
      {-1, 4},
      {-1, 3},
      {-2, 5},
      {-2, 4}
    };
    public static readonly int[,] ReversedFour = new int[4, 2]
    {
      {-1, 5},
      {-1, 4},
      {-2, 4},
      {-2, 3}
    };
    public static readonly int[,] T = new int[4, 2]
    {
      {-1, 4},
      {-2, 5},
      {-2, 4},
      {-2, 3}
    };
  }
  class Figure
  {
    public int[,] Coords = new int[4, 2];

    private static readonly Random rnd = new();
    public Figure()
    {
      Recreate();
    }

    public void Recreate()
    {
      int figure = rnd.Next(7);
      int[,] initFigure;
      switch (figure)
      {
        case 0:
          initFigure = InitCoords.Line;
          break;
        case 1:
          initFigure = InitCoords.Cube;
          break;
        case 2:
          initFigure = InitCoords.L;
          break;
        case 3:
          initFigure = InitCoords.ReversedL;
          break;
        case 4:
          initFigure = InitCoords.Four;
          break;
        case 5:
          initFigure = InitCoords.ReversedFour;
          break;
        case 6:
          initFigure = InitCoords.T;
          break;
        default:
          initFigure = InitCoords.Line;
          break;
      }
      Array.Copy(initFigure, Coords, 8);
    }

    public int GetMinY()
    {
      return Coords[0, 0];
    }

    public List<int[]> GetDownParts()
    {
      List<int[]> downParts = new();

      for (int i = 0; i < Coords.GetLength(0); i++)
      {
        if (Coords[i, 0] < -1) continue;
        int index = downParts.FindIndex(x => x[1] == Coords[i, 1]);
        if (index == -1) downParts.Add(new int[2] { Coords[i, 0], Coords[i, 1] });
        else if (downParts[index][0] < Coords[i, 0]) downParts[index] = new int[2] { Coords[i, 0], Coords[i, 1] };
      }
      return downParts;
    }

    public List<int[]> GetRightParts()
    {
      List<int[]> rightParts = new();
      int currentY = Coords[0, 0];
      if (currentY < 0) return rightParts;
      rightParts.Add(new int[2] { Coords[0, 0], Coords[0, 1] });
      for (int i = 1; i < Coords.GetLength(0); i++)
      {
        if (Coords[i, 0] < 0) break;
        if (currentY != Coords[i, 0])
        {
          rightParts.Add(new int[2] { Coords[i, 0], Coords[i, 1] });
          currentY = Coords[i, 0];
        }
      }
      return rightParts;
    }

    public List<int[]> GetLeftParts()
    {
      List<int[]> leftParts = new();
      int currentY = Coords[0, 0];
      if (currentY < 0) currentY = 0;
      for (int i = Coords.GetLength(0) - 1; i >= 0; i--)
      {
        if (Coords[i, 0] < 0) continue;
        
        if (leftParts.Count == 0 || currentY != Coords[i, 0])
        {
          leftParts.Add(new int[2] { Coords[i, 0], Coords[i, 1] });
          currentY = Coords[i, 0];
        }
      }
      return leftParts;
    }
  }
}
