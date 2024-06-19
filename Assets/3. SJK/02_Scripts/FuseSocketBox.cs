using NavKeypad;
using UnityEngine;

namespace Shim
{
    public class FuseSocketBox : MonoBehaviour
    {
        public FuseSocket[] fuseSockets; // FuseSocket �迭
        public GameObject hiddenObject; // ����� ǻ�� ������ ���� Ȱ��ȭ�� ������ ������Ʈ

        public AudioClip singleFuseConnectedSound; // 1�� ǻ�� ���� ȿ����
        public AudioClip allFusesConnectedSound; // ��� ǻ�� ���� ȿ����

        private Password passwordScript; // Password ��ũ��Ʈ�� ������ ������ ����
        private string password = "open"; // ��ȣ ����

        private bool objectActivated = false;
        private bool singleSoundPlayed = false; // 1�� ǻ�� ���� ȿ���� ��� ���θ� ��Ÿ���� ����
        private bool allSoundPlayed = false; // ��� ǻ�� ���� ȿ���� ��� ���θ� ��Ÿ���� ����

        private AudioSource audioSource; // ��ü���� AudioSource�� �����ϱ� ���� ����

        private void Start()
        {
            // FuseSocketBox �Ʒ��� ��� FuseSocket ������Ʈ�� ã�� �迭�� �Ҵ��մϴ�.
            fuseSockets = GetComponentsInChildren<FuseSocket>();

            // Password ��ũ��Ʈ�� �ν��Ͻ��� �����ɴϴ�.
            passwordScript = FindObjectOfType<Password>();

            // AudioSource ������Ʈ�� �������ų� ������ �߰��մϴ�.
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
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

            // ��� ���Ͽ� ǻ� ����Ǿ��� �� ������ ������Ʈ�� Ȱ��ȭ�ϰ� ȿ���� ����մϴ�.
            if (allSocketsConnected && hiddenObject != null && !objectActivated && !allSoundPlayed)
            {
                hiddenObject.SetActive(true);
                objectActivated = true;
                PlaySound(allFusesConnectedSound);
                allSoundPlayed = true; // �� �� ����Ǿ����� ǥ��
            }
            // 1�� �̻��� ���Ͽ� ǻ� ������� �ʾ��� ��� ������ ������Ʈ�� ��Ȱ��ȭ�ϰ� ȿ���� ����մϴ�.
            else if (!allSocketsConnected && hiddenObject != null && objectActivated && !singleSoundPlayed)
            {
                hiddenObject.SetActive(false);
                objectActivated = false;
                PlaySound(singleFuseConnectedSound);
                singleSoundPlayed = true; // �� �� ����Ǿ����� ǥ��
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
                    singleSoundPlayed = false; // �ٽ� �ʱ�ȭ�Ͽ� ���� ����� ���
                    allSoundPlayed = false; // �ٽ� �ʱ�ȭ�Ͽ� ���� ����� ���
                }
            }
            else
            {
                Debug.LogError("Password ��ũ��Ʈ�� ã�� �� �����ϴ�.");
            }
        }

        // ȿ������ ����ϴ� �Լ�
        private void PlaySound(AudioClip clip)
        {
            if (clip != null && audioSource != null)
            {
                audioSource.PlayOneShot(clip);
            }
        }
    }
}
