using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ginsederp.miniblocks
{
  [RequireComponent( typeof(MeshFilter) )]
  public class Miniblocks : MonoBehaviour
  {
    public Vector3 voxelScale = Vector3.one;
    public Grid3<int> blockIdMap = new Grid3<int>();

    public void GenerateMiniblocks()
    {
      MiniblockRectangleTool miniblockRectangleTool = gameObject.GetComponent<MiniblockRectangleTool>();
      blockIdMap = ( miniblockRectangleTool != null ) ? miniblockRectangleTool.CreateGrid() : blockIdMap;

      GenerateMesh();
      return;
    }

    public void GenerateMesh()
    {
      Mesh mesh = new Mesh();

      List<Vector3> verts = new List<Vector3>();
      List<int> tris = new List<int>();
      List<Color32> colors = new List<Color32>();

      MiniblockVertexColorTool miniblockVertexColorTool = gameObject.GetComponent<MiniblockVertexColorTool>();
      bool hasVertexColorTool = ( miniblockVertexColorTool != null );

      for( int xi = 0; xi < blockIdMap.grid.GetLength( 0 ); xi++ ) {
        for( int yi = 0; yi < blockIdMap.grid.GetLength( 1 ); yi++ ) {
          for( int zi = 0; zi < blockIdMap.grid.GetLength( 2 ); zi++ ) {
            if( blockIdMap.grid[ xi, yi, zi ] == 0 ) {
              continue;
            }

            int facesDrawn = 0;

            // Generate Back Faces
            if( blockIdMap.CellIsEmpty( xi, yi, zi - 1 ) ) {
              tris.AddRange( BuildFaceTris( verts.Count, false ) );
              verts.AddRange( BuildFaceVerts( new Vector3( xi, yi, zi ), Vector3.up, Vector3.right ) );
              facesDrawn++;
            }

            // Generate Front Faces
            if( blockIdMap.CellIsEmpty( xi, yi, zi + 1 ) ) {
              tris.AddRange( BuildFaceTris( verts.Count, true ) );
              verts.AddRange( BuildFaceVerts( new Vector3( xi, yi, zi + 1 ), Vector3.up, Vector3.right ) );
              facesDrawn++;
            }

            // Generate Right Faces
            if( blockIdMap.CellIsEmpty( xi + 1, yi, zi ) ) {
              tris.AddRange( BuildFaceTris( verts.Count, false ) );
              verts.AddRange( BuildFaceVerts( new Vector3( xi + 1, yi, zi ), Vector3.up, Vector3.forward ) );
              facesDrawn++;
            }

            // Generate Left Faces
            if( blockIdMap.CellIsEmpty( xi - 1, yi, zi ) ) {
              tris.AddRange( BuildFaceTris( verts.Count, true ) );
              verts.AddRange( BuildFaceVerts( new Vector3( xi, yi, zi ), Vector3.up, Vector3.forward ) );
              facesDrawn++;
            }

            // Generate Top Faces
            if( blockIdMap.CellIsEmpty( xi, yi + 1, zi ) ) {
              tris.AddRange( BuildFaceTris( verts.Count, false ) );
              verts.AddRange( BuildFaceVerts( new Vector3( xi, yi + 1, zi ), Vector3.forward, Vector3.right ) );
              facesDrawn++;
            }

            // Generate Bottom Faces
            if( blockIdMap.CellIsEmpty( xi, yi - 1, zi ) ) {
              tris.AddRange( BuildFaceTris( verts.Count, true ) );
              verts.AddRange( BuildFaceVerts( new Vector3( xi, yi, zi ), Vector3.forward, Vector3.right ) );
              facesDrawn++;
            }

            // Color Face Vertices
            if( hasVertexColorTool && facesDrawn > 0 ) {
              colors.AddRange( miniblockVertexColorTool.BuildFaceColors( blockIdMap.grid[ xi, yi, zi ], facesDrawn ) );
            }
          }
        }
      }

      mesh.vertices = verts.ToArray();
      mesh.triangles = tris.ToArray();
      if( hasVertexColorTool ) {
        mesh.colors32 = colors.ToArray();
      }

      mesh.RecalculateBounds();
      mesh.RecalculateNormals();

      gameObject.GetComponent<MeshFilter>().mesh = mesh;

      return;
    }

    protected Vector3[] BuildFaceVerts( Vector3 _origin, Vector3 _up, Vector3 _right )
    {
      Vector3[] verts = new Vector3[4];

      // 1--2
      // | /|
      // |/ |
      // 0--3

      verts[0] = _origin;
      verts[1] = _origin + _up;
      verts[2] = _origin + _up + _right;
      verts[3] = _origin + _right;

      for( int i = 0; i < verts.Length; i++ ) {
        verts[i] = Vector3.Scale( verts[i], voxelScale );
      }

      return verts;
    }

    protected int[] BuildFaceTris( int _vertStartIndex, bool _isReversed )
    {
      int[] tris = new int[6];

      // >--v
      // | /|
      // |/ |
      // ^--<

      if( _isReversed ) {
        // First Triangle
        tris[ 0 ] = _vertStartIndex + 0;
        tris[ 1 ] = _vertStartIndex + 3;
        tris[ 2 ] = _vertStartIndex + 1;

        //Second Triangle
        tris[ 3 ] = _vertStartIndex + 2;
        tris[ 4 ] = _vertStartIndex + 1;
        tris[ 5 ] = _vertStartIndex + 3;
      } else {
        // First Triangle
        tris[ 0 ] = _vertStartIndex + 1;
        tris[ 1 ] = _vertStartIndex + 2;
        tris[ 2 ] = _vertStartIndex + 0;

        //Second Triangle
        tris[ 3 ] = _vertStartIndex + 3;
        tris[ 4 ] = _vertStartIndex + 0;
        tris[ 5 ] = _vertStartIndex + 2;
      }

      return tris;
    }

    void Start()
    {
      GenerateMiniblocks();
      return;
    }
  }
}
