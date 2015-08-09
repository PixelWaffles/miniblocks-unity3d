using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ginsederp.miniblocks
{
  [RequireComponent( typeof(Miniblocks) )]
  public class MiniblockRectangleTool : MonoBehaviour
  {
    [System.Serializable]
    protected struct RectTool
    {
      public enum eToolType {
        add,
        remove,
        writeover,
        intersect,
      }

      public eToolType toolType;
      public int toolBlockId;
      public Int3 toolStart;
      public Int3 toolSize;
    }

    [SerializeField] protected RectTool[] rectTools;

    public Grid3<int> CreateGrid()
    {
      Grid3<int> grid = new Grid3<int>();

      foreach( RectTool rectTool in rectTools ) {
        switch( rectTool.toolType ) {
          case RectTool.eToolType.add:
            grid.AddCells( rectTool.toolBlockId, rectTool.toolStart, rectTool.toolSize );
            break;
          case RectTool.eToolType.remove:
            grid.RemoveCells( rectTool.toolStart, rectTool.toolSize );
            break;
          case RectTool.eToolType.writeover:
            grid.WriteoverCells( rectTool.toolBlockId, rectTool.toolStart, rectTool.toolSize );
            break;
          case RectTool.eToolType.intersect:
            grid.IntersectCells( rectTool.toolBlockId, rectTool.toolStart, rectTool.toolSize );
            break;
        }
      }

      return grid;
    }

    void Start()
    {
      return;
    }
  }
}
