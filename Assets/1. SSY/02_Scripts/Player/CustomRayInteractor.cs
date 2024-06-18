using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Transformers;

namespace Song
{
    public class CustomRayInteractor : XRRayInteractor
    {
        void Start()
        {
            // Debug.Log("Start");

            this.selectEntered.AddListener(SelectSomething);
            this.selectExited.AddListener(SelectExitSomething);
            this.hoverEntered.AddListener(HoverEnterSomething);
            this.hoverExited.AddListener(HoverExitSomething);
        }

        //Ray컨트롤러 클릭 시 
        public void SelectSomething(SelectEnterEventArgs args)
        {
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

    }

}