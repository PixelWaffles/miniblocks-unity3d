using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ginsederp.miniblocks
{
  [RequireComponent( typeof(Miniblocks) )]
  public class MiniblockVertexColorTool : MonoBehaviour
  {
    readonly Color32 DEFAULT_COLOR = Color.gray;

    [SerializeField] protected Color32[] idColors;

    public Color32[] BuildFaceColors( int _blockId, int _numberOfFaces )
    {
      Color32[] colors = new Color32[4 * _numberOfFaces];

      for( int n = 0; n < _numberOfFaces; n++ ) {
        for( int i = 0; i < colors.Length; i++ ) {
          if( _blockId >= idColors.Length ) {
            colors[i] = DEFAULT_COLOR;
          } else {
            colors[i] = idColors[_blockId];
          }
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
