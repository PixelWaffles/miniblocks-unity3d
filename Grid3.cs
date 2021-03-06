﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ginsederp.miniblocks
{
  public class Grid3<T>
  {
    public T[,,] grid = new T[ 1, 1, 1 ];

    public bool CellIsFilled( Int3 _cellPosition )
    {
      return !CellIsEmpty( _cellPosition.x, _cellPosition.y, _cellPosition.z );
    }

    public bool CellIsFilled( int _cellPosX, int _cellPosY, int _cellPosZ )
    {
      return !CellIsEmpty( _cellPosX, _cellPosY, _cellPosZ );
    }

    public bool CellIsEmpty( Int3 _cellPosition )
    {
      return CellIsEmpty( _cellPosition.x, _cellPosition.y, _cellPosition.z );
    }

    public bool CellIsEmpty( int _cellPosX, int _cellPosY, int _cellPosZ )
    {
      if( CellIsOutOfRange( _cellPosX, _cellPosY, _cellPosZ ) ) {
        return true;
      }
      T cell = grid[ _cellPosX, _cellPosY, _cellPosZ ];
      return cell == null || cell.Equals( default(T) ) || typeof(T) == typeof(string) && string.IsNullOrEmpty( cell as string );
    }

    public bool CellIsOutOfRange( int _cellPosX, int _cellPosY, int _cellPosZ )
    {
      return _cellPosX < 0 || _cellPosY < 0 || _cellPosZ < 0 || _cellPosX >= grid.GetLength( 0 ) || _cellPosY >= grid.GetLength( 1 ) || _cellPosZ >= grid.GetLength( 2 );
    }

    public void WriteCells( T _value, Int3 _begin, Int3 _size, bool _writeOverFilledCells, bool _writeOverEmptyCells )
    {
      AllocateGrid( _begin, _size );

      for( int xi = _begin.x; xi < _begin.x + _size.x; xi++ ) {
        for( int yi = _begin.y; yi < _begin.y + _size.y; yi++ ) {
          for( int zi = _begin.z; zi < _begin.z + _size.z; zi++ ) {
            if( _writeOverFilledCells && CellIsFilled( xi, yi, zi ) || _writeOverEmptyCells && CellIsEmpty( xi, yi, zi ) ) {
              grid[ xi, yi, zi ] = _value;
            }
          }
        }
      }

      return;
    }

    public void AddCells( T _value, Int3 _begin, Int3 _size )
    {
      WriteCells( _value, _begin, _size, false, true );
      return;
    }

    public void RemoveCells( Int3 _begin, Int3 _size )
    {
      WriteCells( default(T), _begin, _size, true, false );
      return;
    }

    public void WriteoverCells( T _value, Int3 _begin, Int3 _size )
    {
      WriteCells( _value, _begin, _size, true, true );
      return;
    }

    public void IntersectCells( T _value, Int3 _begin, Int3 _size )
    {
      WriteCells( _value, _begin, _size, true, false );
      return;
    }

    public void AllocateGrid( Int3 _begin, Int3 _size )
    {
      Int3 makeAreaAvailable;
      makeAreaAvailable.x = _begin.x + _size.x;
      makeAreaAvailable.y = _begin.y + _size.y;
      makeAreaAvailable.z = _begin.z + _size.z;

      Int3 allocateGrid;
      allocateGrid.x = ( makeAreaAvailable.x > grid.GetLength(0) ) ? makeAreaAvailable.x : grid.GetLength(0);
      allocateGrid.y = ( makeAreaAvailable.y > grid.GetLength(1) ) ? makeAreaAvailable.y : grid.GetLength(1);
      allocateGrid.z = ( makeAreaAvailable.z > grid.GetLength(2) ) ? makeAreaAvailable.z : grid.GetLength(2);

      if( grid.GetLength(0) != allocateGrid.x || grid.GetLength(1) != allocateGrid.y || grid.GetLength(2) != allocateGrid.z ) {
        ResizeGrid(allocateGrid);
      }

      return;
    }

    private void ResizeGrid( Int3 _newSize )
    {
      T[,,] newGrid = new T[ _newSize.x, _newSize.y, _newSize.z ];

      // Copy over data from previous grid.
      for( int xi = 0; xi < grid.GetLength(0); xi++ ) {
        for( int yi = 0; yi < grid.GetLength(1); yi++ ) {
          for( int zi = 0; zi < grid.GetLength(2); zi++ ) {
            newGrid[ xi, yi, zi ] = grid[ xi, yi, zi ];
          }
        }
      }

      grid = newGrid;

      return;
    }
  }
}
