using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Shim
{
    public class Password : MonoBehaviour
    {
        public GameObject[] numberObjects; // 숫자 오브젝트 배열
        private List<int> password; // 생성된 암호를 저장할 리스트

        private void Start()
        {
            // 랜덤 시드를 현재 시간으로 초기화합니다.
            Random.InitState(System.DateTime.Now.Millisecond);

            // 암호를 생성합니다.
            GeneratePassword();

            // 생성된 암호를 출력합니다.
            Debug.Log("생성된 암호: " + string.Join("", password));

            // 암호를 순차적으로 활성화하고 비활성화합니다.
            StartCoroutine(DisplayPassword());
        }

        // 암호를 생성하는 함수
        private void GeneratePassword()
        {
            password = new List<int>();

            // 암호의 각 자리에 0부터 9까지의 숫자를 랜덤으로 생성하여 저장합니다.
            while (password.Count < 4)
            {
                int digit = Random.Range(0, 10);
                if (!password.Contains(digit))
                {
                    password.Add(digit);
                }
            }

            // 암호를 정렬합니다.
            password.Sort();
        }

        // 암호를 순차적으로 활성화하고 비활성화하는 코루틴
        IEnumerator DisplayPassword()
        {
            // 암호를 두 번 반복합니다.
            for (int j = 0; j < 2; j++)
            {
                // 각 숫자 오브젝트를 순차적으로 처리합니다.
                for (int i = 0; i < password.Count; i++)
                {
                    // 현재 숫자에 해당하는 오브젝트를 활성화합니다.
                    numberObjects[password[i]].SetActive(true);

                    // 1초 대기합니다.
                    yield return new WaitForSeconds(1f);

                    // 현재 숫자에 해당하는 오브젝트를 비활성화합니다.
                    numberObjects[password[i]].SetActive(false);
                }
            }
        }

    }
}