using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace NavKeypad
{
    public class VRKeypad : MonoBehaviour
    {
        public TMP_Text inputText; // �Է� ���� ǥ���� TMP Text ���
        public GameObject canvasUI; // UI Canvas ������Ʈ�� ������ ����
        public GameObject lockedDoor; // ����ִ� �� ������Ʈ�� ������ ����
        public Password passwordScript; // Password ��ũ��Ʈ�� ������ ����
        private string currentInput = ""; // ���� �Է� ��

        // ���� ��ư�� ������ �� ȣ��Ǵ� �޼���
        public void PressNumber(string number)
        {
            currentInput += number; // ���� �Է� ���� ���ڸ� �߰�
            inputText.text = currentInput; // TMP Text�� ���� �Է� ���� ǥ��
        }

        // OK ��ư�� ������ �� ȣ��Ǵ� �޼���
        public void CheckPassword()
        {
            if (currentInput == passwordScript.GetPassword())
            {
                Debug.Log("��ȣ�� ��ġ�մϴ�!");
                // ��ȣ�� ��ġ�ϴ� ��� ó���� ������ ���⿡ �߰��ϼ���.

                // UI Canvas ��Ȱ��ȭ
                canvasUI.SetActive(false);

                // ��� �� ����
                lockedDoor.SetActive(false);
            }
            else
            {
                Debug.Log("��ȣ�� ��ġ���� �ʽ��ϴ�.");
                // ��ȣ�� ��ġ���� �ʴ� ��� ó���� ������ ���⿡ �߰��ϼ���.
            }

            // �Է� ���� �ʱ�ȭ�մϴ�.
            currentInput = "";
            inputText.text = currentInput;
        }
    }
}
