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

      EditorGUILayout.PropertyField( so.FindProperty("rectTools"), true );

      if( so.ApplyModifiedProperties() ) {
        EditorHelper.GenerateMiniblocks( blockRectTool.gameObject );
      }

      return;
    }

  }
}
