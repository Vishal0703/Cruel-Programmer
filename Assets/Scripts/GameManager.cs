using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager gm;

    public AudioClip levelRestartSound;
    public bool isJumpAvailable = true;
    public bool isLeftAvailable = true;
    public bool isRightAvailable = true;
    public bool isLightAvailable = true;
    public bool isCircularLevel = false;
    public bool isAirJumpAllowed = false;
    public bool isGravityReversed = false;
    public bool isMotionReversed = false;
    public bool isSlowMotion = false;
    [Range(0.0f, 1.0f)]
    public float slowMotionTimeScale = 1f;
    public bool isJumpCountRestricted = false;
    public int restrictedJumpCount = 3;
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
        if (GameManager.gm.isJumpCountRestricted && GameManager.gm.restrictedJumpCount <= 0)
            GameManager.gm.isJumpAvailable = false;


        if (GameManager.gm.isSlowMotion)
            DilateTime(GameManager.gm.slowMotionTimeScale);

    }

    public void DilateTime(float timescale)
    {
        Time.timeScale = timescale;
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
}
