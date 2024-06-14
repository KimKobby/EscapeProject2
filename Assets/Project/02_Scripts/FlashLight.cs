using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    [SerializeField]
    private GameObject lightObj;  // ����Ʈ ������Ʈ
    private bool isOn = false;  // ���� ����
    private float batteryTime = 100;

    private void Awake() 
    {
        lightObj = this.gameObject.transform.GetChild(0).gameObject;  // �÷��� ����Ʈ �ڽ� ù��° ������Ʈ�� ����Ʈ������Ʈ�̾����
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
                Debug.Log("���͸��� �����ϴ�.");
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
