using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{
    private FMOD.Studio.EventInstance instance;
    public bool isGMAvalable = true;
    public int topLevel = 0;
    [FMODUnity.EventRef]
    public string fmodEvent;



    void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        instance.start();
        //if(isGMAvalable)
        //    instance.setParameterByName("Level", GameManager.gm.topLevel);
        //else
        //    instance.setParameterByName("Level", topLevel);

    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            //Debug.Log("WKAKAKAKA");
            instance.setParameterByName("Level", 0f);

        }
        //instance.setParameterByName("Level", GameManager.gm.topLevel);
        //if (isGMAvalable)
        //    instance.setParameterByName("Level", GameManager.gm.topLevel);
        //else
        //    instance.setParameterByName("Level", topLevel);

    }

}
