using NavKeypad;
using UnityEngine;

namespace Shim
{
    public class FuseSocketBox : MonoBehaviour
    {
        public FuseSocket[] fuseSockets; // FuseSocket 배열
        public GameObject hiddenObject; // 연결된 퓨즈 개수에 따라 활성화할 숨겨진 오브젝트

        private Password passwordScript; // Password 스크립트의 참조를 저장할 변수
        private string password = "open"; // 암호 변수

        private bool objectActivated = false;

        private void Start()
        {
            // FuseSocketBox 아래의 모든 FuseSocket 컴포넌트를 찾아 배열에 할당합니다.
            fuseSockets = GetComponentsInChildren<FuseSocket>();

            // Password 스크립트의 인스턴스를 가져옵니다.
            passwordScript = FindObjectOfType<Password>();
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

            // 모든 소켓에 퓨즈가 연결되었을 때 숨겨진 오브젝트를 활성화합니다.
            if (allSocketsConnected && hiddenObject != null && !objectActivated)
            {
                hiddenObject.SetActive(true);
                objectActivated = true;
            }
            // 아닐 경우 숨겨진 오브젝트를 비활성화합니다.
            else if (!allSocketsConnected && hiddenObject != null && objectActivated)
            {
                hiddenObject.SetActive(false);
                objectActivated = false;
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
                }
            }
            else
            {
                Debug.LogError("Password 스크립트를 찾을 수 없습니다.");
            }
        }
    }
}
