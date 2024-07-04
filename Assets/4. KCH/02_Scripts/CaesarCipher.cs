using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Cipher
{
    public class CaesarCipher : MonoBehaviour
    {
        //�̱��� �ν��Ͻ�
        private static CaesarCipher instance = null;

        // �̱��� �ν��Ͻ� ��ȯ
        public static CaesarCipher Instance
        {
            get
            {
                if (instance == null)
                {
                    return null;
                }
                return instance;
            }
        }

        // �̱��� �ν��Ͻ� ����
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        private TMP_Text hintText;

        private const int alphabet = 26; // ���ĺ� 26��
        private int cipherLength = 4; // ��ȣ �ڸ��� 
        private List<int> chosenNumbers = new List<int>(); // �������� ������ ���ڵ�
        private string textCaesarCipher; // ǥ�� �� ���� ��Ʈ ����
        private string direction;  // ��, �� ���� ��
        private int directionDistance; // ��, �� �̵��Ÿ�
        private List<int> computeNumber = new List<int>(); // ����� ���� ���ڵ�
        public string password { get; private set; } // string���� �ٲ� ���� ��ȣ



        private void Start()
        {
            hintText = this.transform.GetChild(0).GetComponent<TMP_Text>();
            CreateCaesarCipher();
        }


        private void CreateRandomNumber()  // ���� ���� ���� �Լ�
        {

            chosenNumbers.Clear();  // ������ ���� ���� ����Ʈ �ʱ�ȭ
            for (int i = 0; i < cipherLength; i++)  // ��ȣ �ڸ��� ��ŭ ���� ���� ����
            {
                int randomNumber;
                do
                {
                    randomNumber = Random.Range(1, alphabet + 1); // 1���� 26 ������ ���� ���� ����
                }
                while (chosenNumbers.Contains(randomNumber)); // �̹� ���õ� �������� Ȯ��

                chosenNumbers.Add(randomNumber); // ���õ� ���ڸ� ����Ʈ�� �߰�
            }
        }


        private void CreateDirection()  // ���� ���� ���� �Լ�
        {
            string[] directionWE = { "����", "����" }; // ��,�� �� ����
            int maxDistance = 3; // ��,�� �̵� �ִ� �̵��Ÿ�
            int randomIndex = Random.Range(0, directionWE.Length);  // �������� �������� �������� �̱�
            direction = directionWE[randomIndex];
            directionDistance = Random.Range(1, maxDistance + 1);  // ���� �������� �� �̵����� ���� �̱�
        }


        private void ConvertPassword()  // ��¥ ��ȣ ��� �Լ�
        {
            password = "";
            computeNumber.Clear();

            for (int i = 0; i < chosenNumbers.Count; i++)
            {
                int temp;
                switch (direction)
                {
                    case "����":
                        temp = chosenNumbers[i] - directionDistance;
                        if (temp < 1)
                        {
                            temp += alphabet;
                        }
                        computeNumber.Add(temp);
                        break;
                    case "����":
                        temp = chosenNumbers[i] + directionDistance;
                        if (temp > alphabet)
                        {
                            temp -= alphabet;
                        }
                        computeNumber.Add(temp);
                        break;
                }
                password += computeNumber[i].ToString();
            }
            Debug.Log("������ȣ ����" + password);
        }

        public void CreateCaesarCipher() // ���� ��ȣ ���� ���� ���� �Լ�
        {
            textCaesarCipher = null;  // ���� ��ȣ ��Ʈ ���� �ʱ�ȭ
            CreateRandomNumber();
            CreateDirection();

            for (int i = 0; i < cipherLength; i++)  // ���� ��ȣ ���� ���� ����
            {
                textCaesarCipher += chosenNumbers[i].ToString();
                if (i != cipherLength - 1) // ������ ���� ���� ������ - �߰�
                {
                    textCaesarCipher += " - ";
                }
            }

            // ���� ��ȣ ǥ�� ����
            textCaesarCipher += "\n" + direction + "���� " + directionDistance + "����..";
            hintText.text = textCaesarCipher;
            //Debug.Log(textCaesarCipher);

            // ��¥ ��ȣ ��ȯ
            ConvertPassword();
        }


        //private void Update()
        //{
        //    if (Input.GetMouseButtonDown(1))
        //    {
        //        CreateCaesarCipher();
        //    }
        //}

    }

}
