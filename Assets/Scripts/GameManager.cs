using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager gm;

    public AudioClip levelRestartSound;
    public bool isJumpAvailable = true;
    public bool isleftAvailable = true;
    public bool isLightAvailable = true;
    public bool isCircularLevel = false;
    //To check for center
    public Transform center;

    public Light light;

    AudioSource audio;
    private void Awake()
    {
        if(gm==null)
        {
            gm = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.PlayOneShot(levelRestartSound);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (light)
        {
            if (!GameManager.gm.isLightAvailable)
            {
                light.intensity = 0f;
            }
            else
            {
                light.intensity = 1f;
            }
        }
    }
}
