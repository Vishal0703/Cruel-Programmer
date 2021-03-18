using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    [SerializeField] float speed = 50f;
    Controls controls;

    void Awake()
    {
        controls = new Controls();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        float xMovement = controls.Player.HorizontalMove.ReadValue<float>();
        float yMovement = controls.Player.VerticalMove.ReadValue<float>();
        Vector2 movement = new Vector2(xMovement * speed * Time.deltaTime, yMovement * speed * Time.deltaTime);
        transform.Translate(movement);
    }
}
