using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Changhoon
{
    public class CaesarCipher : MonoBehaviour
    {
        //싱글톤 인스턴스
        private static CaesarCipher instance = null;

        // 싱글톤 인스턴스 반환
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

        // 싱글톤 인스턴스 생성
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        private TMP_Text hintText;

        private List<int> chosenNumbers = new List<int>(); // 랜덤으로 생성된 숫자들
        private const int alphabetLength = 26; // 알파벳 26개
        private int cipherLenght = 4; // 암호 자릿수 
        private string textCaesarCipher; // 표시 할 시저 힌트 문구
        private string direction;  // 동, 서 방향 값
        private int directionWELenght; // 동, 서 이동거리
        private List<int> computeNumber = new List<int>(); // 계산한 정답 숫자들
        public string password { get; private set; } // string으로 바꾼 정답 암호



        private void Start()
        {
            hintText = this.transform.GetChild(0).GetComponent<TMP_Text>();
            CreateCaesarCipher();
        }


        private void RandomNumber()  // 숫자 랜덤 생성 함수
        {

            chosenNumbers.Clear();  // 생성된 랜덤 숫자 리스트 초기화
            for (int i = 0; i < cipherLenght; i++)  // 암호 자릿수 만큼 랜덤 숫자 생성
            {
                int randomNumber;
                do
                {
                    randomNumber = Random.Range(1, alphabetLength + 1); // 1부터 26 사이의 랜덤 숫자 선택
                }
                while (chosenNumbers.Contains(randomNumber)); // 이미 선택된 숫자인지 확인

                chosenNumbers.Add(randomNumber); // 선택된 숫자를 리스트에 추가
            }
        }


        private void Direction()  // 방향 랜덤 생성 함수
        {
            string[] directionWE = { "동쪽", "서쪽" }; // 동,서 쪽 방향
            int directionWEMaxLenght = 6; // 동,서 이동 최대 이동거리
            int randomIndex = Random.Range(0, directionWE.Length);  // 랜덤으로 동쪽인지 서쪽인지 뽑기
            direction = directionWE[randomIndex];
            directionWELenght = Random.Range(1, directionWEMaxLenght + 1);  // 랜덤 방향으로 몇 이동할지 랜덤 뽑기
        }


        private void ConvertPassword()  // 진짜 암호 계산 함수
        {
            password = "";
            computeNumber.Clear();

            for (int i = 0; i < chosenNumbers.Count; i++)
            {
                int temp;
                switch (direction)
                {
                    case "동쪽":
                        temp = chosenNumbers[i] - directionWELenght;
                        if (temp < 1)
                        {
                            temp += alphabetLength;
                        }
                        computeNumber.Add(temp);
                        break;
                    case "서쪽":
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

        private void CreateCaesarCipher() // 시저 암호 랜덤 숫자 조합 함수
        {
            textCaesarCipher = null;  // 시저 암호 힌트 문구 초기화
            RandomNumber();
            Direction();

            for (int i = 0; i < cipherLenght; i++)  // 시저 암호 랜덤 숫자 조합
            {
                textCaesarCipher += chosenNumbers[i].ToString();
                if (i != cipherLenght - 1) // 마지막 숫자 조합 전까지 - 추가
                {
                    textCaesarCipher += " - ";
                }
            }

            // 최종 암호 표시 조합
            textCaesarCipher += "\n" + direction + "으로 " + directionWELenght + "걸음..";
            hintText.text = textCaesarCipher;
            Debug.Log(textCaesarCipher);

            // 진짜 암호 변환
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
