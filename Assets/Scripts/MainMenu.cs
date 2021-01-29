using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject canvas_toenable;
    public GameObject canvas_todisable;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            canvas_toenable.SetActive(true);
            canvas_todisable.SetActive(false);
        }
            //SceneManager.LoadScene("LevelSelector");
    }
}
