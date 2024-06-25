using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackGroundmp3 : MonoBehaviour
{
   public  AudioClip cp;

    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.SceneManagement.Scene currentScene = SceneManager.GetActiveScene();

       
        if(currentScene.name == "EscapeScene_B1")
        {
            Debug.Log(currentScene.name);

            AudioManager.Inst.PlayBackGroundMusic();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
