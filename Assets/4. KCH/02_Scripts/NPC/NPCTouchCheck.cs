using NPC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTouchCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Only Hand Interaction"))
        {
            NPCEvent.touchCount++;
        }
        
    }
}


