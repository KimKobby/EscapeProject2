using UnityEngine;

namespace Shim
{
    public class FuseSocketBox : MonoBehaviour
    {
        public FuseSocket[] fuseSockets; // FuseSocket 배열
        public GameObject hiddenObject; // 연결된 퓨즈 개수에 따라 활성화할 숨겨진 오브젝트

        private void Start()
        {
            // FuseSocketBox 아래의 모든 FuseSocket 컴포넌트를 찾아 배열에 할당합니다.
            fuseSockets = GetComponentsInChildren<FuseSocket>();
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
            if (allSocketsConnected && hiddenObject != null)
            {
                hiddenObject.SetActive(true);
            }
            // 아닐 경우 숨겨진 오브젝트를 비활성화합니다.
            else if (hiddenObject != null)
            {
                hiddenObject.SetActive(false);
            }
        }
    }
}
