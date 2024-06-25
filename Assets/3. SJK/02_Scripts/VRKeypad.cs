using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

namespace NavKeypad
{
    public class VRKeypad : MonoBehaviour
    {
        public TMP_Text inputText; // �Է� ���� ǥ���� TMP Text ���
        public TMP_Text resultText; // ��� �޽����� ǥ���� TMP Text ���
        public GameObject canvasUI; // UI Canvas ������Ʈ�� ������ ����
        public GameObject[] lockedDoors; // ����ִ� �� ������Ʈ�� ������ ����
        public float[] doorOpenAngles = { -90f, 90f }; // ���� ���� ������ ������ �迭
        public float doorOpenSpeed = 2f; // ���� ���� �ӵ��� ������ ����
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
            if (doorOpenAngles == null || doorOpenAngles.Length != lockedDoors.Length)
                Debug.LogError("doorOpenAngles �迭�� ũ�Ⱑ lockedDoors �迭�� ũ��� ��ġ���� �ʽ��ϴ�.");

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
                resultText.text = "OPEN"; // ���� �޽��� ǥ��

                // ���� ȿ���� ��� �� �� ����
                StartCoroutine(PlayCorrectSoundAndOpenDoor());
            }
            else
            {
                Debug.Log("��ȣ�� ��ġ���� �ʽ��ϴ�.");
                resultText.text = "WARNING"; // Ʋ�� ��й�ȣ �޽��� ǥ��

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
            for (int i = 0; i < lockedDoors.Length; i++)
            {
                if (lockedDoors[i] != null)
                {
                    StartCoroutine(OpenDoor(lockedDoors[i], doorOpenAngles[i], doorOpenSpeed));
                }
            }

            // ��� ���� ���� ���� ��ٸ���
            yield return new WaitForSeconds(doorOpenSpeed);

            // �� ��ȯ
            SceneManager.LoadScene(5);
        }

        // �� ���� �ڷ�ƾ
        private IEnumerator OpenDoor(GameObject door, float angle, float speed)
        {
            Quaternion initialRotation = door.transform.rotation;
            Quaternion targetRotation = initialRotation * Quaternion.Euler(0, angle, 0);
            float elapsedTime = 0f;

            while (elapsedTime < speed)
            {
                door.transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime / speed);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            door.transform.rotation = targetRotation;
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
