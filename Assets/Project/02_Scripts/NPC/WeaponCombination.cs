using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Weapons;


namespace NPC
{
    public class WeaponCombination : MonoBehaviour
    {
        private List<GameObject> combinationBox = new List<GameObject>(); // 조합 박스 

        public void WeaponAddList(SelectEnterEventArgs args)  // 소켓 아이템 추가
        {
            combinationBox.Add(args.interactableObject.transform.gameObject);
        }

        public void WeaponRemoveList(SelectExitEventArgs args)  // 소켓 아이템 제거
        {
            combinationBox.Remove(args.interactableObject.transform.gameObject);
        }



        public void Combination()  // 무기 조합 기능
        {
            int combinationDamage = 0;  // 조합데미지 0 으로 초기화

            if (combinationBox != null)
            {
                for (int i = 0; i < combinationBox.Count; i++)
                {
                    if (combinationBox[i].GetComponent<Weapon>() != null)
                    {
                        combinationDamage += combinationBox[i].GetComponent<Weapon>().stunDamage;
                    }
                    else
                    {
                        Debug.Log(combinationBox[i].name + "무기가 Weapon클래스를 상속받은 무기가 아닙니다.");
                    }
                }
                CreateCombinationWeapon(combinationDamage);
            }
            else
            {
                Debug.Log("조합 할 무기가 없습니다.");
            }
        }

        private void CreateCombinationWeapon(int _combinationDamage)  // 새 조합 무기 생성 기능
        {  
            GameObject combinationWeapon = Instantiate(combinationBox[0], transform.GetChild(1).GetChild(4).gameObject.transform.position, Quaternion.identity);  // 첫번째로 들어온 무기를 메인으로 인스턴스

            if (combinationWeapon != null)
            {
                combinationWeapon.GetComponent<Weapon>().stunDamage = _combinationDamage;  // 조합무기의 stunDamage 값을 combinationDamage로 설정
                for (int i = 0; i < combinationBox.Count; i++)
                {
                    Debug.Log(combinationBox[i].name);
                    combinationBox[i].SetActive(false);
                }
            }
            else
            {
                Debug.Log("조합된 무기가 만들어지지 않았습니다.");
            }
        }
    }
}

