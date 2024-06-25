using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class HandleKeyGimmick2 : MonoBehaviour
{
    public GameObject doorKey;
    public bool KeyCombineflag;
    public Mesh mesh;


   
    void Start()
    {
        KeyCombineflag = false;
    }


    

    private void OnCollisionEnter(Collision collision) // 문 큐브 코라이더에 다른 오브젝트의 코라이더가 충돌하면 이하의 처리를 행한다.
    {
        
        if (collision.gameObject.CompareTag("HandleKey")) // otherの中でKeyという名前を持ったGameobjectがコライダーに衝突した場合、
        {
            GameObject targetKey = collision.gameObject;
            targetKey.SetActive(false);
            Debug.Log(doorKey.GetComponent<MeshFilter>());
            this.gameObject.GetComponent<MeshFilter>().mesh = mesh;
            this.gameObject.name = "Key";
            //doorKey.transform.SetParent(this.transform);
            //doorKey.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

            KeyCombineflag = true;
            Debug.Log("keyyyyyy");

            // Keyのローカル座標にHandleKeyの右端を代入する
            //transformを取得
/*
            Transform myTransform = this.transform;

            // ローカル座標での座標を取得
            Vector3 localPos = myTransform.localPosition;
            localPos.x = 0f;    // ローカル座標を基準にした、x座標を1に変更
            localPos.y = 0f;    // ローカル座標を基準にした、y座標を1に変更
            localPos.z = 0f;    // ローカル座標を基準にした、z座標を1に変更
            myTransform.localPosition = localPos; // ローカル座標での座標を設定*/
        }
    }
}

