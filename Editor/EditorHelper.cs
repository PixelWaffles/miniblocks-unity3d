using UnityEngine;
using UnityEditor;

namespace ginsederp.miniblocks
{
  public static class EditorHelper
  {
    public static void GenerateMiniblocks( GameObject _gameobject )
    {
        Miniblocks miniblocks = _gameobject.GetComponent<Miniblocks>();
        MeshFilter meshFilter = _gameobject.GetComponent<MeshFilter>();

        Object[] objectsToUndo = { miniblocks, meshFilter };

        Undo.RecordObjects( objectsToUndo, "MeshGenerate" );

        miniblocks.GenerateMiniblocks();
        EditorUtility.SetDirty( miniblocks );
        EditorUtility.SetDirty( meshFilter );

        return;
    }
  }
}
