using NavKeypad;
using UnityEngine;

namespace Shim
{
    public class FuseSocketBox : MonoBehaviour
    {
        public FuseSocket[] fuseSockets; // FuseSocket 배열
        public GameObject hiddenObject; // 연결된 퓨즈 개수에 따라 활성화할 숨겨진 오브젝트

        public AudioClip singleFuseConnectedSound; // 1개 퓨즈 연결 효과음
        public AudioClip allFusesConnectedSound; // 모든 퓨즈 연결 효과음

        private Password passwordScript; // Password 스크립트의 참조를 저장할 변수
        private string password = "open"; // 암호 변수

        private bool objectActivated = false;
        private bool singleSoundPlayed = false; // 1개 퓨즈 연결 효과음 재생 여부를 나타내는 변수
        private bool allSoundPlayed = false; // 모든 퓨즈 연결 효과음 재생 여부를 나타내는 변수

        private AudioSource audioSource; // 전체적인 AudioSource를 관리하기 위한 변수

        private void Start()
        {
            // FuseSocketBox 아래의 모든 FuseSocket 컴포넌트를 찾아 배열에 할당합니다.
            fuseSockets = GetComponentsInChildren<FuseSocket>();

            // Password 스크립트의 인스턴스를 가져옵니다.
            passwordScript = FindObjectOfType<Password>();

            // AudioSource 컴포넌트를 가져오거나 없으면 추가합니다.
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }

        private void Update()
        {
            bool allSocketsConnected = true;

            // 모든 소켓에 연결된 퓨즈를 확인합니다.
            foreach (FuseSocket socket in fuseSockets)
            {
                if (!socket.IsFuseConnected())
                {
                    allSocketsConnected = false;
                    break;
                }
            }

            // 모든 소켓에 퓨즈가 연결되었을 때 숨겨진 오브젝트를 활성화하고 효과음 재생합니다.
            if (allSocketsConnected && hiddenObject != null && !objectActivated && !allSoundPlayed)
            {
                hiddenObject.SetActive(true);
                objectActivated = true;
                PlaySound(allFusesConnectedSound);
                allSoundPlayed = true; // 한 번 재생되었음을 표시
            }
            // 1개 이상의 소켓에 퓨즈가 연결되지 않았을 경우 숨겨진 오브젝트를 비활성화하고 효과음 재생합니다.
            else if (!allSocketsConnected && hiddenObject != null && objectActivated && !singleSoundPlayed)
            {
                hiddenObject.SetActive(false);
                objectActivated = false;
                PlaySound(singleFuseConnectedSound);
                singleSoundPlayed = true; // 한 번 재생되었음을 표시
            }
        }

        // 암호 검사 함수
        public void CheckPassword()
        {
            if (passwordScript != null)
            {
                string input = passwordScript.GetPassword(); // Password 스크립트에서 암호 가져오기
                if (input == password)
                {
                    // 올바른 암호 입력 시 숨겨진 오브젝트를 다시 비활성화합니다.
                    hiddenObject.SetActive(false);
                    objectActivated = false;
                    singleSoundPlayed = false; // 다시 초기화하여 다음 재생을 허용
                    allSoundPlayed = false; // 다시 초기화하여 다음 재생을 허용
                }
            }
            else
            {
                Debug.LogError("Password 스크립트를 찾을 수 없습니다.");
            }
        }

        // 효과음을 재생하는 함수
        private void PlaySound(AudioClip clip)
        {
            if (clip != null && audioSource != null)
            {
                audioSource.PlayOneShot(clip);
            }
        }
    }
}
