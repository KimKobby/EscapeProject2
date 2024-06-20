using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEvent : MonoBehaviour
{
    public delegate void DoorOpenedEventHandler();
    public event DoorOpenedEventHandler OnDoorOpened;

    private bool isOpen = false;

    public void OpenDoor()
    {
        if (!isOpen)
        {
            isOpen = true;
            Debug.Log("Door opened" + gameObject.name);
            OnDoorOpened?.Invoke();
        }
    }
}
