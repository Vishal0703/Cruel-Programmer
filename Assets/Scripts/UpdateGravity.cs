using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateGravity : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.gm.center != null)
        {
            Vector2 dir = transform.position - GameManager.gm.center.position;
            dir = dir.normalized * 9.81f;
            //Debug.Log($"dir is {dir}");
            Physics2D.gravity = dir;
        }
        if(GameManager.gm.isGravityReversed)
        {
            Physics2D.gravity *= -1f;
        }
    }
}
