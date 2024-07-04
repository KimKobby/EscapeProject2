using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cipher
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

        private int wrongCount;

        [SerializeField] private GameObject doorLock;

        void Start()
        {
            padText.text = startStr;
            doorLock.GetComponent<OpenClose>().isSolution = false;
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
            //Debug.Log(convertPassword + "입력된 패스워드");
            //Debug.Log(Cipher.CaesarCipher.Instance.password + "정답 패스워드");
            if (convertPassword == Cipher.CaesarCipher.Instance.password)
            {
                padText.text = "O P E N";
                StartCoroutine(OpenDoorDelay());
                doorLock.GetComponent<OpenClose>().isSolution = true;
                //Debug.Log("오픈");
            }
            else
            {
                //Debug.Log("땡");
                wrongCount += 1;

                if (wrongCount >= 3)
                {
                    padText.text = "R E S E T";
                    Cipher.CaesarCipher.Instance.CreateCaesarCipher();
                    wrongCount = 0;
                }
                else
                {
                    padText.text = wrongStr;
                }

                inputText = "";
            }
        }

        IEnumerator OpenDoorDelay()
        {
            yield return new WaitForSeconds(1f); // 1초 대기 후 꺼짐
            this.gameObject.SetActive(false);
        }

    }
}

