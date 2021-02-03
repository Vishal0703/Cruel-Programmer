using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckCollision : MonoBehaviour
{
    int nextSceneToLoad;
    string sceneName;
    [SerializeField] float jumpHeight = 100f;
    bool isDead = false;
    bool isVictory = false;
    [SerializeField] public bool isPause;
    [SerializeField] bool isManualPause = false;

    private void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        nextSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
        //GetComponent<Animator>().SetBool("isDead", false);
        PauseGame();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadScene("LevelLoader");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            string sceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }

        else if (Input.GetKeyDown(KeyCode.P))
        {
            if (isManualPause)
                ResumeGame();
            else
                ManualPauseGame();
        }
        else if (Input.anyKeyDown)
        {
            if (isPause)
            {
                ResumeGame();
            }
            if (isVictory)
            {
                SceneManager.LoadScene(nextSceneToLoad);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Die")
        {
            GetComponent<Animator>().SetBool("isDead", true);
            FMODUnity.RuntimeManager.PlayOneShot("event:/Hurt");
            StartCoroutine("Die");
            //isDead = true;
        }

        if (collision.gameObject.tag == "Win")
        {
            PauseGame();
            isVictory = true;
            GetComponent<Animator>().SetBool("isVictory", true);
            FMODUnity.RuntimeManager.PlayOneShot("event:/Goal");
        }

        if (collision.gameObject.tag == "Bounce")
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Jump");
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0;
        Debug.Log("Pause");
        isPause = true;
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
        isPause = false;
        isManualPause = false;
        Debug.Log("Resume");
    }

    IEnumerator Die()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    void ManualPauseGame()
    {
        Time.timeScale = 0;
        Debug.Log("Pause");
        isManualPause = true;
    }
}
