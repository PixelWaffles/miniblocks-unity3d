using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ginsederp.miniblocks
{
  public class Miniblocks : MonoBehaviour
  {
    public Grid3<int> blockIdMap = new Grid3<int>();
    [NonSerialized] public MeshRenderer meshRenderer;
    [NonSerialized] public MeshFilter meshFilter;

    protected void InitComponents()
    {
      meshRenderer = gameObject.GetComponent<MeshRenderer>();
      meshFilter = gameObject.GetComponent<MeshFilter>();

      meshRenderer = ( meshRenderer != null ) ? meshRenderer : gameObject.AddComponent<MeshRenderer>();
      meshFilter = ( meshFilter != null ) ? meshFilter : gameObject.AddComponent<MeshFilter>();

      GenerateMesh();

      return;
    }

    protected void GenerateMesh()
    {
      Mesh mesh = new Mesh();

      List<Vector3> verts = new List<Vector3>();
      List<int> tris = new List<int>();

      // Test Generate Face
      tris.AddRange( BuildFaceTris( verts.Count, false ) );
      verts.AddRange( BuildFaceVerts( Vector3.zero, Vector3.up, Vector3.right ) );

      mesh.vertices = verts.ToArray();
      mesh.triangles = tris.ToArray();

      mesh.RecalculateBounds();
      mesh.RecalculateNormals();

      meshFilter.mesh = mesh;

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

      return verts;
    }

    protected int[] BuildFaceTris( int _vertStartIndex, bool _isReversed )
    {
      int[] tris = new int[6];

      // >--v
      // | /|
      // |/ |
      // ^--<

      // First Triangle
      tris[0] = _vertStartIndex + 1;
      tris[1] = _vertStartIndex + 2;
      tris[2] = _vertStartIndex + 0;

      //Second Triangle
      tris[3] = _vertStartIndex + 3;
      tris[4] = _vertStartIndex + 0;
      tris[5] = _vertStartIndex + 2;

      return tris;
    }

    void Start()
    {
      InitComponents();

      blockIdMap.Add( 1, new Int3( 3, 4, 5 ), new Int3( 10, 20, 30 ) );
      blockIdMap.Add( 2, new Int3( 2, 3, 4 ), new Int3( 15, 22, 34 ) );
      blockIdMap.Remove( new Int3( 0, 0, 0 ), new Int3( 10, 10, 10 ) );
      return;
    }
  }
}
