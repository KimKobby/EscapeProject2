using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    public GameObject door;
    private bool isLocked = false;

    // ドアのロック状態を管理するメソッド
    //private void LockDoor()
    //{
    //isLocked = true;
    // ここでドアのロックアニメーションやロック状態を表現するコードを追加
    //Debug.Log("ドアがロックされました");
    //}


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // door.GetComponent<Transform>().rotation = Quaternion.Euler(0f, 2.092f, 0f);
        //Y 114.449  -1.351
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isLocked)
            {
                Debug.Log("DoorLock");
                door.GetComponent<Transform>().rotation = Quaternion.Euler(0f, -87.188f, 0f);
                //Y 114.449  -1.351
            }


        }
    }
}
