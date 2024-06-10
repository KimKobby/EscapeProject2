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
        private List<GameObject> combinationBox = new List<GameObject>(); // ���� �ڽ� 

        public void WeaponAddList(SelectEnterEventArgs args)  // ���� ������ �߰�
        {
            combinationBox.Add(args.interactableObject.transform.gameObject);
        }

        public void WeaponRemoveList(SelectExitEventArgs args)  // ���� ������ ����
        {
            combinationBox.Remove(args.interactableObject.transform.gameObject);
        }



        public void Combination()  // ���� ���� ���
        {
            int combinationDamage = 0;  // ���յ����� 0 ���� �ʱ�ȭ

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
                        Debug.Log(combinationBox[i].name + "���Ⱑ WeaponŬ������ ��ӹ��� ���Ⱑ �ƴմϴ�.");
                    }
                }
                CreateCombinationWeapon(combinationDamage);
            }
            else
            {
                Debug.Log("���� �� ���Ⱑ �����ϴ�.");
            }
        }

        private void CreateCombinationWeapon(int _combinationDamage)  // �� ���� ���� ���� ���
        {  
            GameObject combinationWeapon = Instantiate(combinationBox[0], transform.GetChild(1).GetChild(4).gameObject.transform.position, Quaternion.identity);  // ù��°�� ���� ���⸦ �������� �ν��Ͻ�

            if (combinationWeapon != null)
            {
                combinationWeapon.GetComponent<Weapon>().stunDamage = _combinationDamage;  // ���չ����� stunDamage ���� combinationDamage�� ����
                for (int i = 0; i < combinationBox.Count; i++)
                {
                    Debug.Log(combinationBox[i].name);
                    combinationBox[i].SetActive(false);
                }
            }
            else
            {
                Debug.Log("���յ� ���Ⱑ ��������� �ʾҽ��ϴ�.");
            }
        }
    }
}

