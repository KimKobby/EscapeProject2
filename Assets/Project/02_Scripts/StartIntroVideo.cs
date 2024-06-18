using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace Start
{
    public class StartIntroVideo : MonoBehaviour
    {
        [SerializeField]
        private VideoPlayer introVideo;
        private bool isVideo = false;

        public void OnVideo()
        {
            isVideo = true;
            introVideo.Play();
            introVideo.loopPointReached += HideVideo;
        }

        private void HideVideo(VideoPlayer vp)
        {
            vp = introVideo;
            introVideo.gameObject.SetActive(false);
        }


        private void Start()
        {
            introVideo = this.GetComponent<VideoPlayer>();
        }

        private void Update()
        {
            if (!isVideo && Input.GetMouseButtonDown(1))
            {
                OnVideo();
            }
        }
    }
}
