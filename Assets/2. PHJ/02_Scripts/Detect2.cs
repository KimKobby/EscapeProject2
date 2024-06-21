using UnityEngine;
using System.Collections;

public class Detect2 : MonoBehaviour
{
    [SerializeField]
    private GameObject door;
    public bool isLocked = false;
    public GameObject soundManager; // SoundManagerのオブジェクト
    public float doorCloseDuration = 2.0f; // ドアが閉まるのにかかる時間（秒）

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isLocked)
            {
                isLocked = true;
                Debug.Log("DoorLock");

                // サウンド再生とドアの閉まる動作を開始
                StartCoroutine(CloseDoorAndPlaySound());
            }
        }
    }

    private IEnumerator CloseDoorAndPlaySound()
    {
        // サウンドを再生
        int[] soundIndices = { 0, 1 }; // 再生したいサウンドクリップのインデックス
        soundManager.GetComponent<SoundManager>().PlayMultipleSounds(soundIndices);

        // ドアを回転させる
        Quaternion initialRotation = door.transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0f, -90f, 0f); // 目標の回転

        float elapsed = 0f;

        while (elapsed < doorCloseDuration)
        {
            door.transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsed / doorCloseDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // 最後に目標の回転を正確に設定
        door.transform.rotation = targetRotation;
    }
}
