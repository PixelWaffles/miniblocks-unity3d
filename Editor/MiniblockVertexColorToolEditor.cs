using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace ginsederp.miniblocks
{
  [CustomEditor( typeof(MiniblockVertexColorTool) )]
  public class MiniblockVertexColorToolEditor : Editor
  {
    protected SerializedObject so;
    protected MiniblockVertexColorTool colorTool;

    protected ReorderableList reorderableList;

    protected void ReorderableDrawElement( Rect _rect, int _index, bool _active, bool _focused )
    {
      SerializedProperty element = reorderableList.serializedProperty.GetArrayElementAtIndex( _index );
      _rect.y += 2;

      EditorGUI.LabelField(
        new Rect( _rect.x, _rect.y, EditorGUIUtility.fieldWidth, EditorGUIUtility.singleLineHeight ),
        _index.ToString()
      );

      EditorGUI.PropertyField(
        new Rect( _rect.x + EditorGUIUtility.fieldWidth, _rect.y, _rect.width - EditorGUIUtility.fieldWidth, EditorGUIUtility.singleLineHeight ),
        element,
        GUIContent.none
      );
      return;
    }

    protected void ReorderableDrawHeader( Rect _rect )
    {
      EditorGUI.LabelField(
        new Rect( _rect.x, _rect.y, EditorGUIUtility.fieldWidth, EditorGUIUtility.singleLineHeight ),
       "Id" );

      EditorGUI.LabelField(
        new Rect( _rect.x + EditorGUIUtility.fieldWidth, _rect.y, _rect.width - EditorGUIUtility.fieldWidth, EditorGUIUtility.singleLineHeight ),
       "Colors" );
      return;
    }

    protected void SetupReorderableList()
    {
      reorderableList = new ReorderableList( so, so.FindProperty("idColors"), true, true, true, true );
      reorderableList.drawElementCallback = ReorderableDrawElement;
      reorderableList.drawHeaderCallback = ReorderableDrawHeader;
      return;
    }

    void OnEnable()
    {
      so = new SerializedObject(target);
      colorTool = (MiniblockVertexColorTool)target;

      SetupReorderableList();
      return;
    }

    public override void OnInspectorGUI()
    {
      so.Update();

      reorderableList.DoLayoutList();

      if( so.ApplyModifiedProperties() ) {
        EditorHelper.GenerateMiniblocks( colorTool.gameObject );
      }

      return;
    }

  }
}
