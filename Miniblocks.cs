using UnityEngine;
using System.Collections;

namespace ginsederp.miniblocks
{
  public class Miniblocks : MonoBehaviour
  {
    public Grid3<int> blockIdMap = new Grid3<int>();

    void Start()
    {
      blockIdMap.Add( 1, new Int3( 3, 4, 5 ), new Int3( 10, 20, 30 ) );
      return;
    }
  }
}
