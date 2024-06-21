using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NavKeypad
{
    public class Password : MonoBehaviour
    {
        public GameObject[] numberObjects; // 0-9 ���ڸ� ǥ���� GameObject �迭
        public GameObject[] potionObjects; // �浹�� ������ ���� ������Ʈ
        public AudioClip collisionSound; // �浹 �Ҹ�
        private AudioSource audioSource; // ����� �ҽ�
        private List<int> password; // ������ ��ȣ�� ������ ����Ʈ

        private void Start()
        {
            // ���� �ʱ�ȭ
            Random.InitState(System.DateTime.Now.Millisecond);
            // ��ȣ ����
            GeneratePassword();
            // ������ ��ȣ ���
            Debug.Log("������ ��ȣ: " + string.Join("", password));

            // ����� �ҽ� ������Ʈ�� �������ų� ������ �߰�
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }

        // ��ȣ ���� �޼��� (�ߺ��� ������� �ʴ� ���� ���)
        private void GeneratePassword()
        {
            password = new List<int>();

            // �ߺ��� ������� �ʴ� ���� ����
            while (password.Count < 4)
            {
                int digit = Random.Range(0, 10); // 0-9 ������ ���� ���� ����

                // ����Ʈ�� ���� ��쿡�� �߰�
                if (!password.Contains(digit))
                {
                    password.Add(digit); // ����Ʈ�� �߰�
                }
            }
        }

        // ���� �浹 ���� �޼���
        private void OnTriggerEnter(Collider other)
        {
            foreach ( var potionObject in potionObjects)
            {

                if (other.gameObject == potionObject)
                {
                    // ���� ������Ʈ ��Ȱ��ȭ
                    potionObject.SetActive(false);

                    // �浹 �Ҹ� ���
                    if (collisionSound != null)
                    {
                        AudioSource.PlayClipAtPoint(collisionSound, transform.position);
                    }

                    // ��ȣ ǥ�� �ڷ�ƾ ����
                    StartCoroutine(DisplayPassword());
                }
            }
        }

        // ��ȣ�� ǥ���ϴ� �ڷ�ƾ
        private IEnumerator DisplayPassword()
        {
            for (int repeat = 0; repeat < 2; repeat++)
            {
                for (int i = 0; i < password.Count; i++)
                {
                    int digit = password[i]; // ���� ��ġ�� ��ȣ ���ڸ� ������
                    GameObject numberObject = numberObjects[digit]; // �ش� ���ڿ� �����ϴ� GameObject�� ������

                    numberObject.SetActive(true); // �ش� ���ڸ� Ȱ��ȭ
                    yield return new WaitForSeconds(1f); // 1�� ���
                    numberObject.SetActive(false); // �ش� ���ڸ� ��Ȱ��ȭ
                }

                // �� ���� �ݺ��� ���� ������ 1���� �����̸� �ݴϴ�.
                yield return new WaitForSeconds(1f);
            }
        }

        // ��ȣ�� ��ȯ�ϴ� �޼���
        public string GetPassword()
        {
            return string.Join("", password);
        }
    }
}
