using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;


namespace Song
{
    public class RemoteControl : MonoBehaviour
    {
        public VideoClip[] videoClips;
        //0 : 전원, 1 : 음소거, 2 : 이전화면, 3: 다음화면, 4 : 볼륨up, 5: 볼륨다운
        public Button[] btns;

        public GameObject screen;
        private VideoPlayer video;

        public int currenClipIdx;
        [SerializeField] private bool isOn;
        private bool isMute;

        void Start()
        {


            video = screen.GetComponent<VideoPlayer>();
            video.gameObject.SetActive(false);

            video.clip = videoClips[currenClipIdx];


            for (int i = 0; i < btns.Length; ++i)
            {
                int temp = i;

                btns[i].onClick.AddListener(() => SetBtnFunction(temp));
            }
        }

        private void SetBtnFunction(int _idx)
        {
            switch (_idx)
            {
                case 0:
                    ScreenPowerOn();
                    break;

                case 1:
                    VolumeMute();
                    break;
                case 2:
                    ChannelControl(-1);
                    break;

                case 3:
                    ChannelControl(1);
                    break;

                default:

                    break;
            }
        }




        public void ScreenPowerOn()
        {
            isOn = !isOn;

            Debug.Log("ScrrenPower");

            if (isOn)
            {
                screen.SetActive(true);
            }
            else
            {
                screen.SetActive(false);
            }

        }

        public void VolumeMute()
        {
            if (isOn)
            {
                isMute = !isMute;
                if (isMute)
                {
                    video.SetDirectAudioMute(0, isMute);
                }
            }


        }

        public void ChannelControl(int _control)
        {
            if (isOn)
            {
                currenClipIdx += _control;

                if (currenClipIdx < 0)
                {
                    currenClipIdx = videoClips.Length - 1;
                }
                else if (currenClipIdx > videoClips.Length - 1)
                {
                    currenClipIdx = 0;
                }

                video.clip = videoClips[currenClipIdx];
                video.Play();

            }

        }


    }

}
