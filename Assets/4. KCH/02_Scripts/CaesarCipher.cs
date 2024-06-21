using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Changhoon
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

        private List<int> chosenNumbers = new List<int>(); // �������� ������ ���ڵ�
        private const int alphabetLength = 26; // ���ĺ� 26��
        private int cipherLenght = 4; // ��ȣ �ڸ��� 
        private string textCaesarCipher; // ǥ�� �� ���� ��Ʈ ����
        private string direction;  // ��, �� ���� ��
        private int directionWELenght; // ��, �� �̵��Ÿ�
        private List<int> computeNumber = new List<int>(); // ����� ���� ���ڵ�
        public string password { get; private set; } // string���� �ٲ� ���� ��ȣ



        private void Start()
        {
            hintText = this.transform.GetChild(0).GetComponent<TMP_Text>();
            CreateCaesarCipher();
        }


        private void RandomNumber()  // ���� ���� ���� �Լ�
        {

            chosenNumbers.Clear();  // ������ ���� ���� ����Ʈ �ʱ�ȭ
            for (int i = 0; i < cipherLenght; i++)  // ��ȣ �ڸ��� ��ŭ ���� ���� ����
            {
                int randomNumber;
                do
                {
                    randomNumber = Random.Range(1, alphabetLength + 1); // 1���� 26 ������ ���� ���� ����
                }
                while (chosenNumbers.Contains(randomNumber)); // �̹� ���õ� �������� Ȯ��

                chosenNumbers.Add(randomNumber); // ���õ� ���ڸ� ����Ʈ�� �߰�
            }
        }


        private void Direction()  // ���� ���� ���� �Լ�
        {
            string[] directionWE = { "����", "����" }; // ��,�� �� ����
            int directionWEMaxLenght = 6; // ��,�� �̵� �ִ� �̵��Ÿ�
            int randomIndex = Random.Range(0, directionWE.Length);  // �������� �������� �������� �̱�
            direction = directionWE[randomIndex];
            directionWELenght = Random.Range(1, directionWEMaxLenght + 1);  // ���� �������� �� �̵����� ���� �̱�
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
                        temp = chosenNumbers[i] - directionWELenght;
                        if (temp < 1)
                        {
                            temp += alphabetLength;
                        }
                        computeNumber.Add(temp);
                        break;
                    case "����":
                        temp = chosenNumbers[i] + directionWELenght;
                        if (temp > alphabetLength)
                        {
                            temp -= alphabetLength;
                        }
                        computeNumber.Add(temp);
                        break;
                }
                password += computeNumber[i].ToString();
            }
            Debug.Log(password);
        }

        private void CreateCaesarCipher() // ���� ��ȣ ���� ���� ���� �Լ�
        {
            textCaesarCipher = null;  // ���� ��ȣ ��Ʈ ���� �ʱ�ȭ
            RandomNumber();
            Direction();

            for (int i = 0; i < cipherLenght; i++)  // ���� ��ȣ ���� ���� ����
            {
                textCaesarCipher += chosenNumbers[i].ToString();
                if (i != cipherLenght - 1) // ������ ���� ���� ������ - �߰�
                {
                    textCaesarCipher += " - ";
                }
            }

            // ���� ��ȣ ǥ�� ����
            textCaesarCipher += "\n" + direction + "���� " + directionWELenght + "����..";
            hintText.text = textCaesarCipher;
            Debug.Log(textCaesarCipher);

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
