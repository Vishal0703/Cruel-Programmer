using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float speed;
    Transform startPos;
    [SerializeField] float pos1 = 14;
    [SerializeField] float pos2 = -10;

    Vector3 nextPos;

    // Start is called before the first frame update
    void Start()
    {
        nextPos = transform.position;
        nextPos.y = pos1;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y == pos1)
        {
            nextPos.y = pos2;
        }
        if (transform.position.y == pos2)
        {
            nextPos.y = pos1;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed*Time.deltaTime);
    }
}
