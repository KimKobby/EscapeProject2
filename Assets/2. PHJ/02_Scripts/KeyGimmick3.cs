using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyGimmick3 : MonoBehaviour
{
    public GameObject Key;
    public GameObject door;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
   
    private void OnTriggerExit(Collider other) // 문 큐브 코라이더에 다른 오브젝트의 코라이더가 충돌하면 이하의 처리를 행한다.
    {
        Debug.Log("hanno" + other.gameObject.name + Key.GetComponent<HandleKeyGimmick2>().KeyCombineflag);
        if (other.gameObject.name == "Key" && Key.GetComponent<HandleKeyGimmick2>().KeyCombineflag == true)
            //otherの中でKeyという名前を持ったGameobjectがコライダーに衝突した場合、
        {
            Debug.Log("open");
            door.GetComponent<Transform>().rotation = Quaternion.Euler(0f, -120f, 0f);
        }
        Debug.Log(other.gameObject.name);
    }
}
