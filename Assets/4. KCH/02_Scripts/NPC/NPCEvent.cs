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
        [SerializeField] private GameObject previousUI;
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
                countGuideText.text = 5 - NPCEvent.touchCount + "���� �� \n �ε�⼼��.";

                if (touchCount == 5)
                {
                    WakeUpNPC();
                    touchCount = 0;
                    countGuideUI.gameObject.SetActive(false);
                }
            }
            
        }

        private void WakeUpNPC()
        {
            previousUI.SetActive(false );
            //var rightControllerVal_a = actionAsset.actionMaps[8].actions[2].ReadValue<bool>();
            for (int i = 0; i < touchPoint.Length; i++)
            {
                touchPoint[i].gameObject.SetActive(false);
            }
            NPC_Animation.SetTrigger("WakeUpNPC");
            //combinationUI.SetActive(true);
            guideUI.SetActive(true);
        }

        private void OnCombinationUI()
        {
            combinationUI.SetActive(true);
        }


        public void SleepNPC()
        {
            NPC_Animation.SetTrigger("SleepNPC");
            combinationUI.SetActive(false);
            guideUI.SetActive(false);
        }
    }
}