using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace Song
{
    public class UIScript : MonoBehaviour
    {
        public AudioClip[] audioclips;

        public Button start_Btn;
        public Button setting_Btn;
        //public Button startReset_Btn;
       // public Button settingReset_Btn;
        public Button alpha_Btn;
        public GameObject mainCanvas;

        private bool mainvisible;

        public GameObject startBox;
        public GameObject settingBox;
        public GameObject volumeUI;


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
            volumeUI.transform.GetChild(2).GetComponent<Slider>().onValueChanged.AddListener(VolumeSound);
        }

        // Update is called once per frame
        void Update()
        {

        }

 

        public void StartClick()
        {
            startBox.GetComponent<SocketCheck>().OnClickBtn();
           // Debug.Log("StartClick");
            if (startBox.GetComponent<SocketCheck>().GetStartCheck())
            {
                AudioManager.Inst.UIBtnClickSound(true);
                //Debug.Log("StartCheck");
                SceneManager.LoadScene(1);
            }
            else
            {
                AudioManager.Inst.UIBtnClickSound(false);
            }
        }

        public void SettingClick()
        {
            // Debug.Log("SettingClick");
            settingBox.GetComponent<SocketCheck>().OnClickBtn();

            if (settingBox.GetComponent<SocketCheck>().GetSettingCheck())
            {
                //Debug.Log("StartCheck");
                AudioManager.Inst.UIBtnClickSound(true);
                mainCanvas.SetActive(!mainvisible);
            }
            else
            {
                AudioManager.Inst.UIBtnClickSound(false);
            }


        }

        void Reset()
        {
            Debug.Log("Reset");
        }

        public void AlphaClick()
        {
            AudioManager.Inst.UIBtnClickSound(true);
            ObjPool.GetComponent<ObjectPool>().OnDequeue();

        }

        public void VolumeSound(float _value)
        {
            //Debug.Log(_value);
             AudioManager.Inst.SetMusicVolume(_value);
        }
    }

}
