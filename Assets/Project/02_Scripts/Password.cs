using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NavKeypad
{
    public class Password : MonoBehaviour
    {
        public GameObject[] numberObjects; // 0-9 ���ڸ� ǥ���� GameObject �迭
        public GameObject potionObject; // �浹�� ������ ���� ������Ʈ
        private List<int> password; // ������ ��ȣ�� ������ ����Ʈ

        private void Start()
        {
            Random.InitState(System.DateTime.Now.Millisecond); // ���� �ʱ�ȭ
            GeneratePassword(); // ��ȣ ����
            Debug.Log("������ ��ȣ: " + string.Join("", password)); // ������ ��ȣ ���
        }

        // ��ȣ ���� �޼���
        private void GeneratePassword()
        {
            password = new List<int>();

            for (int i = 0; i < 4; i++)
            {
                int digit = Random.Range(0, 10); // 0-9 ������ ���� ���� ����
                password.Add(digit); // ����Ʈ�� �߰�
            }
        }

        // ���� �浹 ���� �޼���
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == potionObject)
            {
                StartCoroutine(DisplayPassword()); // ��ȣ ǥ�� �ڷ�ƾ ����
            }
        }

        // ��ȣ�� ǥ���ϴ� �ڷ�ƾ
        private IEnumerator DisplayPassword()
        {
            for (int j = 0; j < 2; j++)
            {
                for (int k = 0; k < 2; k++)
                {
                    for (int i = 0; i < password.Count; i++)
                    {
                        int digit = password[i]; // ���� ��ġ�� ��ȣ ���ڸ� ������
                        GameObject numberObject = numberObjects[digit]; // �ش� ���ڿ� �����ϴ� GameObject�� ������

                        numberObject.SetActive(true); // �ش� ���ڸ� Ȱ��ȭ
                        yield return new WaitForSeconds(1f); // 1�� ���
                        numberObject.SetActive(false); // �ش� ���ڸ� ��Ȱ��ȭ
                    }
                }

                // �� ���� �ݺ��� ���� ������ 1���� �����̸� �ݴϴ�.
                yield return new WaitForSeconds(1f);
            }
        }

        // ����� �Է� ó�� �޼���
        public void AddInput(string input)
        {
            Debug.Log("�Էµ� ��: " + input);
            if (input == string.Join("", password))
            {
                Debug.Log("��ȣ�� ��ġ�մϴ�!");
                // ��ȣ ��ġ �� ������ ���� �߰�
            }
            else
            {
                Debug.Log("��ȣ�� ��ġ���� �ʽ��ϴ�.");
                // ��ȣ ����ġ �� ������ ���� �߰�
            }
        }

        // ��ȣ�� ��ȯ�ϴ� �޼���
        public string GetPassword()
        {
            return string.Join("", password);
        }
    }
}
