using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Weapons;


namespace NPC
{
    public class WeaponCombination : MonoBehaviour
    {
        public List<GameObject> combinationBox = new List<GameObject>(); // 조합 박스 

        public GameObject combinationWeapon;  // 조합된 무기

        public GameObject combinatioWweaponPrefab;  // 조합 무기 프리팹

        public int combinationDamage;  //  조합 스턴 데미지



        public void Combination()
        {
            combinationDamage = 0;  // 조합데미지 0 으로 초기화

            for (int i = 0; i < combinationBox.Count; i++)
            {
                Weapon weapon = combinationBox[i].GetComponent<Weapon>();
                if (weapon != null)
                {
                    combinationDamage += weapon.stunDamage;
                }
                else
                {
                    Debug.Log(combinationBox[i].name + "무기가 무기클래스를 상속받지 못했습니다.");
                }

            }
            CreateWeaponFromPrefab();
            NewCombinationWeapon();

        }

        private void NewCombinationWeapon()
        {
            combinationWeapon = new GameObject("CombinedWeapon"); // 새로운 GameObject 생성
            combinationWeapon = combinatioWweaponPrefab;
            Weapon combinedWeapon = combinationWeapon.AddComponent<Weapon>(); // Weapon 컴포넌트 추가
            combinedWeapon.stunDamage = combinationDamage;  // stunDamage 값을 combinationDamage로 설정

        }


        //--------------------------------------------------------------------------------------------------------------
        
       

        void CreateWeaponFromPrefab()  // 프리팹 인스턴스화
        {
            GameObject weaponInstance = Instantiate(combinatioWweaponPrefab, transform.parent);

            // 필요한 경우, weaponInstance를 사용하여 추가 설정을 할 수 있습니다.
            // 예를 들어, 생성된 오브젝트에 Weapon 컴포넌트가 있다면 다음과 같이 접근할 수 있습니다.
            Weapon weaponComponent = weaponInstance.GetComponent<Weapon>();
            if (weaponComponent != null)
            {
                // 여기서 weaponComponent의 속성을 설정할 수 있습니다.
                weaponComponent.stunDamage = combinationDamage; // 예시로 조합된 데미지 값을 설정
            }
        }
    }
}

