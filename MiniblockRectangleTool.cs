using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ginsederp.miniblocks
{
  [RequireComponent( typeof(Miniblocks) )]
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
            grid.AddCells( toolBlockId[i], toolStart[i], toolSize[i] );
            break;
          case eToolType.remove:
            grid.RemoveCells( toolStart[i], toolSize[i] );
            break;
          case eToolType.writeover:
            grid.WriteoverCells( toolBlockId[i], toolStart[i], toolSize[i] );
            break;
          case eToolType.intersect:
            grid.IntersectCells( toolBlockId[i], toolStart[i], toolSize[i] );
            break;
        }
      }

      return grid;
    }

    public bool ToolArrayLengthsIsEqual()
    {
      return toolBlockId.Length == toolType.Length && toolStart.Length == toolType.Length && toolSize.Length == toolType.Length;
    }

    protected void AssertEqualToolArrayLengths()
    {
      if( !ToolArrayLengthsIsEqual() ) {
        throw new Exception("Array lengths of tool array variables are not equal.");
      }

      return;
    }

    void Start()
    {
      return;
    }
  }
}
