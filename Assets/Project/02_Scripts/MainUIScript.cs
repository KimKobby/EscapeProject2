using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Song
{
    public class MainUIScript : MonoBehaviour
    {
        public Button volume_Btn;
        public Button brightnness_Btn;
        public Button Exit_Btn;

        public GameObject volume_img;
        public GameObject brightnness_img;

        // Start is called before the first frame update
        void Start()
        {
            Exit_Btn.onClick.AddListener(ExitMainScreen);

            volume_Btn.onClick.AddListener(() => VolumeScreenOpen(true));

            brightnness_Btn.onClick.AddListener(() => BrightnessScreenOpen(true));

        }

        void VolumeScreenOpen(bool isVisible)
        {
            Debug.Log("VolumeScreenOpen");

            volume_img.SetActive(isVisible);
        }

        void BrightnessScreenOpen(bool isVisible)
        {
            Debug.Log("BrightnessScreenOpen");
            brightnness_img.SetActive(isVisible);
        }

        void ExitMainScreen()
        {
            Debug.Log("ExitMainScreen");
            this.gameObject.SetActive(false);
        }


    }

}
