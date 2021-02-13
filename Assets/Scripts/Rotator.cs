using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotateSpeed = 5f;
    float currentSpeed;
    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = rotateSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f, 0f, currentSpeed * Time.deltaTime));
        currentSpeed += (Time.deltaTime*0.5f);
    }
}
