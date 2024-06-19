using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace NavKeypad
{
    public class VRKeypad : MonoBehaviour
    {
        public TMP_Text inputText; // �Է� ���� ǥ���� TMP Text ���
        public TMP_Text resultText; // ��� �޽����� ǥ���� TMP Text ���
        public GameObject canvasUI; // UI Canvas ������Ʈ�� ������ ����
        public GameObject[] lockedDoors; // ����ִ� �� ������Ʈ�� ������ ����
        public Password passwordScript; // Password ��ũ��Ʈ�� ������ ����
        public AudioSource audioSource; // ȿ������ ����� AudioSource
        public AudioClip buttonPressSound; // ��ư Ŭ�� ȿ����
        public AudioClip correctSound; // ���� ȿ����
        public AudioClip incorrectSound; // ���� ȿ����
        private string currentInput = ""; // ���� �Է� ��

       
        void Start()
        {
            // �� �������� ����� �Ҵ�Ǿ����� Ȯ���մϴ�.
            if (inputText == null)
                Debug.LogError("inputText�� �Ҵ���� �ʾҽ��ϴ�.");
            if (resultText == null)
                Debug.LogError("resultText�� �Ҵ���� �ʾҽ��ϴ�.");
            if (canvasUI == null)
                Debug.LogError("canvasUI�� �Ҵ���� �ʾҽ��ϴ�.");
            if (passwordScript == null)
                Debug.LogError("passwordScript�� �Ҵ���� �ʾҽ��ϴ�.");
            if (lockedDoors == null || lockedDoors.Length == 0)
                Debug.LogError("lockedDoors�� �Ҵ���� �ʾҰų� ��� �ֽ��ϴ�.");
            if (audioSource == null)
                Debug.LogError("audioSource�� �Ҵ���� �ʾҽ��ϴ�.");
            if (buttonPressSound == null || correctSound == null || incorrectSound == null)
                Debug.LogError("ȿ������ �Ҵ���� �ʾҽ��ϴ�.");

            // �ʱ� ���¿��� Ű�е� UI�� ��Ȱ��ȭ�մϴ�.
            canvasUI.SetActive(false);
            resultText.text = ""; // �ʱ� ���¿��� ��� �޽����� ���ϴ�.
        }

        // Ű�е� ������Ʈ�� Ŭ������ �� ȣ��Ǵ� �޼���
        public void ToggleKeypad()
        {
            // Ű�е� UI�� Ȱ��/��Ȱ�� ���¸� ����մϴ�.
            canvasUI.SetActive(!canvasUI.activeSelf);
        }

        // ���� ��ư�� ������ �� ȣ��Ǵ� �޼���
        public void PressNumber(string number)
        {
            currentInput += number; // ���� �Է� ���� ���ڸ� �߰�
            inputText.text = currentInput; // TMP Text�� ���� �Է� ���� ǥ��
            PlaySound(buttonPressSound); // ��ư Ŭ�� ȿ���� ���
        }

        // OK ��ư�� ������ �� ȣ��Ǵ� �޼���
        public void CheckPassword()
        {
            if (passwordScript == null)
            {
                Debug.LogError("passwordScript�� �Ҵ���� �ʾҽ��ϴ�.");
                return;
            }

            if (currentInput == passwordScript.GetPassword())
            {
                Debug.Log("��ȣ�� ��ġ�մϴ�!");
                resultText.text = "�����Դϴ�!"; // ���� �޽��� ǥ��

                // ���� ȿ���� ��� �� �� ����
                StartCoroutine(PlayCorrectSoundAndOpenDoor());
            }
            else
            {
                Debug.Log("��ȣ�� ��ġ���� �ʽ��ϴ�.");
                resultText.text = "��й�ȣ�� Ʋ�Ƚ��ϴ�."; // Ʋ�� ��й�ȣ �޽��� ǥ��

                // ���� ȿ���� ���
                PlaySound(incorrectSound);

                // 1�� �Ŀ� ��� �޽����� ����� �Է°� �ʱ�ȭ
                StartCoroutine(ClearResultTextAndInputAfterDelay(1f));
            }
        }

        // ���� ȿ���� ��� �� �� ����
        private IEnumerator PlayCorrectSoundAndOpenDoor()
        {
            // ���� ȿ���� ���
            PlaySound(correctSound);

            // 1�� ���
            yield return new WaitForSeconds(1f);

            // UI Canvas ��Ȱ��ȭ
            canvasUI.SetActive(false);

            // ��� �� ����
            foreach (GameObject door in lockedDoors)
            {
                if (door != null)
                {
                    door.SetActive(false);
                }
            }
        }

        // ��� �޽����� ����� �Է°� �ʱ�ȭ�ϴ� �ڷ�ƾ
        private IEnumerator ClearResultTextAndInputAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            resultText.text = ""; // ��� �޽����� ���ϴ�.
            ClearInput(); // �Է� ���� �ʱ�ȭ�մϴ�.
        }

        // �Է� ���� �ʱ�ȭ�ϴ� �޼��� (����� ��ư��)
        public void ClearInput()
        {
            currentInput = "";
            inputText.text = currentInput;
        }

        // ȿ���� ��� �޼���
        private void PlaySound(AudioClip clip)
        {
            if (audioSource != null && clip != null)
            {
                audioSource.clip = clip;
                audioSource.Play();
            }
        }
    }
}