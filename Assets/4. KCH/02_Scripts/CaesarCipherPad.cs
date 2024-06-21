using Changhoon;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Changhoon
{
    public class CaesarCipherPad : MonoBehaviour
    {
        private readonly Dictionary<char, int> alphabetMap = new Dictionary<char, int>()
    {
        {'A', 1}, {'B', 2}, {'C', 3}, {'D', 4}, {'E', 5}, {'F', 6}, {'G', 7}, {'H', 8}, {'I', 9},
        {'J', 10}, {'K', 11}, {'L', 12}, {'M', 13}, {'N', 14}, {'O', 15}, {'P', 16}, {'Q', 17},
        {'R', 18}, {'S', 19}, {'T', 20}, {'U', 21}, {'V', 22}, {'W', 23}, {'X', 24}, {'Y', 25}, {'Z', 26}
    };

        private string convertPassword = "";  // 숫자로 변환된 암호 입력 값
        [SerializeField]private TMP_Text padText;
        private string inputText;

        private string startStr = "P A S S  W O R D";
        private string wrongStr = "W A R N I G";

        void Start()
        {
            padText.text = startStr;
        }

        private void OnInputNumber(string str)
        {

            if (padText.text == startStr)
            {
                padText.text = null;
            }
            else if (padText.text == wrongStr)
            {
                padText.text = null;
            }
            inputText += str;
            padText.text = inputText;
        }

        private void ConvertTextToNumber()
        {
            string numberText = "";
            
            if (inputText == null)
            {
                padText.text = wrongStr;
            }
            else
            {
                foreach (char c in inputText)
                {
                    if (alphabetMap.ContainsKey(c))
                    {
                        int charValue = alphabetMap[c];
                        numberText += charValue.ToString();
                    }
                    else
                    {
                        numberText += c;
                    }
                }
            }
            

            padText.text = numberText;
            convertPassword = numberText;
        }

        private void CheckPassword()
        {
            ConvertTextToNumber();
            if (convertPassword == Changhoon.CaesarCipher.Instance.password)
            {
                Debug.Log("오픈");
            }
            else
            {
                Debug.Log("땡");
                inputText = "";
                padText.text = wrongStr;
            }
        }



        //private void Update()
        //{
        //    if (Input.GetMouseButtonDown(2))
        //    {

        //        CheckPassword();


        //    }
        //}
    }
}

