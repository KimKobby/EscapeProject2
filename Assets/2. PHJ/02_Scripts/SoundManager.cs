using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] clips; // サウンドクリップの配列
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // 単一のサウンドクリップを再生
    public void OnPlayOneShot(int index)
    {
        if (index >= 0 && index < clips.Length)
        {
            audioSource.PlayOneShot(clips[index]);
        }
        else
        {
            Debug.LogWarning("サウンドインデックスが範囲外です: " + index);
        }
    }

    // 複数のサウンドクリップを同時に再生
    public void PlayMultipleSounds(int[] indices)
    {
        foreach (int index in indices)
        {
            if (index >= 0 && index < clips.Length)
            {
                audioSource.PlayOneShot(clips[index]);
            }
            else
            {
                Debug.LogWarning("サウンドインデックスが範囲外です: " + index);
            }
        }
    }


    public void Stop()
    {
       
        audioSource.enabled = false;
    }
}


