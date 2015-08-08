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

      if( !blockRectTool.ToolArrayLengthsIsEqual() ) {
        EditorGUILayout.HelpBox("Array lengths of tool array variables are not equal.", MessageType.Error);
      }

      EditorGUILayout.PropertyField( so.FindProperty("toolType"), true );
      EditorGUILayout.PropertyField( so.FindProperty("toolBlockId"), true );
      EditorGUILayout.PropertyField( so.FindProperty("toolStart"), true );
      EditorGUILayout.PropertyField( so.FindProperty("toolSize"), true );

      if( so.ApplyModifiedProperties() ) {
        if( blockRectTool.ToolArrayLengthsIsEqual() ) {
          EditorHelper.GenerateMiniblocks( blockRectTool.gameObject );
        }
      }

      return;
    }

  }
}
