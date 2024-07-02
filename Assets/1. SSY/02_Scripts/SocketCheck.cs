using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace Song
{
    public class SocketCheck : MonoBehaviour
    {
        [SerializeField] public GameObject[] socketBoxs;

        private bool b_SocketStartCheck = false;
        private bool b_SocketSettingCheck = false;
        string startstr;
        string settingstr;

        public bool GetStartCheck()
        {
            return b_SocketStartCheck;
        }

        public bool GetSettingCheck()
        {
            return b_SocketSettingCheck;
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            //foreach (GameObject obj in socketBoxs)
            //{
            //    startstr += obj.GetComponent<CustomSocketInteractor>().GetSocketString();
            //}

            //Debug.Log("startstr : " + startstr);
            //startstr = "";
        }

        public void OnClickBtn()
        {
            startstr = "";

            foreach (GameObject obj in socketBoxs)
            {
                startstr += obj.GetComponent<CustomSocketInteractor>().GetSocketString();
            }

            if (startstr == "START")
            {
                b_SocketStartCheck = true;
                Debug.Log("Start True");
            }
            else
            {
                b_SocketStartCheck = false;
            }

            if (startstr == "SETTING")
            {
                b_SocketSettingCheck = true;
                Debug.Log("Setting True");
            }
            else
            {
                b_SocketSettingCheck = false;
            }



        }

        public void OnClickSettingBtn()
        {

        }
    }

}
