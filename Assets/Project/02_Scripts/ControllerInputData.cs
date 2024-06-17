
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.UIElements.Experimental;
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

        public GameObject Inventory;


        public bool isClicked = false;
        private bool xButtonClick = false;


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
<<<<<<< HEAD
          //  this.transform.position = new Vector3(this.transform.position.x, 0.5f, this.transform.position.z);
=======
>>>>>>> SJK_test
            Inventory.gameObject.SetActive(false);
            clockCanvas.transform.gameObject.SetActive(false);
            actionAsset.actionMaps[8].actions[0].canceled += PlayerSitandStand;
        }

        void Update()
        {
            LeftHandCheck();
            TriggerClick();
            InventoryOn();
            PlayerMoveSpeed();

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

        void InventoryOn()
        {
            var leftControllerVal_y = actionAsset.actionMaps[8].actions[1].ReadValue<float>();

            //왼쪽 컨트롤러 y누르면 On
            if(leftControllerVal_y == 1f)
            {
                Inventory.gameObject.SetActive(true);
            }
            else
            {
                Inventory.gameObject.SetActive(false);
            }
        }


        void PlayerMoveSpeed()
        {

            //왼쪽 컨트롤러 중지값
            var leftControllerVal = actionAsset.actionMaps[2].actions[0].ReadValue<float>();

            //왼쪽 컨트롤러 조이스틱값
            var leftControllerJoystickval = actionAsset.actionMaps[3].actions[5].ReadValue<Vector2>();


            //중지를 눌렀을 때(달리기)
            if (leftControllerVal == 1f)
            {
                //움직임이 없는경우
                if (leftControllerJoystickval == new Vector2(0, 0))
                {
                    
                   this.GetComponent<ContinuousMoveProviderBase>().moveSpeed = 1f;
                    
                }
                //달리는 중
                else
                {
                    this.GetComponent<ContinuousMoveProviderBase>().moveSpeed = 3f;
                }
            }
            //평소
            else
            {
                this.GetComponent<ContinuousMoveProviderBase>().moveSpeed = 1f;
            }
        }


        public void PlayerSitandStand(InputAction.CallbackContext action)
        {
            xButtonClick = !xButtonClick;

            //X 토글방식 앉기 일어서기
            if (xButtonClick)
            {
                this.transform.position = new Vector3(this.transform.position.x, -0.5f, this.transform.position.z);
               
            }
            else
                this.transform.position = new Vector3(this.transform.position.x, 0.5f, this.transform.position.z);
        }

    }
}
