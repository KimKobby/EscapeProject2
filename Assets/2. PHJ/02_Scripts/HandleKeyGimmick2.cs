using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class HandleKeyGimmick2 : MonoBehaviour
{
    public GameObject RootObject;
    public bool KeyCombineflag;
  

   
    void Start()
    {
        KeyCombineflag = false;
    }



    private void OnTriggerEnter(Collider other) // 문 큐브 코라이더에 다른 오브젝트의 코라이더가 충돌하면 이하의 처리를 행한다.
    {
        if (other.gameObject.name == "HandleKey") // otherの中でKeyという名前を持ったGameobjectがコライダーに衝突した場合、
        {
            //Debug.Log("keyyyyyy");

            this.gameObject.transform.parent = RootObject.gameObject.transform; // Key가 자식!!!!!
            KeyCombineflag = true;
            Debug.Log("keyyyyyy");

            // Keyのローカル座標にHandleKeyの右端を代入する
            //transformを取得

            Transform myTransform = this.transform;

            // ローカル座標での座標を取得
            Vector3 localPos = myTransform.localPosition;
            localPos.x = 0f;    // ローカル座標を基準にした、x座標を1に変更
            localPos.y = 0f;    // ローカル座標を基準にした、y座標を1に変更
            localPos.z = 0f;    // ローカル座標を基準にした、z座標を1に変更
            myTransform.localPosition = localPos; // ローカル座標での座標を設定
        }
    }
}

