using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;
using Weapons;


namespace NPC
{
    public class WeaponCombination : MonoBehaviour
    {
        private bool isCombiantion = true;
        public List<GameObject> combinationBox = new List<GameObject>(); // ���� �ڽ� 
        public List<GameObject> objsBox = new List<GameObject>(); // ���� �ڽ� ��ü��


        // ���� ���� ������ ����Ʈ�� �߰�
        public void WeaponAddList(SelectEnterEventArgs args) 
        {
            combinationBox.Add(args.interactableObject.transform.gameObject);
            //Debug.Log(args.interactableObject + "_ADD");
        }


        // ���� ���� ������ ����Ʈ�� ����
        public void WeaponRemoveList(SelectExitEventArgs args)
        {
            combinationBox.Remove(args.interactableObject.transform.gameObject);
            //Debug.Log(args.interactableObject + "_Remove");
        }

        /*
        // ���� �ϼ� ���� ������ ����
        public void TakeOutWeapon(SelectExitEventArgs args)  
        {
            args.interactableObject.transform.gameObject.GetComponent<Rigidbody>().angularDrag = 0.05f;
            args.interactableObject.transform.gameObject.GetComponent<Rigidbody>().useGravity = true;
            args.interactableObject.transform.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
        */


        // ���� ���� ���
        public void Combination()  
        {
            OnSocket();

            int combinationDamage = 0;  // ���յ����� 0 ���� �ʱ�ȭ

            if (combinationBox != null && isCombiantion)  // ���� �ڽ��� ��ڽ��� �ƴϰ� ���� ������ true �϶� ����
            {
                isCombiantion = false;

                for (int i = 0; i < combinationBox.Count; i++)
                {
                    objsBox.Add(combinationBox[i]);  // ��ü�� �ڽ��� ������Ʈ�� �־��ֱ�

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

        // ���� �ϼ� ���� ĭ ���� ���
        private void OnSocket()
        {
            Debug.Log(this.transform.GetChild(1).GetChild(4).gameObject.name);
            
           
            this.transform.GetChild(1).GetChild(4).gameObject.GetComponent<XRSocketInteractor>().interactionLayers = InteractionLayerMask.GetMask("Weapon");
        }


        // �� ���� ���� ���� ���
        private void CreateCombinationWeapon(int _combinationDamage)  
        {
                // ù��°�� ���� ���⸦ �������� �ν��Ͻ�
                GameObject combinationWeapon = Instantiate(combinationBox[0], transform.GetChild(1).GetChild(4).gameObject.transform.position, Quaternion.identity);

                // RIgidbody �� ����
                combinationWeapon.GetComponent<Rigidbody>().angularDrag = 0.05f;
                combinationWeapon.GetComponent<Rigidbody>().useGravity = true;
                combinationWeapon.GetComponent<Rigidbody>().isKinematic = false;

                // ����� ��Ƽ���� ������
                Material newMat = combinationWeapon.GetComponent<MeshRenderer>().material;

                // ��Ƽ���� ����
                newMat.EnableKeyword("_EMISSION");
                newMat.SetTexture("_EmissionMap", newMat.GetTexture("_BaseMap"));
                newMat.SetColor("_EmissionColor", new Color(190f, 8f, 0f));
                newMat.SetFloat("_EmissionIntensity", 5.5f);

                // ����� ��Ƽ���� ����
                combinationWeapon.GetComponent<MeshRenderer>().material = newMat;


                if (combinationWeapon != null)
                {
                    combinationWeapon.GetComponent<Weapon>().stunDamage = _combinationDamage;  // ���չ����� stunDamage ���� combinationDamage�� ����

                    // ���� ���� �� ���� ����
                    for (int i = 0; i < objsBox.Count; i++)
                    {
                        objsBox[i].SetActive(false);
                    }
                    objsBox.Clear();
                }
                else
                {
                    Debug.Log("���� �� ���Ⱑ ��������� �ʾҽ��ϴ�.");
                }
        }
    }
}

