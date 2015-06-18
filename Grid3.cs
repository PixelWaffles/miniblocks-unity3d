using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ginsederp.miniblocks
{
  public class Grid3<T>
  {
    public T[,,] grid = new T[ 1, 1, 1 ];

    public void Add( T _value, Int3 _begin, Int3 _size )
    {
      AllocateGrid( _begin, _size );

      for( int xi = _begin.x; xi < _begin.x + _size.x; xi++ ) {
        for( int yi = _begin.y; yi < _begin.y + _size.y; yi++ ) {
          for( int zi = _begin.z; zi < _begin.z + _size.z; zi++ ) {
            grid[ xi, yi, zi ] = _value;
          }
        }
      }

      for( int xi = 0; xi < grid.GetLength(0); xi++ ) {
        for( int yi = 0; yi < grid.GetLength(1); yi++ ) {
          for( int zi = 0; zi < grid.GetLength(2); zi++ ) {
            Debug.Log( xi + ",\t" + yi + ",\t" + zi + ":\t" + grid[ xi, yi, zi ] );
          }
        }
      }

      Debug.Log( "x alloc: " + grid.GetLength(0) );
      Debug.Log( "y alloc: " + grid.GetLength(1) );
      Debug.Log( "z alloc: " + grid.GetLength(2) );

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
  