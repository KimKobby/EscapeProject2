using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detect2 : MonoBehaviour
{
    [SerializeField]
    private GameObject door;
    public bool isLocked = false;

    private void Start()
    {
       // door.GetComponent<Transform>().rotation = Quaternion.Euler(0f, 0f, 0f);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isLocked)
            {
                isLocked = true;
                Debug.Log("DoorLock");
                door.GetComponent<Transform>().rotation = Quaternion.Euler(0f, -90f, 0f);
                //door.GetComponent<Transform>().rotation = Quaternion.Euler(0f, -87.188f, 0f);
                //Y 114.449  -1.351
            }
        }
    }
}
