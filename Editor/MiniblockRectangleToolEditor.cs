using UnityEngine;
using UnityEditor;

namespace ginsederp.miniblocks
{
  [CustomEditor( typeof(MiniblockRectangleTool) )]
  public class MiniblockRectangleToolEditor : Editor
  {
    protected SerializedObject blockRectToolSo;
    protected MiniblockRectangleTool blockRectTool;

    void OnEnable()
    {
      blockRectToolSo = new SerializedObject(target);
      blockRectTool = (MiniblockRectangleTool)target;
      return;
    }

    public override void OnInspectorGUI()
    {
      blockRectToolSo.Update();
      //TODO GUI for consolidated tool array fields.
      blockRectToolSo.ApplyModifiedProperties();
      return;
    }

  }
}
