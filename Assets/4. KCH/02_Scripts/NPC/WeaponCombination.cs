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
        private bool isCombiantion = false;
        private List<GameObject> combinationBox = new List<GameObject>(); // ���� �ڽ� ����Ʈ
        private List<GameObject> objsBox = new List<GameObject>(); // ���� �ڽ� ��ü��

        [SerializeField] private GameObject ingredientSocket;  // ���� ĭ ���ϸ���
        [SerializeField] private GameObject combiantionSocket;  // ���� �� ����



        // ���� ���� ������ ����Ʈ�� �߰�
        public void WeaponAddList(SelectEnterEventArgs args)
        {
            if (isCombiantion == false)
            {
                combinationBox.Add(args.interactableObject.transform.gameObject);
                //Debug.Log(args.interactableObject + "_ADD");
            }
        }


        // ���� ���� ������ ����Ʈ�� ����
        public void WeaponRemoveList(SelectExitEventArgs args)
        {
            if (isCombiantion == false)
            {
                combinationBox.Remove(args.interactableObject.transform.gameObject);
                //Debug.Log(args.interactableObject + "_Remove");
            }
        }

        public void OffCombinationUI()
        {
            GameObject gameObject = this.gameObject;
            gameObject.SetActive(false);
        }

        // ���� ���� ���
        public void Combination()
        {
            int combinationDamage = 0;  // ���յ����� 0 ���� �ʱ�ȭ

            if (combinationBox.Count > 0 && isCombiantion == false)  // ���� �ڽ��� ��ڽ��� �ƴϰ� ���� ������ false �϶� ����
            {
                isCombiantion = true;

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
                OnCombiantionSocket();
                CreateCombinationWeapon(combinationDamage);
                ingredientSocket.SetActive(!isCombiantion);  // ���� ����ĭ�� ����

            }
            else
            {
                Debug.Log("���� �� ���Ⱑ ���ų� �̹� ������ �߽��ϴ�.");
            }
        }

        // ���� �ϼ� ���� ĭ ���� ���
        private void OnCombiantionSocket()
        {

            combiantionSocket.SetActive(isCombiantion);  // ���� �� ���� ���� ���� �ѱ�
            combiantionSocket.GetComponent<XRSocketInteractor>().interactionLayers = InteractionLayerMask.GetMask("Weapon");
        }

        // �� ���� ���� ���� ���
        private void CreateCombinationWeapon(int _combinationDamage)
        {
            // ù��°�� ���� ���⸦ �������� �ν��Ͻ�
            GameObject combinationWeapon = Instantiate(combinationBox[Random.Range(0, combinationBox.Count)], combiantionSocket.transform.position, Quaternion.identity);

            AddEmission(combinationWeapon);

            if (combinationWeapon != null)
            {
                // ���չ����� stunDamage ���� combinationDamage�� ����
                combinationWeapon.transform.gameObject.GetComponent<Weapon>().stunDamage = _combinationDamage;
            }
            else
            {
                Debug.Log("���� �� ���Ⱑ ��������� �ʾҽ��ϴ�.");
            }
            RemoveObjBox();


        }

        // �� ���� ���⿡ �߱� �߰�
        private void AddEmission(GameObject _combinationWeapon)
        {
            // RIgidbody �� ����
            _combinationWeapon.transform.GetComponent<Rigidbody>().angularDrag = 0.05f;
            _combinationWeapon.transform.GetComponent<Rigidbody>().useGravity = true;
            _combinationWeapon.transform.GetComponent<Rigidbody>().isKinematic = false;

            // ����� ��Ƽ���� ������
            Material newMat = _combinationWeapon.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material;

            // ��Ƽ���� ����
            newMat.EnableKeyword("_EMISSION");
            //newMat.SetTexture("_EmissionMap", newMat.GetTexture("_BaseMap"));
            //newMat.SetColor("_EmissionColor", new Color(190f, 80f, 50f));
            //newMat.SetFloat("_EmissionIntensity", 1f);



            // ����� ��Ƽ���� ����
            _combinationWeapon.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = newMat;
        }

        // ���� ���� �� ���� ����
        private void RemoveObjBox()
        {
            for (int i = 0; i < objsBox.Count; i++)
            {
                objsBox[i].SetActive(false);
            }
            objsBox.Clear();
        }
    }
}

