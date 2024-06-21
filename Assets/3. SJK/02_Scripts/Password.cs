using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NavKeypad
{
    public class Password : MonoBehaviour
    {
        public GameObject[] numberObjects; // 0-9 숫자를 표시할 GameObject 배열
        public GameObject[] potionObjects; // 충돌을 감지할 포션 오브젝트
        public AudioClip collisionSound; // 충돌 소리
        private AudioSource audioSource; // 오디오 소스
        private List<int> password; // 생성된 암호를 저장할 리스트

        private void Start()
        {
            // 난수 초기화
            Random.InitState(System.DateTime.Now.Millisecond);
            // 암호 생성
            GeneratePassword();
            // 생성된 암호 출력
            Debug.Log("생성된 암호: " + string.Join("", password));

            // 오디오 소스 컴포넌트를 가져오거나 없으면 추가
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }

        // 암호 생성 메서드 (중복을 허용하지 않는 난수 방식)
        private void GeneratePassword()
        {
            password = new List<int>();

            // 중복을 허용하지 않는 난수 생성
            while (password.Count < 4)
            {
                int digit = Random.Range(0, 10); // 0-9 사이의 랜덤 숫자 생성

                // 리스트에 없는 경우에만 추가
                if (!password.Contains(digit))
                {
                    password.Add(digit); // 리스트에 추가
                }
            }
        }

        // 포션 충돌 감지 메서드
        private void OnTriggerEnter(Collider other)
        {
            foreach ( var potionObject in potionObjects)
            {

                if (other.gameObject == potionObject)
                {
                    // 포션 오브젝트 비활성화
                    potionObject.SetActive(false);

                    // 충돌 소리 재생
                    if (collisionSound != null)
                    {
                        AudioSource.PlayClipAtPoint(collisionSound, transform.position);
                    }

                    // 암호 표시 코루틴 시작
                    StartCoroutine(DisplayPassword());
                }
            }
        }

        // 암호를 표시하는 코루틴
        private IEnumerator DisplayPassword()
        {
            for (int repeat = 0; repeat < 2; repeat++)
            {
                for (int i = 0; i < password.Count; i++)
                {
                    int digit = password[i]; // 현재 위치의 암호 숫자를 가져옴
                    GameObject numberObject = numberObjects[digit]; // 해당 숫자에 대응하는 GameObject를 가져옴

                    numberObject.SetActive(true); // 해당 숫자를 활성화
                    yield return new WaitForSeconds(1f); // 1초 대기
                    numberObject.SetActive(false); // 해당 숫자를 비활성화
                }

                // 한 번의 반복이 끝날 때마다 1초의 딜레이를 줍니다.
                yield return new WaitForSeconds(1f);
            }
        }

        // 암호를 반환하는 메서드
        public string GetPassword()
        {
            return string.Join("", password);
        }
    }
}
