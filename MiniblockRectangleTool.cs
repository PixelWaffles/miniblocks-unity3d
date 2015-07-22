using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ginsederp.miniblocks
{
  public class MiniblockRectangleTool : MonoBehaviour
  {
    protected enum eToolType {
      add,
      remove,
    }

    [SerializeField] protected eToolType[] toolType;
    [SerializeField] protected Int3[] toolStart;
    [SerializeField] protected Int3[] toolSize;

    void Start()
    {
      return;
    }
  }
}
