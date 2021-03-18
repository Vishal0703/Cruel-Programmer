using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject canvas_toenable;
    public GameObject canvas_todisable;
    public Controls controls;
    private bool jump;

    private void Awake()
    {
        controls = new Controls();
        controls.UI.Space.performed += _ => Jump(true);
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Jump(bool isJumping)
    {
        jump = isJumping;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (jump)
        {
            jump = false;
            canvas_toenable.SetActive(true);
            canvas_todisable.SetActive(false);
        }
            //SceneManager.LoadScene("LevelSelector");
    }
}
