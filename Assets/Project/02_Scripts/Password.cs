using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Shim
{
    public class Password : MonoBehaviour
    {
        public GameObject[] numberObjects; // ���� ������Ʈ �迭
        private List<int> password; // ������ ��ȣ�� ������ ����Ʈ

        private void Start()
        {
            // ���� �õ带 ���� �ð����� �ʱ�ȭ�մϴ�.
            Random.InitState(System.DateTime.Now.Millisecond);

            // ��ȣ�� �����մϴ�.
            GeneratePassword();

            // ������ ��ȣ�� ����մϴ�.
            Debug.Log("������ ��ȣ: " + string.Join("", password));

            // ��ȣ�� ���������� Ȱ��ȭ�ϰ� ��Ȱ��ȭ�մϴ�.
            StartCoroutine(DisplayPassword());
        }

        // ��ȣ�� �����ϴ� �Լ�
        private void GeneratePassword()
        {
            password = new List<int>();

            // ��ȣ�� �� �ڸ��� 0���� 9������ ���ڸ� �������� �����Ͽ� �����մϴ�.
            while (password.Count < 4)
            {
                int digit = Random.Range(0, 10);
                if (!password.Contains(digit))
                {
                    password.Add(digit);
                }
            }

            // ��ȣ�� �����մϴ�.
            password.Sort();
        }

        // ��ȣ�� ���������� Ȱ��ȭ�ϰ� ��Ȱ��ȭ�ϴ� �ڷ�ƾ
        IEnumerator DisplayPassword()
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