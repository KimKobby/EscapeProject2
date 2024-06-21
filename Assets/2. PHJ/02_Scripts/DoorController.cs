using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float closeSpeed = 1.0f; // ドアが閉まる速度

    private AudioSource audioSource; // AudioSourceコンポーネントへの参照
    public AudioClip closeSound; // ドアが閉まるときのサウンド

    void Start()
    {

        // AudioSourceコンポーネントを取得
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    public void SetCloseSpeed(float speed)
    {
        // ドアの閉じる速度を変更する
        closeSpeed = speed;
    }

    private void PlayCloseSound()
    {
        if (closeSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(closeSound);
        }
    }
}


