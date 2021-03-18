using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 1f;
    public Controls controls;

    private void Awake()
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = controls.Player.HorizontalMove.ReadValue<float>();
        transform.Rotate(Vector3.forward * Time.deltaTime * -horizontalInput * rotationSpeed);
    }
}
