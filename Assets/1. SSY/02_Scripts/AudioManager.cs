using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Inst;

    public AudioSource backgrounndAS;
    public AudioSource playerAS;
    public AudioSource uiAS;

    public AudioClip backgroundMusic;
    public AudioClip[] soundEffects;

    public GameObject player;
    int playerwalkcount;


    private void Awake()
    {
        if (Inst == null)
        {
            Inst = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        playerwalkcount = 2;
        PlayMusic(backgroundMusic);
    }

    public void PlayMusic(AudioClip clip)
    {
        backgrounndAS.clip = clip;
        backgrounndAS.Play();
    }

 

    public void SetMusicVolume(float _volume)
    {
        backgrounndAS.volume = _volume;
        uiAS.volume = _volume;
        playerAS.volume = _volume;
    }

    public void Mute(bool isMuted)
    {
        backgrounndAS.mute = isMuted;
        playerAS.mute = isMuted;
        uiAS.mute = isMuted;
        
    }


    public void UIBtnClickSound(bool _b)
    {
        if (_b)
            uiAS.clip = soundEffects[0];
        else
            uiAS.clip = soundEffects[1];

        Debug.Log(uiAS.volume);

        uiAS.Play();
    }

    public IEnumerator PlayerWalk(bool _iswalk, float _num)
    {
       while(_iswalk)
        {

            if(!_iswalk)
            {
                playerAS.Stop();
                yield break;
            }
       

            playerAS.clip = soundEffects[playerwalkcount];
            playerAS.Play();


            if (playerwalkcount == 5)
            {
                playerwalkcount = 2;
            }
            playerwalkcount++;
            yield return new WaitForSeconds(_num);
        }
          
    }

    public void PlayerIdle()
    {
        playerAS.Stop();
    }
    

}