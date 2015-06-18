using UnityEngine;
using System.Collections;

namespace ginsederp.miniblocks
{
  public class Miniblocks : MonoBehaviour
  {
    public Grid3<int> blockIdMap = new Grid3<int>();

    void Start()
    {
      blockIdMap.Add( new Int3( 0, 0, 0 ), new Int3( 9, 19, 29 ) );
      return;
    }
  }
}
