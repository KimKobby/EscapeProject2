using Song;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    [SerializeField]
    private GameObject lightObj;  // 라이트 오브젝트
    private bool isOn = false;  // 켜짐 꺼짐
    private float batteryTime = 100;
    public GameObject player;


    private void Awake()
    {
        lightObj = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;  // 플래시 라이트 자식 첫번째 오브젝트가 라이트오브젝트이어야함
    }

    private void Update()
    {
        
    }



    private void OnTriggerStay(Collider other)
    {
        Debug.Log("태그");
        if (other.gameObject.CompareTag("Hand"))
        {
            if (player.GetComponent<ControllerInputData>().getAButton())
            {
                Debug.Log("불");
                isOn = !isOn;
                lightObj.SetActive(true);
            }
            else
            {
                lightObj.SetActive(false);
            }
        }
    }

}
       
