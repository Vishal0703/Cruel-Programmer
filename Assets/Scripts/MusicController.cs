using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{
    public static FMOD.Studio.EventInstance instance;

    [FMODUnity.EventRef]
    public string fmodEvent;



    void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        instance.start();
        instance.setParameterByName("Level", GameManager.gm.topLevel);

    }

    // Update is called once per frame
    void Update()
    {
        //instance.setParameterByName("Level", GameManager.gm.topLevel);
    }

}
