using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Song
{
    public class CustomSocketInteractor : XRSocketInteractor
    {
        private string socketStr;

        // Start is called before the first frame update
        void Start()
        {
            this.selectEntered.AddListener(CheckSocket);
            this.selectExited.AddListener(ExitBox);


        }

        // Update is called once per frame
        void Update()
        {

        }

        void CheckSocket(SelectEnterEventArgs args)
        {

            if (args.interactableObject.transform.name[4].ToString() == this.transform.name)
            {
                socketStr = this.transform.name;

            }

        }

        void ExitBox(SelectExitEventArgs args)
        {
            socketStr = "";

        }


        public bool InCheck()
        {
            return true;
        }


        public string GetSocketString()
        {
            return socketStr;
        }

    }

}
