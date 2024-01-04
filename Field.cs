using System;
using System.Runtime.InteropServices;

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
        _figure.Recreate();
        return CanSpawn();
      }
      if (_figure.GetMinY() == _matrix.GetLength(0) - 1)
      {
        _figure.Recreate();
        return true;
      }
      for (int i = 0; i < _figure.Coords.GetLength(0); i++)
      {
        if (_figure.Coords[i, 0] >= 0)
        {
          if (_matrix[_figure.Coords[i, 0] + 1, _figure.Coords[i, 1]])
          {
            _figure.Recreate();
            return CanSpawn();
          }
          _matrix[_figure.Coords[i, 0], _figure.Coords[i, 1]] = false;
        }
        _figure.Coords[i, 0]++;
        if (_figure.Coords[i, 0] >= 0)
          _matrix[_figure.Coords[i, 0], _figure.Coords[i, 1]] = true;
      }
      return true;
    }
    public void AddFigure()
    {
      for (int i = 0; i < _figure.Coords.GetLength(0); i++)
      {
        if (_figure.Coords[i, 0] >= 0)
          _matrix[_figure.Coords[i, 0], _figure.Coords[i, 1]] = true;
      }
    }

    public bool CanSpawn()
    {
      List<int[]> downParts = _figure.GetDownParts();
      for (int i = 0; i < downParts.Count; i++)
        if (_matrix[0, downX[i]])
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
      }
    }
    private bool CanMoveRight()
    {
      List<int[]> rightParts = _figure.GetRightParts();
      foreach (int[] i in rightParts)
      {
        if (i[1] + 1 == _matrix.GetLength(1) || _matrix[i[0], i[1] + 1]) return false;
      }
      return true;
    }
    public void MoveFigureLeft()
    {
      List<int[]> downParts = _figure.GetDownParts();
    }
    
    public bool CanMoveDown()
    {
      
      return true;
    }

    public void ReverseFigure()
    {

    }
  }
}