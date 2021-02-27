using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portals : MonoBehaviour
{
    public GameObject targetPoint;
    // Start is called before the first frame update
    void Start()
    {
        if (targetPoint == null)
            Debug.LogError("Portal destination not assigned");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Portal");
            targetPoint.GetComponent<BoxCollider2D>().enabled = false;
            collision.gameObject.transform.position = targetPoint.transform.position;
            StartCoroutine(ReEnableCollider());
        }

    }

    IEnumerator ReEnableCollider()
    {
        yield return new WaitForSeconds(0.5f);
        targetPoint.GetComponent<BoxCollider2D>().enabled = true;
    }
}
