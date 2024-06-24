using Song;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    [SerializeField]
    private GameObject lightObj;  // ����Ʈ ������Ʈ
    private bool isOn = false;  // ���� ����
    private float batteryTime = 100;
    public GameObject player;


    private void Awake()
    {
        lightObj = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;  // �÷��� ����Ʈ �ڽ� ù��° ������Ʈ�� ����Ʈ������Ʈ�̾����
    }

    private void Update()
    {
        
    }



    private void OnTriggerStay(Collider other)
    {
        Debug.Log("�±�");
        if (other.gameObject.CompareTag("Hand"))
        {
            if (player.GetComponent<ControllerInputData>().getAButton())
            {
                Debug.Log("��");
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
       
