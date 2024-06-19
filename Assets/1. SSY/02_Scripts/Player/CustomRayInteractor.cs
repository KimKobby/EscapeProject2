using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Transformers;
using UnityEngine.XR.Interaction.Toolkit.UI;

namespace Song
{
    public class CustomRayInteractor : XRRayInteractor
    {
        private ControllerInputData controllerinputdata;


        void Start()
        {
            // Debug.Log("Start");
            controllerinputdata = this.transform.root.GetComponent<ControllerInputData>();

            this.selectEntered.AddListener(SelectSomething);
            this.selectExited.AddListener(SelectExitSomething);
            this.hoverEntered.AddListener(HoverEnterSomething);
            this.hoverExited.AddListener(HoverExitSomething);
            this.uiHoverEntered.AddListener(RightHandRaySelect);
        }

        //Ray컨트롤러 클릭 시 
        public void SelectSomething(SelectEnterEventArgs args)
        {

            Debug.Log("SELECT");
            //자물쇠 돌릴때
            if (args.interactableObject.transform.CompareTag("Well"))
            {
                float val = this.transform.root.GetComponent<ControllerInputData>().getStickVal();
                args.interactableObject.transform.Rotate(new Vector3(val * 10f, 0, 0));
                //Debug.Log(args.interactableObject.transform.rotation);
            }


            ////자물쇠 버튼 클릭할때
            //if (args.interactableObject.transform.CompareTag("Btn"))
            //{
            //    Debug.Log("root = " + args.interactableObject.transform.root);

            //    args.interactableObject.transform.root.GetComponent<Lock>().ClickLockBtn();
            //}

            if (args.interactableObject.transform.name == "Prev_Btn")
            {
                //Debug.Log("Prev_Btn");
            }

        }


        private void SelectExitSomething(SelectExitEventArgs args)
        {
           // Debug.Log("SelectExited");
        }

        private void HoverEnterSomething(HoverEnterEventArgs  args)
        {
          if(args.interactableObject.transform.gameObject.layer ==  5)
            {
                Debug.Log("HoverEnterSomething");
            }

            if (args.interactableObject.transform.CompareTag("Btn"))
            {
               // Debug.Log("Button Hover");

                var rayselectval = args.interactorObject.transform.parent.parent.GetComponent<ControllerInputData>().getInRaySelectVal();
             

                if (rayselectval == 1)
                {
                    var button = args.interactableObject;
                    var lockButton = button.transform.parent.GetComponent<Lock>();
                    lockButton.ClickLockBtn();
                }

            }
        }

        private void HoverExitSomething(HoverExitEventArgs args)
        {
           // Debug.Log("Hover Exit");
        }

        void RightHandRaySelect(UIHoverEventArgs args)
        {
            //Start Btn 클릭 시
            if (controllerinputdata.getInRaySelectVal() == 1f && args.uiObject.name == "Btn_Start")
            {
                args.uiObject.transform.root.GetChild(0).GetComponent<UIScript>().StartClick();
            }

            //Setting Btn 클릭 시
            if (controllerinputdata.getInRaySelectVal() == 1f && args.uiObject.name == "Btn_Setting")
            {
                args.uiObject.transform.root.GetChild(0).GetComponent<UIScript>().SettingClick();
            }

            //Alpha Btn 클릭 시
            if (controllerinputdata.getInRaySelectVal() == 1f && args.uiObject.name == "Btn_Alpha")
            {
                args.uiObject.transform.root.GetChild(0).GetComponent<UIScript>().AlphaClick();
            }
        }

    }

}