using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NPC
{
    public class NPCEvent : MonoBehaviour
    {
        [SerializeField] private GameObject combinationUI;
        [SerializeField] private GameObject guideUI;
        [SerializeField] private GameObject countGuideUI;
        [SerializeField] private TMP_Text countGuideText;
        [SerializeField] private Collider[] touchPoint;
        public static int touchCount;
        private Animator NPC_Animation;

        private void Start()
        {
            NPC_Animation = this.GetComponent<Animator>();
        }


        private void Update()
        {
            if (touchCount > 0)
            {
                countGuideUI.gameObject.SetActive(true);
                countGuideText.text = 10 - NPCEvent.touchCount + "번을 더 두들기세요.";

                if (touchCount == 10)
                {
                    WakeUpNPC();
                    touchCount = 0;
                    countGuideUI.gameObject.SetActive(false);
                }
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
            guideUI.SetActive(true);
        }

        public void SleepNPC()
        {
            NPC_Animation.SetTrigger("SleepNPC");
            combinationUI.SetActive(false);
            guideUI.SetActive(false);
        }
    }
}