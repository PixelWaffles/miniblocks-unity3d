using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ginsederp.miniblocks
{
  public class MiniblockRectangleTool : MonoBehaviour
  {
    protected enum eToolType {
      add,
      remove,
      writeover,
      intersect,
    }

    [SerializeField] protected eToolType[] toolType;
    [SerializeField] protected int[] toolBlockId;
    [SerializeField] protected Int3[] toolStart;
    [SerializeField] protected Int3[] toolSize;

    public Grid3<int> CreateGrid()
    {
      AssertEqualToolArrayLengths();

      Grid3<int> grid = new Grid3<int>();

      for( int i = 0; i < toolType.Length; i++ ) {
        switch( toolType[i] ) {
          case eToolType.add:
            grid.Add( toolBlockId[i], toolStart[i], toolSize[i] );
            break;
          case eToolType.remove:
            grid.Remove( toolStart[i], toolSize[i] );
            break;
          case eToolType.writeover:
            grid.Writeover( toolBlockId[i], toolStart[i], toolSize[i] );
            break;
          case eToolType.intersect:
            grid.Intersect( toolBlockId[i], toolStart[i], toolSize[i] );
            break;
        }
      }

      return grid;
    }

    protected void AssertEqualToolArrayLengths()
    {
      if( toolBlockId.Length != toolType.Length || toolStart.Length != toolType.Length || toolSize.Length != toolType.Length ) {
        throw new Exception("Array length of tool array variables are not equal.");
      }

      return;
    }

    void Start()
    {
      AssertEqualToolArrayLengths();
      return;
    }
  }
}
