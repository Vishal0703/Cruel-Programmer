using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakePlatformInvisible : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator TurnPlatformInvisible()
    {
        foreach (Transform child in transform)
        {
            
        }
        yield return new WaitForSeconds(2);
    }
}
