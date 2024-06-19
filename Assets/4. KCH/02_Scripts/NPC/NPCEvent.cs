using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NPCEvent : MonoBehaviour
{
    [SerializeField]
    private GameObject combinationUI;
    [SerializeField]
    private InputActionAsset actionAsset;
    private Animator anim;

    [SerializeField]
    Collider area;

    private void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        WakeUpNPC();
        area.enabled = false;
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))//태그 바꿔야함
    //    {
    //        var rightControllerVal_a = actionAsset.actionMaps[8].actions[2].ReadValue<bool>();
    //        if (rightControllerVal_a)
    //        {
    //            WakeUpNPC();
    //        }
    //    }
    //}

    private void WakeUpNPC()
    {
        anim.SetTrigger("WakeUpNPC");
        combinationUI.SetActive(true);
    }

    public void SleepNPC()
    {
        anim.SetTrigger("WakeUpNPC");
        combinationUI.SetActive(false);
    }
}
