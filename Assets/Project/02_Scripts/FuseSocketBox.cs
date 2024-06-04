using UnityEngine;

namespace Shim
{
    public class FuseSocketBox : MonoBehaviour
    {
        public FuseSocket[] fuseSockets; // FuseSocket �迭
        public GameObject hiddenObject; // ����� ǻ�� ������ ���� Ȱ��ȭ�� ������ ������Ʈ

        private void Start()
        {
            // FuseSocketBox �Ʒ��� ��� FuseSocket ������Ʈ�� ã�� �迭�� �Ҵ��մϴ�.
            fuseSockets = GetComponentsInChildren<FuseSocket>();
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
            if (allSocketsConnected && hiddenObject != null)
            {
                hiddenObject.SetActive(true);
            }
            // �ƴ� ��� ������ ������Ʈ�� ��Ȱ��ȭ�մϴ�.
            else if (hiddenObject != null)
            {
                hiddenObject.SetActive(false);
            }
        }
    }
}
