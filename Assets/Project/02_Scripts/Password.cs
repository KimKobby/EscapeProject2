using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Shim
{
    public class Password : MonoBehaviour
    {
        public GameObject[] numberObjects; // ���� ������Ʈ �迭
        public GameObject Potion; // ���� ������Ʈ
        private List<int> password; // ������ ��ȣ�� ������ ����Ʈ
        private bool passwordRevealed = false; // ��ȣ�� �̹� ǥ�õǾ����� ����

        private void Start()
        {
            // ���� �õ带 ���� �ð����� �ʱ�ȭ�մϴ�.
            Random.InitState(System.DateTime.Now.Millisecond);

            // ��ȣ�� �����մϴ�.
            GeneratePassword();

            // ������ ��ȣ�� ����մϴ�.
            Debug.Log("������ ��ȣ: " + string.Join("", password));
        }

        // ��ȣ�� �����ϴ� �Լ�
        private void GeneratePassword()
        {
            password = new List<int>();

            // 0���� 9������ ���� �� �������� 4�ڸ� ��ȣ�� �����մϴ�.
            for (int i = 0; i < 4; i++)
            {
                int digit = Random.Range(0, 10);
                password.Add(digit);
            }
        }

        // ���ǰ� �浹���� �� ȣ��Ǵ� �Լ�
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == Potion && !passwordRevealed)
            {
                StartCoroutine(DisplayPassword());
                passwordRevealed = true;
            }
        }

        // ��ȣ�� ���������� Ȱ��ȭ�ϰ� ��Ȱ��ȭ�ϴ� �ڷ�ƾ
        private IEnumerator DisplayPassword()
        {
            // ��ȣ�� �� �� �ݺ��մϴ�.
            for (int j = 0; j < 2; j++)
            {
                // �� ���� ������Ʈ�� ���������� ó���մϴ�.
                for (int i = 0; i < password.Count; i++)
                {
                    // ���� ���ڿ� �ش��ϴ� ������Ʈ�� Ȱ��ȭ�մϴ�.
                    numberObjects[password[i]].SetActive(true);

                    // 1�� ����մϴ�.
                    yield return new WaitForSeconds(1f);

                    // ���� ���ڿ� �ش��ϴ� ������Ʈ�� ��Ȱ��ȭ�մϴ�.
                    numberObjects[password[i]].SetActive(false);
                }
            }
        }
    }
}
