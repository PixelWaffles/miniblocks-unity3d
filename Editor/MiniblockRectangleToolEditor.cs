using UnityEngine;
using UnityEditor;

namespace ginsederp.miniblocks
{
  [CustomEditor( typeof(MiniblockRectangleTool) )]
  public class MiniblockRectangleToolEditor : Editor
  {
    protected SerializedObject so;
    protected MiniblockRectangleTool blockRectTool;

    void OnEnable()
    {
      so = new SerializedObject(target);
      blockRectTool = (MiniblockRectangleTool)target;
      return;
    }

    public override void OnInspectorGUI()
    {
      so.Update();

      EditorGUILayout.PropertyField( so.FindProperty("toolType"), true );
      EditorGUILayout.PropertyField( so.FindProperty("toolBlockId"), true );
      EditorGUILayout.PropertyField( so.FindProperty("toolStart"), true );
      EditorGUILayout.PropertyField( so.FindProperty("toolSize"), true );

      so.ApplyModifiedProperties();

      if( GUI.changed ) {
        if( blockRectTool.ToolArrayLengthsIsEqual() ) {
          blockRectTool.gameObject.GetComponent<Miniblocks>().GenerateMiniblocks();
        }
      }

      return;
    }

  }
}
