using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

public class GameManager : MonoBehaviour
{

    public static GameManager gm;

    //public AudioClip levelRestartSound;
    public bool isJumpAvailable = true;
    public bool isLeftAvailable = true;
    public bool isRightAvailable = true;
    public bool isLightAvailable = true;
    public bool isCircularLevel = false;
    public bool isAirJumpAllowed = false;
    public bool isGravityReversed = false;
    public bool isMotionReversed = false;
    public bool isSlowMotion = false;
    public bool isControlGravity = false;
    [Range(0.0f, 1.0f)]
    public float slowMotionTimeScale = 1f;
    public bool isJumpCountRestricted = false;
    public int restrictedJumpCount = 3;
    public float topLevel = 0f;
    //To check for center
    public Transform center;

    public Light light;
    bool isPaused = false;

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
        DilateTime(1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene("MainMenu");
        }
            if (Input.GetKeyDown(KeyCode.P))
        {
            if (!isPaused)
                DilateTime(0f);
            else
                DilateTime(GameManager.gm.slowMotionTimeScale);

            isPaused = !isPaused;
        }
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
        if (GameManager.gm.isJumpCountRestricted && GameManager.gm.restrictedJumpCount <= 0)
            GameManager.gm.isJumpAvailable = false;


        if (GameManager.gm.isSlowMotion)
            DilateTime(GameManager.gm.slowMotionTimeScale);

    }

    public void DilateTime(float timescale)
    {
        Time.timeScale = timescale;
        audio.pitch = timescale;
        //MusicController.instance.setParameterByName("Pitch", timescale);
        //if (!resettimestarted)
        //{
        //    //Debug.Log("Entered dilate time");
        //    Time.timeScale = timescale;
        //    if (musicAudioSource)
        //        musicAudioSource.pitch = timescale;
        //    if (laserAudioSource)
        //        laserAudioSource.pitch = timescale;
        //    StartCoroutine(ResetTime());
        //}
    }

    public void LevelSelect(string name)
    {
        StartCoroutine(WaitforLoadingNextScene(name));
    }

    public void LevelSelect(int index, float timetoWait = 1f)
    {
        StartCoroutine(WaitforLoadingNextScene(index, timetoWait));
    }

    public void LevelSelect(string name, float timetoWait = 1f)
    {
        StartCoroutine(WaitforLoadingNextScene(name, timetoWait));
    }


    IEnumerator WaitforLoadingNextScene(int sceneIndex, float timetoWait = 1f)
    {
        yield return new WaitForSeconds(timetoWait);
        Debug.Log($"Scene number is {sceneIndex}");
        SceneManager.LoadScene(sceneIndex);
    }

    IEnumerator WaitforLoadingNextScene(string sceneName, float timetoWait = 1f)
    {
        yield return new WaitForSeconds(timetoWait);
        Debug.Log($"Scene name is {sceneName}");
        SceneManager.LoadScene(sceneName);
    }

    //public int GetPlayableLevels()
    //{
    //    return numPlayableLevels;
    //}
}
