using UnityEngine;

public class DoorLock : MonoBehaviour
{
    private bool isLocked = false;
    public GameObject door;

    // ドアのロック状態を管理するメソッド
    private void LockDoor()
    {
        isLocked = true;
        // ここでドアのロックアニメーションやロック状態を表現するコードを追加
        Debug.Log("ドアがロックされました");
    }

    private void OnTriggerEnter(Collider other)
    {
        // プレイヤーがトリガーに入った時
        if (other.CompareTag("Player"))
        {
            if (!isLocked)
            {
                Debug.Log("DoorLock");
                door.GetComponent<Transform>().rotation = Quaternion.Euler(0f, 3.286f, 0f); // アンロックの処理を追加したい場合はここにコードを追加
                LockDoor();
            }
        }
    }
}

