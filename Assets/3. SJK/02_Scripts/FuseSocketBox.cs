using NavKeypad;
using UnityEngine;

namespace Shim
{
    public class FuseSocketBox : MonoBehaviour
    {
        public FuseSocket[] fuseSockets; // FuseSocket �迭
        public GameObject hiddenObject; // ����� ǻ�� ������ ���� Ȱ��ȭ�� ������ ������Ʈ

        private Password passwordScript; // Password ��ũ��Ʈ�� ������ ������ ����
        private string password = "open"; // ��ȣ ����

        private bool objectActivated = false;

        private void Start()
        {
            // FuseSocketBox �Ʒ��� ��� FuseSocket ������Ʈ�� ã�� �迭�� �Ҵ��մϴ�.
            fuseSockets = GetComponentsInChildren<FuseSocket>();

            // Password ��ũ��Ʈ�� �ν��Ͻ��� �����ɴϴ�.
            passwordScript = FindObjectOfType<Password>();
        }

        private void Update()
        {
            bool allSocketsConnected = true;

            // ��� ���Ͽ� ����� ǻ� Ȯ���մϴ�.
            foreach (FuseSocket socket in fuseSockets)
            {
                if (!socket.IsFuseConnected())
                {
                    allSocketsConnected = false;
                    break;
                }
            }

            // ��� ���Ͽ� ǻ� ����Ǿ��� �� ������ ������Ʈ�� Ȱ��ȭ�մϴ�.
            if (allSocketsConnected && hiddenObject != null && !objectActivated)
            {
                hiddenObject.SetActive(true);
                objectActivated = true;
            }
            // �ƴ� ��� ������ ������Ʈ�� ��Ȱ��ȭ�մϴ�.
            else if (!allSocketsConnected && hiddenObject != null && objectActivated)
            {
                hiddenObject.SetActive(false);
                objectActivated = false;
            }
        }

        // ��ȣ �˻� �Լ�
        public void CheckPassword()
        {
            if (passwordScript != null)
            {
                string input = passwordScript.GetPassword(); // Password ��ũ��Ʈ���� ��ȣ ��������
                if (input == password)
                {
                    // �ùٸ� ��ȣ �Է� �� ������ ������Ʈ�� �ٽ� ��Ȱ��ȭ�մϴ�.
                    hiddenObject.SetActive(false);
                    objectActivated = false;
                }
            }
            else
            {
                Debug.LogError("Password ��ũ��Ʈ�� ã�� �� �����ϴ�.");
            }
        }
    }
}
