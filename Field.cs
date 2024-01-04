using System;
using System.Diagnostics;
using System.Runtime.ExceptionServices;

namespace Tetris
{
  class Field
  {
    public bool[,] Matrix => _matrix;

    private readonly bool[,] _matrix = new bool[Game.MatrixY, Game.MatrixX];

    private readonly Figure _figure;

    public Field(Figure figure)
    {
      _figure = figure;
    }
    public bool Update()
    {
      if (!CanMoveDown())
      {
        Game.SleepTime = Game.InitSleepTime;
        _figure.Recreate();
        return CanSpawn();
      }
      for (int i = 0; i < _figure.Coords.GetLength(0); i++)
      {
        if (_figure.Coords[i, 0] >= 0)
          _matrix[_figure.Coords[i, 0], _figure.Coords[i, 1]] = false;
        _figure.Coords[i, 0]++;
        if (_figure.Coords[i, 0] >= 0)
          _matrix[_figure.Coords[i, 0], _figure.Coords[i, 1]] = true;
      }
      DeleteFilledLines();
      return true;
    }

    private void DeleteFilledLines()
    {
      for (int i = _matrix.GetLength(0) - 1; i >= 0; i--)
      {
        if (IsLineFilled(i))
        {
          for (int j = i; j > 0; j--)
            for (int k = 0; k < _matrix.GetLength(1); k++)
              _matrix[j, k] = _matrix[j - 1, k];
          for (int j = 0; j < _matrix.GetLength(1); j++)
            _matrix[0, j] = false;
        }
      }
    }

    private bool IsLineFilled(int y)
    {
      for (int x = 0; x < _matrix.GetLength(1); x++)
        if (!_matrix[y, x]) return false;
      return true;
    }
    private bool CanSpawn()
    {
      List<int[]> downParts = _figure.GetDownParts();
      for (int i = 0; i < downParts.Count; i++)
        if (_matrix[0, downParts[i][1]])
          return false;
      return true;
    }

    public void MoveFigureRight()
    {
      if (CanMoveRight())
      {
        for (int i = 0; i < _figure.Coords.GetLength(0); i++)
        {
          if (_figure.Coords[i, 0] >= 0)
            _matrix[_figure.Coords[i, 0], _figure.Coords[i, 1]] = false;
          _figure.Coords[i, 1]++;
          if (_figure.Coords[i, 0] >= 0)
            _matrix[_figure.Coords[i, 0], _figure.Coords[i, 1]] = true;
        }
        Visual.Print(this);
      }
    }
    private bool CanMoveRight()
    {
      List<int[]> rightParts = _figure.GetRightParts();
      foreach (int[] i in rightParts)
        if (i[1] + 1 == _matrix.GetLength(1) || _matrix[i[0], i[1] + 1])
          return false;

      return true;
    }
    public void MoveFigureLeft()
    {
      if (CanMoveLeft())
      {
        for (int i = _figure.Coords.GetLength(0) - 1; i >= 0; i--)
        {
          if (_figure.Coords[i, 0] >= 0)
            _matrix[_figure.Coords[i, 0], _figure.Coords[i, 1]] = false;
          _figure.Coords[i, 1]--;
          if (_figure.Coords[i, 0] >= 0)
            _matrix[_figure.Coords[i, 0], _figure.Coords[i, 1]] = true;
        }
        Visual.Print(this);
      }
    }

    private bool CanMoveLeft()
    {
      List<int[]> leftParts = _figure.GetLeftParts();
      foreach (int[] i in leftParts)
        if (i[1] == 0 || _matrix[i[0], i[1] - 1])
          return false;

      return true;
    }
    private bool CanMoveDown()
    {
      List<int[]> downParts = _figure.GetDownParts();
      foreach (int[] i in downParts)
      {
        if (i[0] + 1 == _matrix.GetLength(0) || _matrix[i[0] + 1, i[1]]) return false;
      }
      return true;
    }

    public void ReverseFigure()
    {
      for (int i = 0; i < _figure.Coords.GetLength(0); i++)
        if (_figure.Coords[i, 0] >= 0) _matrix[_figure.Coords[i, 0], _figure.Coords[i, 1]] = false;

      int[,] newCoords = _figure.GetReversedCoords();
      if (CanReverseFigure(newCoords)) _figure.Coords = newCoords;

      for (int i = 0; i < _figure.Coords.GetLength(0); i++)
        if (_figure.Coords[i, 0] >= 0) _matrix[_figure.Coords[i, 0], _figure.Coords[i, 1]] = true;

      Visual.Print(this);
    }

    private bool CanReverseFigure(int[,] coords)
    {
      for (int i = 0; i < coords.GetLength(0); i++)
      {
        bool OutOfRangeY = coords[i, 0] >= _matrix.GetLength(0);
        bool OutOfRangeX = coords[i, 1] < 0 || coords[i, 1] >= _matrix.GetLength(1);
        if (OutOfRangeY || OutOfRangeX) return false;
        if (coords[i, 0] < 0) continue;
        if (_matrix[coords[i, 0], coords[i, 1]]) return false;
      }
      return true;
    }
  }
}