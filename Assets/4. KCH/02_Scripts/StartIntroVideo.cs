using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

namespace Video
{
    public class StartIntroVideo : MonoBehaviour
    {
        [SerializeField]
        private VideoPlayer video;
        private bool isVideo = false;
        public int nextSceneNumber;

        public void OnVideo()
        {
            isVideo = true;
            video.Play();
            video.loopPointReached += HideVideo;
        }

        private void HideVideo(VideoPlayer videoPlayer)
        {
            SceneManager.LoadScene(nextSceneNumber);
            videoPlayer = video;
        }


        private void Start()
        {
            video = this.GetComponent<VideoPlayer>();
            if (!isVideo)
            {
                OnVideo();
            }
        }
    }
}
