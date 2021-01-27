using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringForce : MonoBehaviour
{
    public float springImpulse = 10f;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        if (target == null)
            target = GameManager.gm.center;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 dir = target.position - transform.position;
            Rigidbody2D rgbd = collision.gameObject.GetComponent<Rigidbody2D>();
            rgbd.AddForce(dir.normalized * springImpulse, ForceMode2D.Impulse);
        }
    }
}
