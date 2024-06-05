using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NavKeypad
{
    public class Password : MonoBehaviour
    {
        public GameObject[] numberObjects; // 0-9 숫자를 표시할 GameObject 배열
        public GameObject potionObject; // 충돌을 감지할 포션 오브젝트
        private List<int> password; // 생성된 암호를 저장할 리스트

        private void Start()
        {
            Random.InitState(System.DateTime.Now.Millisecond); // 난수 초기화
            GeneratePassword(); // 암호 생성
            Debug.Log("생성된 암호: " + string.Join("", password)); // 생성된 암호 출력
        }

        // 암호 생성 메서드
        private void GeneratePassword()
        {
            password = new List<int>();

            for (int i = 0; i < 4; i++)
            {
                int digit = Random.Range(0, 10); // 0-9 사이의 랜덤 숫자 생성
                password.Add(digit); // 리스트에 추가
            }
        }

        // 포션 충돌 감지 메서드
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == potionObject)
            {
                StartCoroutine(DisplayPassword()); // 암호 표시 코루틴 시작
            }
        }

        // 암호를 표시하는 코루틴
        private IEnumerator DisplayPassword()
        {
            for (int j = 0; j < 2; j++)
            {
                for (int k = 0; k < 2; k++)
                {
                    for (int i = 0; i < password.Count; i++)
                    {
                        int digit = password[i]; // 현재 위치의 암호 숫자를 가져옴
                        GameObject numberObject = numberObjects[digit]; // 해당 숫자에 대응하는 GameObject를 가져옴

                        numberObject.SetActive(true); // 해당 숫자를 활성화
                        yield return new WaitForSeconds(1f); // 1초 대기
                        numberObject.SetActive(false); // 해당 숫자를 비활성화
                    }
                }

                // 한 번의 반복이 끝날 때마다 1초의 딜레이를 줍니다.
                yield return new WaitForSeconds(1f);
            }
        }

        // 사용자 입력 처리 메서드
        public void AddInput(string input)
        {
            Debug.Log("입력된 값: " + input);
            if (input == string.Join("", password))
            {
                Debug.Log("암호가 일치합니다!");
                // 암호 일치 시 수행할 동작 추가
            }
            else
            {
                Debug.Log("암호가 일치하지 않습니다.");
                // 암호 불일치 시 수행할 동작 추가
            }
        }

        // 암호를 반환하는 메서드
        public string GetPassword()
        {
            return string.Join("", password);
        }
    }
}
