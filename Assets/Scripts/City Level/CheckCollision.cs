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

    private void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        nextSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
        //GetComponent<Animator>().SetBool("isDead", false);
        PauseGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            string sceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }

        if (Input.anyKeyDown)
        {
            if (isVictory)
            {
                SceneManager.LoadScene(nextSceneToLoad);
            }
            else if (!isDead)
            {
                ResumeGame();
            }
            else
            {
                SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Die")
        {
            PauseGame();
            isDead = true;
            GetComponent<Animator>().SetBool("isDead", true);
            FMODUnity.RuntimeManager.PlayOneShot("event:/Hurt");
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
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
