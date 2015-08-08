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
        EditorHelper.GenerateMiniblocks( colorTool.gameObject );
      }

      return;
    }

  }
}
