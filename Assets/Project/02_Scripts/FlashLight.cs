using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    [SerializeField]
    private GameObject lightObj;  // 라이트 오브젝트
    private bool isOn = false;  // 켜짐 꺼짐
    private float batteryTime = 100;

    private void Awake() 
    {
        lightObj = this.gameObject.transform.GetChild(0).gameObject;  // 플래시 라이트 자식 첫번째 오브젝트가 라이트오브젝트이어야함
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (batteryTime > 0)
            {
                isOn = !isOn;
                lightObj.SetActive(isOn);
            }
            else
            {
                Debug.Log("배터리가 없습니다.");
            }
        }
       
        if (isOn)
        {
            batteryTime -= Time.deltaTime;
        }

        if (batteryTime < 0)
        {
            lightObj.SetActive(false);
        }
    }
}
