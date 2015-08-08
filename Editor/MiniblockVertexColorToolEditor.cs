using UnityEngine;
using UnityEditor;

namespace ginsederp.miniblocks
{
  [CustomEditor( typeof(MiniblockVertexColorTool) )]
  public class MiniblockVertexColorToolEditor : Editor
  {
    protected SerializedObject so;
    protected MiniblockVertexColorTool colorTool;

    void OnEnable()
    {
      so = new SerializedObject(target);
      colorTool = (MiniblockVertexColorTool)target;
      return;
    }

    public override void OnInspectorGUI()
    {
      so.Update();

      EditorGUILayout.PropertyField( so.FindProperty("idColors"), true );

      if( so.ApplyModifiedProperties() ) {
        Miniblocks miniblocks = colorTool.gameObject.GetComponent<Miniblocks>();
        MeshFilter meshFilter = colorTool.gameObject.GetComponent<MeshFilter>();

        Object[] objectsToUndo = { miniblocks, meshFilter };

        Undo.RecordObjects( objectsToUndo, "MeshGenerate" );

        miniblocks.GenerateMiniblocks();
        EditorUtility.SetDirty( miniblocks );
        EditorUtility.SetDirty( meshFilter );
      }

      return;
    }

  }
}
