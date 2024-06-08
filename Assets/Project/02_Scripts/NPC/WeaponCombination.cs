using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Weapons;


namespace NPC
{
    public class WeaponCombination : MonoBehaviour
    {
        public List<GameObject> combinationBox = new List<GameObject>(); // ���� �ڽ� 

        public GameObject combinationWeapon;  // ���յ� ����

        public GameObject combinatioWweaponPrefab;  // ���� ���� ������

        public int combinationDamage;  //  ���� ���� ������



        public void Combination()
        {
            combinationDamage = 0;  // ���յ����� 0 ���� �ʱ�ȭ

            for (int i = 0; i < combinationBox.Count; i++)
            {
                Weapon weapon = combinationBox[i].GetComponent<Weapon>();
                if (weapon != null)
                {
                    combinationDamage += weapon.stunDamage;
                }
                else
                {
                    Debug.Log(combinationBox[i].name + "���Ⱑ ����Ŭ������ ��ӹ��� ���߽��ϴ�.");
                }

            }
            CreateWeaponFromPrefab();
            NewCombinationWeapon();

        }

        private void NewCombinationWeapon()
        {
            combinationWeapon = new GameObject("CombinedWeapon"); // ���ο� GameObject ����
            combinationWeapon = combinatioWweaponPrefab;
            Weapon combinedWeapon = combinationWeapon.AddComponent<Weapon>(); // Weapon ������Ʈ �߰�
            combinedWeapon.stunDamage = combinationDamage;  // stunDamage ���� combinationDamage�� ����

        }


        //--------------------------------------------------------------------------------------------------------------
        
       

        void CreateWeaponFromPrefab()  // ������ �ν��Ͻ�ȭ
        {
            GameObject weaponInstance = Instantiate(combinatioWweaponPrefab, transform.parent);

            // �ʿ��� ���, weaponInstance�� ����Ͽ� �߰� ������ �� �� �ֽ��ϴ�.
            // ���� ���, ������ ������Ʈ�� Weapon ������Ʈ�� �ִٸ� ������ ���� ������ �� �ֽ��ϴ�.
            Weapon weaponComponent = weaponInstance.GetComponent<Weapon>();
            if (weaponComponent != null)
            {
                // ���⼭ weaponComponent�� �Ӽ��� ������ �� �ֽ��ϴ�.
                weaponComponent.stunDamage = combinationDamage; // ���÷� ���յ� ������ ���� ����
            }
        }
    }
}

