using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class MakePlatformInvisible : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("MakeInvisible");
    }

    IEnumerator MakeInvisible()
    {
        yield return new WaitForSecondsRealtime(2);
        GetComponent<TilemapRenderer>().enabled = false;
    }
}
