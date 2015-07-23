using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ginsederp.miniblocks
{
  public class MiniblockVertexColorTool : MonoBehaviour
  {
    [SerializeField] protected Color32[] idColors;

    public Color32[] BuildFaceColors( int _blockId, int _numberOfFaces )
    {
      if( _blockId >= idColors.Length ) {
        throw new Exception("Block id larger than color id array length.");
      }

      Color32[] colors = new Color32[4 * _numberOfFaces];

      for( int n = 0; n < _numberOfFaces; n++ ) {
        for( int i = 0; i < colors.Length; i++ ) {
          colors[i] = idColors[_blockId];
        }
      }

      return colors;
    }
    void Start()
    {
      return;
    }
  }
}
