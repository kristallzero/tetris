using System;
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

    private int _figureType;
    private int _reverses;

    private static readonly Random rnd = new();
    public Figure()
    {
      Recreate();
    }

    public void Recreate()
    {
      _figureType = rnd.Next(7);
      _reverses = 0;
      int[,] initFigure = _figureType switch
      {
        0 => InitCoords.Line,
        1 => InitCoords.Cube,
        2 => InitCoords.L,
        3 => InitCoords.ReversedL,
        4 => InitCoords.Four,
        5 => InitCoords.ReversedFour,
        6 => InitCoords.T,
        _ => InitCoords.Line,
      };
      Array.Copy(initFigure, Coords, 8);
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

    public int[,] GetReversedCoords()
    {
      int[,] newCoords;
      int y, x;
      switch (_figureType)
      {
        case 0:
          if (_reverses == 1)
          {
            y = Coords[2, 0] - 1;
            x = Coords[2, 1];
            newCoords = new int[4, 2] { { y + 3, x }, { y + 2, x }, { y + 1, x }, { y, x } };
            _reverses = 0;
          }
          else
          {
            y = Coords[2, 0];
            x = Coords[2, 1] - 1;
            newCoords = new int[4, 2] { { y, x + 3 }, { y, x + 2 }, { y, x + 1 }, { y, x } };
            _reverses = 1;
          }
          break;
        case 1:
          newCoords = Coords;
          break;
        case 2:
          switch (_reverses)
          {
            case 0:
              y = Coords[2, 0];
              x = Coords[2, 1] - 1;
              newCoords = new int[4, 2] { { y + 1, x }, { y, x + 2 }, { y, x + 1 }, { y, x } };
              break;
            case 1:
              y = Coords[2, 0] - 1;
              x = Coords[2, 1];
              newCoords = new int[4, 2] { { y + 2, x }, { y + 1, x }, { y, x }, { y, x - 1 } };
              break;
            case 2:
              y = Coords[1, 0] - 1;
              x = Coords[1, 1] - 1;
              newCoords = new int[4, 2] { { y + 1, x + 2 }, { y + 1, x + 1 }, { y + 1, x }, { y, x + 2 } };
              break;
            case 3:
              y = Coords[1, 0] - 1;
              x = Coords[1, 1];
              newCoords = new int[4, 2] { { y + 2, x + 1 }, { y + 2, x }, { y + 1, x }, { y, x } };
              break;
            default:
              y = Coords[2, 0];
              x = Coords[2, 1] - 1;
              newCoords = new int[4, 2] { { y + 1, x }, { y, x + 2 }, { y, x + 1 }, { y, x } };
              break;
          }
          if (_reverses == 3) _reverses = 0;
          else _reverses++;
          break;
        case 3:
          switch (_reverses)
          {
            case 0:
              y = Coords[2, 0] - 1;
              x = Coords[2, 1] - 1;
              newCoords = new int[4, 2] { { y + 1, x + 2 }, { y + 1, x + 1 }, { y + 1, x }, { y, x } };
              break;
            case 1:
              y = Coords[1, 0] - 1;
              x = Coords[1, 1] - 1;
              newCoords = new int[4, 2] { { y + 2, x }, { y + 1, x }, { y, x + 1 }, { y, x } };
              break;
            case 2:
              y = Coords[1, 0];
              x = Coords[1, 1] - 1;
              newCoords = new int[4, 2] { { y + 1, x + 2 }, { y, x + 2 }, { y, x + 1 }, { y, x } };
              break;
            case 3:
              y = Coords[1, 0] - 1;
              x = Coords[1, 1] - 1;
              newCoords = new int[4, 2] { { y + 2, x }, { y + 2, x + 1 }, { y + 1, x + 1 }, { y, x + 1 } };
              break;
            default:
              y = Coords[2, 0] - 1;
              x = Coords[2, 1] - 1;
              newCoords = new int[4, 2] { { y + 1, x + 2 }, { y + 1, x + 1 }, { y + 1, x }, { y, x } };
              break;
          }
          if (_reverses == 3) _reverses = 0;
          else _reverses++;
          break;
        case 4:
          if (_reverses == 0)
          {
            y = Coords[2, 0] - 1;
            x = Coords[2, 1];
            newCoords = new int[4, 2] { { y + 2, x + 1 }, { y + 1, x + 1 }, { y + 1, x }, { y, x } };
            _reverses = 1;
          }
          else
          {
            y = Coords[2, 0];
            x = Coords[2, 1] - 2;
            newCoords = new int[4, 2] { { y + 1, x + 1 }, { y + 1, x }, { y, x + 2 }, { y, x + 1 } };
            _reverses = 0;
          }
          break;
        case 5:
          if (_reverses == 0)
          {
            y = Coords[3, 0] - 1;
            x = Coords[3, 1];
            newCoords = new int[4, 2] { { y + 2, x }, { y + 1, x + 1 }, { y + 1, x }, { y, x + 1 } };
            _reverses = 1;
          }
          else
          {
            y = Coords[2, 0];
            x = Coords[2, 1];
            newCoords = new int[4, 2] { { y + 1, x + 2 }, { y + 1, x + 1 }, { y, x + 1 }, { y, x } };
            _reverses = 0;
          }
          break;
        case 6:
          switch (_reverses)
          {
            case 0:
              y = Coords[3, 0] - 1;
              x = Coords[3, 1];
              newCoords = new int[4, 2] { { y + 2, x + 1 }, { y + 1, x + 1 }, { y + 1, x }, { y, x + 1 } };
              break;
            case 1:
              y = Coords[3, 0];
              x = Coords[3, 1] - 1;
              newCoords = new int[4, 2] { { y + 1, x + 2 }, { y + 1, x + 1 }, { y + 1, x }, { y, x + 1 } };
              break;
            case 2:
              y = Coords[3, 0];
              x = Coords[3, 1] - 1;
              newCoords = new int[4, 2] { { y + 2, x + 1 }, { y + 1, x + 2 }, { y + 1, x + 1 }, { y, x + 1 } };
              break;
            case 3:
              y = Coords[3, 0];
              x = Coords[3, 1] - 1;
              newCoords = new int[4, 2] { { y + 2, x + 1 }, { y + 1, x + 2 }, { y + 1, x + 1 }, { y + 1, x } };
              break;
            default:
              y = Coords[3, 0] - 1;
              x = Coords[3, 1];
              newCoords = new int[4, 2] { { y + 2, x + 1 }, { y + 1, x + 1 }, { y + 1, x }, { y, x + 1 } };
              break;
          }
          if (_reverses == 3) _reverses = 0;
          else _reverses++;
          break;
        default:
          if (_reverses == 1)
          {
            y = Coords[2, 0];
            x = Coords[2, 1];
            newCoords = new int[4, 2] { { y + 3, x }, { y + 2, x }, { y + 1, x }, { y, x } };
            _reverses = 0;
          }
          else
          {
            y = Coords[2, 0];
            x = Coords[2, 1] - 1;
            newCoords = new int[4, 2] { { y, x + 3 }, { y, x + 2 }, { y, x + 1 }, { y, x } };
            _reverses = 1;
          }
          break;
      }
      return newCoords;
    }
  }
}
