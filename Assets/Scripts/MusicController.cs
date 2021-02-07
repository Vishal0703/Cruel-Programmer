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
        

    }

    //This part of the script changes parameters for the music. If you'd like to change the pitch of the music, use if (SceneManager.GetActiveScene().buildIndex = whatever level is the slow motion one) {instance.setParameterByName("Pitch", 0f);}
    //Additionally, you could use SceneManger.GetActiveScene().name to function very similarly. This script is only for controlling music! you can trigger the slowed down sound effects using oneshots in your player controller scripts. 
    //I named the slowed actions "Slow Footsteps", "Slow Jump", "Slow Hurt", "Slow Goal"
    
    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex >= 0 && SceneManager.GetActiveScene().buildIndex <= 1) //this scene range is for both the menu and the level; the more upbeat music will start when you enter a level 
        {
            //Debug.Log("menu");
            instance.setParameterByName("Level", 0f);
        }
        else if (SceneManager.GetActiveScene().buildIndex >= 2 && SceneManager.GetActiveScene().buildIndex <=8) //copy and paste this for whatever your build indexes for different levels are..... I intended for both the main menu and the level loader to be set to 0, city levels to be set to 1, scifi levles set to 2, space levels set to 3
        {
            //Debug.Log("level 1");
            
            instance.setParameterByName("Level", 1f);
           
        }
        else if (SceneManager.GetActiveScene().buildIndex >= 9 && SceneManager.GetActiveScene().buildIndex <=13) 
        {
            //Debug.Log("level 1");

            instance.setParameterByName("Level", 2f);

        }
        else
        {
            instance.setParameterByName("Level", 3f);
        }



    }
        

    }


