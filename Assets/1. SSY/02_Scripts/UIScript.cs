using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace Song
{
    public class UIScript : MonoBehaviour
    {
        public Button start_Btn;
        public Button setting_Btn;
        //public Button startReset_Btn;
       // public Button settingReset_Btn;
        public Button alpha_Btn;
        public GameObject mainCanvas;

        private bool mainvisible;

        public GameObject startBox;
        public GameObject settingBox;


        public GameObject ObjPool;
        // Start is called before the first frame update
        void Start()
        {
            start_Btn.onClick.AddListener(StartClick);
            setting_Btn.onClick.AddListener(SettingClick);
            //settingReset_Btn.onClick.AddListener(() => SettingClick(!mainvisible));
           // startReset_Btn.onClick.AddListener(Reset);
            alpha_Btn.onClick.AddListener(AlphaClick);
            mainCanvas.SetActive(false);

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void StartClick()
        {
            startBox.GetComponent<SocketCheck>().OnClickStartBtn();
            Debug.Log("StartClick");
            if (startBox.GetComponent<SocketCheck>().GetStartCheck())
            {
                Debug.Log("StartCheck");
                SceneManager.LoadScene(1);
               

            }
        }

        public void SettingClick()
        {
            // Debug.Log("SettingClick");
            settingBox.GetComponent<SocketCheck>().OnClickStartBtn();

            if (settingBox.GetComponent<SocketCheck>().GetSettingCheck())
            {
                //Debug.Log("StartCheck");

                mainCanvas.SetActive(!mainvisible);
            }


        }

        void Reset()
        {
            Debug.Log("Reset");
        }

        public void AlphaClick()
        {
            Debug.Log("AlphaClick");
            ObjPool.GetComponent<ObjectPool>().OnDequeue();


        }


    }

}
