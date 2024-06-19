using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEvent : MonoBehaviour
{
    public Animator doorAnim;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorAnim.SetTrigger("IsOpen");
        }
    }

    public void OnTriggerExit(Collider other)
    {
       if (other.CompareTag("Player"))
        {
            doorAnim.SetTrigger("IsClose");
        }
    }
}
