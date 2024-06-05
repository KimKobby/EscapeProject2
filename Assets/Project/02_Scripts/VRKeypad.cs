using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace NavKeypad
{
    public class VRKeypad : MonoBehaviour
    {
        public TMP_Text inputText; // 입력 값을 표시할 TMP Text 요소
        public GameObject canvasUI; // UI Canvas 오브젝트를 참조할 변수
        public GameObject lockedDoor; // 잠겨있는 문 오브젝트를 참조할 변수
        public Password passwordScript; // Password 스크립트를 참조할 변수
        private string currentInput = ""; // 현재 입력 값

        // 숫자 버튼을 눌렀을 때 호출되는 메서드
        public void PressNumber(string number)
        {
            currentInput += number; // 현재 입력 값에 숫자를 추가
            inputText.text = currentInput; // TMP Text에 현재 입력 값을 표시
        }

        // OK 버튼을 눌렀을 때 호출되는 메서드
        public void CheckPassword()
        {
            if (currentInput == passwordScript.GetPassword())
            {
                Debug.Log("암호가 일치합니다!");
                // 암호가 일치하는 경우 처리할 내용을 여기에 추가하세요.

                // UI Canvas 비활성화
                canvasUI.SetActive(false);

                // 잠긴 문 열기
                lockedDoor.SetActive(false);
            }
            else
            {
                Debug.Log("암호가 일치하지 않습니다.");
                // 암호가 일치하지 않는 경우 처리할 내용을 여기에 추가하세요.
            }

            // 입력 값을 초기화합니다.
            currentInput = "";
            inputText.text = currentInput;
        }
    }
}
