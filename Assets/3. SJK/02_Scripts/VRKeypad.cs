using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace NavKeypad
{
    public class VRKeypad : MonoBehaviour
    {
        public TMP_Text inputText; // 입력 값을 표시할 TMP Text 요소
        public TMP_Text resultText; // 결과 메시지를 표시할 TMP Text 요소
        public GameObject canvasUI; // UI Canvas 오브젝트를 참조할 변수
        public GameObject[] lockedDoors; // 잠겨있는 문 오브젝트를 참조할 변수
        public Password passwordScript; // Password 스크립트를 참조할 변수
        public AudioSource audioSource; // 효과음을 재생할 AudioSource
        public AudioClip buttonPressSound; // 버튼 클릭 효과음
        public AudioClip correctSound; // 정답 효과음
        public AudioClip incorrectSound; // 오답 효과음
        private string currentInput = ""; // 현재 입력 값

       
        void Start()
        {
            // 각 변수들이 제대로 할당되었는지 확인합니다.
            if (inputText == null)
                Debug.LogError("inputText가 할당되지 않았습니다.");
            if (resultText == null)
                Debug.LogError("resultText가 할당되지 않았습니다.");
            if (canvasUI == null)
                Debug.LogError("canvasUI가 할당되지 않았습니다.");
            if (passwordScript == null)
                Debug.LogError("passwordScript가 할당되지 않았습니다.");
            if (lockedDoors == null || lockedDoors.Length == 0)
                Debug.LogError("lockedDoors가 할당되지 않았거나 비어 있습니다.");
            if (audioSource == null)
                Debug.LogError("audioSource가 할당되지 않았습니다.");
            if (buttonPressSound == null || correctSound == null || incorrectSound == null)
                Debug.LogError("효과음이 할당되지 않았습니다.");

            // 초기 상태에서 키패드 UI를 비활성화합니다.
            canvasUI.SetActive(false);
            resultText.text = ""; // 초기 상태에서 결과 메시지를 비웁니다.
        }

        // 키패드 오브젝트를 클릭했을 때 호출되는 메서드
        public void ToggleKeypad()
        {
            // 키패드 UI의 활성/비활성 상태를 토글합니다.
            canvasUI.SetActive(!canvasUI.activeSelf);
        }

        // 숫자 버튼을 눌렀을 때 호출되는 메서드
        public void PressNumber(string number)
        {
            currentInput += number; // 현재 입력 값에 숫자를 추가
            inputText.text = currentInput; // TMP Text에 현재 입력 값을 표시
            PlaySound(buttonPressSound); // 버튼 클릭 효과음 재생
        }

        // OK 버튼을 눌렀을 때 호출되는 메서드
        public void CheckPassword()
        {
            if (passwordScript == null)
            {
                Debug.LogError("passwordScript가 할당되지 않았습니다.");
                return;
            }

            if (currentInput == passwordScript.GetPassword())
            {
                Debug.Log("암호가 일치합니다!");
                resultText.text = "정답입니다!"; // 정답 메시지 표시

                // 정답 효과음 재생 후 문 열기
                StartCoroutine(PlayCorrectSoundAndOpenDoor());
            }
            else
            {
                Debug.Log("암호가 일치하지 않습니다.");
                resultText.text = "비밀번호가 틀렸습니다."; // 틀린 비밀번호 메시지 표시

                // 오답 효과음 재생
                PlaySound(incorrectSound);

                // 1초 후에 결과 메시지를 지우고 입력값 초기화
                StartCoroutine(ClearResultTextAndInputAfterDelay(1f));
            }
        }

        // 정답 효과음 재생 후 문 열기
        private IEnumerator PlayCorrectSoundAndOpenDoor()
        {
            // 정답 효과음 재생
            PlaySound(correctSound);

            // 1초 대기
            yield return new WaitForSeconds(1f);

            // UI Canvas 비활성화
            canvasUI.SetActive(false);

            // 잠긴 문 열기
            foreach (GameObject door in lockedDoors)
            {
                if (door != null)
                {
                    door.SetActive(false);
                }
            }
        }

        // 결과 메시지를 지우고 입력값 초기화하는 코루틴
        private IEnumerator ClearResultTextAndInputAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            resultText.text = ""; // 결과 메시지를 비웁니다.
            ClearInput(); // 입력 값을 초기화합니다.
        }

        // 입력 값을 초기화하는 메서드 (지우기 버튼용)
        public void ClearInput()
        {
            currentInput = "";
            inputText.text = currentInput;
        }

        // 효과음 재생 메서드
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