
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;


namespace Song
{
    public class ControllerInputData : MonoBehaviour
    {
        public InputActionAsset actionAsset;

        public GameObject leftRayController;
        public GameObject rightRayController;

        public GameObject leftDirectController;
        public GameObject rightDeirectController;

        public bool b_RightControllerRay;

        public GameObject clockinnerobj;
        public GameObject headinnerobj;

        public Canvas clockCanvas;


        private float rightJoyStickVal;
        private float inRaySelectVal;

        public float getStickVal()
        {
            return rightJoyStickVal;
        }

        public float getInRaySelectVal()
        {
            return inRaySelectVal;
        }
        void Start()
        {
            clockCanvas.transform.gameObject.SetActive(false);
        }

        void Update()
        {
            LeftHandCheck();
            TriggerClick();
        }

        private void TriggerClick()
        {
            var rightControllerVal = actionAsset.actionMaps[5].actions[2].ReadValue<float>();
            inRaySelectVal = actionAsset.actionMaps[5].actions[0].ReadValue<float>();

            rightJoyStickVal = actionAsset.actionMaps[6].actions[5].ReadValue<Vector2>().y;


            if (rightControllerVal == 1f)
            {
                b_RightControllerRay = true;
                rightRayController.SetActive(true);
                rightDeirectController.SetActive(false);

            }
            else if (rightControllerVal == 0f)
            {
                b_RightControllerRay = false;

                leftRayController.SetActive(false);
                rightRayController.SetActive(false);

                leftDirectController.SetActive(true);
                rightDeirectController.SetActive(true);
            }


        }

        void LeftHandCheck()
        {
            // var leftControllerRotationVal = actionAsset.actionMaps[1].actions[1].ReadValue<Quaternion>().eulerAngles;

            //Debug.Log(leftControllerRotationVal);

            var clockrightval = clockinnerobj.transform.right;
            var headforwrdval = headinnerobj.transform.forward;

            var innerval = Vector3.Dot(clockrightval, headforwrdval);

            // Debug.Log("innerval :" + innerval);

            if (innerval <= -0.6f && innerval >= -1f)
            {
                clockCanvas.transform.gameObject.SetActive(true);
            }
            else
            {
                clockCanvas.transform.gameObject.SetActive(false);
            }
        }
    }
}
