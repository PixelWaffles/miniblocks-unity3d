using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace ginsederp.miniblocks
{
  [CustomEditor( typeof(MiniblockRectangleTool) )]
  public class MiniblockRectangleToolEditor : Editor
  {
    protected SerializedObject so;
    protected MiniblockRectangleTool blockRectTool;

    protected ReorderableList reorderableList;

    protected void ReorderableDrawElement( Rect _rect, int _index, bool _active, bool _focused )
    {
      SerializedProperty element = reorderableList.serializedProperty.GetArrayElementAtIndex( _index );
      _rect.y += 2;

      EditorGUI.PropertyField(
        new Rect( _rect.x, _rect.y, EditorGUIUtility.fieldWidth, EditorGUIUtility.singleLineHeight ),
        element.FindPropertyRelative( "name" ),
        GUIContent.none
      );

      string[] propertyNames = { "toolType", "toolBlockId", "toolStart.x", "toolStart.y", "toolStart.z", "toolSize.x", "toolSize.y", "toolSize.z" };
      float rectBeforePropertyShelve = _rect.x + EditorGUIUtility.fieldWidth;
      float rectPropertyShelveWidth = _rect.width - EditorGUIUtility.fieldWidth;
      float rectPropertyWidth = rectPropertyShelveWidth / propertyNames.Length;

      for( int i = 0; i < propertyNames.Length; i++ ) {
        EditorGUI.PropertyField(
          new Rect( rectBeforePropertyShelve + rectPropertyWidth * i, _rect.y, rectPropertyWidth, EditorGUIUtility.singleLineHeight ),
          element.FindPropertyRelative( propertyNames[i] ),
          GUIContent.none
        );
      }
      return;
    }

    protected void ReorderableDrawHeader( Rect _rect )
    {
      float rectIndent = 10.0f;
      string[] propertyNames = { "Type", "BlockId", "Start X", "Y", "Z", "Size X", "Y", "Z" };
      float rectBeforePropertyShelve = _rect.x + rectIndent + EditorGUIUtility.fieldWidth;
      float rectPropertyShelveWidth = _rect.width - EditorGUIUtility.fieldWidth - rectIndent;
      float rectPropertyWidth = rectPropertyShelveWidth / propertyNames.Length;

      EditorGUI.LabelField(
        new Rect( _rect.x + rectIndent, _rect.y, EditorGUIUtility.fieldWidth, EditorGUIUtility.singleLineHeight ),
       "Name" );

      for( int i = 0; i < propertyNames.Length; i++ ) {
        EditorGUI.LabelField(
          new Rect( rectBeforePropertyShelve + rectPropertyWidth * i, _rect.y, rectPropertyWidth, EditorGUIUtility.singleLineHeight ),
          propertyNames[i]
        );
      }
      return;
    }

    protected void ReorderableReorder( ReorderableList _list )
    {
      UpdateMiniblocks();
      return;
    }

    protected void UpdateMiniblocks()
    {
      EditorHelper.GenerateMiniblocks( blockRectTool.gameObject );
      return;
    }

    protected void SetupReorderableList()
    {
      reorderableList = new ReorderableList( so, so.FindProperty("rectTools"), true, true, true, true );
      reorderableList.drawElementCallback = ReorderableDrawElement;
      reorderableList.drawHeaderCallback = ReorderableDrawHeader;
      reorderableList.onReorderCallback = ReorderableReorder;
      return;
    }

    void OnEnable()
    {
      so = new SerializedObject(target);
      blockRectTool = (MiniblockRectangleTool)target;

      SetupReorderableList();
      return;
    }

    public override void OnInspectorGUI()
    {
      so.Update();

      reorderableList.DoLayoutList();

      if( so.ApplyModifiedProperties() ) {
        UpdateMiniblocks();
      }

      return;
    }

  }
}
