using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicObject : MonoBehaviour
{
    public static GameObject MusicPlayer;

    //this script simply makes sure that there is only one musicplayer gameobject in the scene; music parameters are changed in the other script! 
    void Awake()
    {
        //When the scene loads it checks if there is an object called "MUSIC".
        MusicPlayer = GameObject.Find("MUSIC");
        if (MusicPlayer == null)
        {
            //If this object does not exist then it does the following:
            //1. Sets the object this script is attached to as the music player
            MusicPlayer = this.gameObject;
            //2. Renames THIS object to "MUSIC" for next time
            MusicPlayer.name = "MUSIC";
            //3. Tells THIS object not to die when changing scenes.
            DontDestroyOnLoad(MusicPlayer);
        }
        else
        {
            if (this.gameObject.name != "MUSIC")
            {
                //If there WAS an object in the scene called "MUSIC" (because we have come back to
                //the scene where the music was started) then it just tells this object to 
                //destroy itself if this is not the original
                Destroy(this.gameObject);
            }
        }




        
    }

  

}