using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NPCEvent : MonoBehaviour
{
    [SerializeField]
    private GameObject combinationUI;
    [SerializeField]
    private GameObject guideUI;
    //[SerializeField]
    //private InputActionAsset actionAsset;
    [SerializeField]
    private Collider[] touchPoint;

    private int touchCount;

    private Animator NPC_Animation;

    private void Start()
    {
        NPC_Animation = this.GetComponent<Animator>();
    }

/*    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("트리거" + other.gameObject.name);
        Debug.Log(touchCount);
        if (other.CompareTag("InteractiveObject"))
        {
            touchCount++;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("충돌" + collision.gameObject.name);
        Debug.Log(touchCount);
        if (collision.collider.CompareTag("InteractiveObject"))
        {
            touchCount++;
        }
    }*/

    private void Update()
    {
        if (touchCount > 5)
        {
            WakeUpNPC();
        }
    }

    private void WakeUpNPC()
    {
        //var rightControllerVal_a = actionAsset.actionMaps[8].actions[2].ReadValue<bool>();
        for (int i = 0; i < touchPoint.Length; i++)
        {
            touchPoint[i].gameObject.SetActive(false);
        }
        NPC_Animation.SetTrigger("WakeUpNPC");
        combinationUI.SetActive(true);
    }

    public void SleepNPC()
    {
        NPC_Animation.SetTrigger("SleepNPC");
        combinationUI.SetActive(false);
    }
}
