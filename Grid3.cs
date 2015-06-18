using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ginsederp.miniblocks
{
  public class Grid3<T>
  {
    public List<T> xGrid = new List<T>();
    public List<T> yGrid = new List<T>();
    public List<T> zGrid = new List<T>();

    public void Add( Int3 _begin, Int3 _size )
    {
      AllocateGrid( _begin, _size );

      Debug.Log( "x alloc: " + xGrid.Count );
      Debug.Log( "y alloc: " + yGrid.Count );
      Debug.Log( "z alloc: " + zGrid.Count );
    }

    public void AllocateGrid( Int3 _begin, Int3 _size )
    {
      Int3 allocateArea;
      allocateArea.x = _begin.x + _size.x;
      allocateArea.y = _begin.y + _size.y;
      allocateArea.z = _begin.z + _size.z;

      if( xGrid.Count <= allocateArea.x ) {
        xGrid.AddRange( new T[ allocateArea.x - ( xGrid.Count - 1 ) ] ); 
      }

      if( yGrid.Count <= allocateArea.y ) {
        yGrid.AddRange( new T[ allocateArea.y - ( yGrid.Count - 1 ) ] ); 
      }

      if( zGrid.Count <= allocateArea.z ) {
        zGrid.AddRange( new T[ allocateArea.z - ( zGrid.Count - 1 ) ] ); 
      }

      return;
    }
  }
}
