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
        private List<GameObject> combinationBox = new List<GameObject>(); // 조합 박스 
        private List<GameObject> objsBox = new List<GameObject>(); // 조합 박스 대체용

        [SerializeField]
        private GameObject ingredientSocket;
        [SerializeField]
        private GameObject combiantionSocket;



        // 조합 소켓 아이템 리스트에 추가
        public void WeaponAddList(SelectEnterEventArgs args)
        {
            combinationBox.Add(args.interactableObject.transform.gameObject);
            //Debug.Log(args.interactableObject + "_ADD");
        }


        // 조합 소켓 아이템 리스트에 제거
        public void WeaponRemoveList(SelectExitEventArgs args)
        {
            combinationBox.Remove(args.interactableObject.transform.gameObject);
            //Debug.Log(args.interactableObject + "_Remove");
        }


        // 무기 조합 기능
        public void Combination()
        {

            int combinationDamage = 0;  // 조합데미지 0 으로 초기화

            if (combinationBox.Count > 0 && isCombiantion == false)  // 조합 박스가 빈박스가 아니고 조합 유무가 false 일때 실행
            {
                isCombiantion = true;

                for (int i = 0; i < combinationBox.Count; i++)
                {
                    objsBox.Add(combinationBox[i]);  // 대체용 박스에 오브젝트들 넣어주기

                    if (combinationBox[i].GetComponent<Weapon>() != null)
                    {
                        combinationDamage += combinationBox[i].GetComponent<Weapon>().stunDamage;
                    }
                    else
                    {
                        Debug.Log(combinationBox[i].name + "무기가 Weapon클래스를 상속받은 무기가 아닙니다.");
                    }
                }
                OnCombiantionSocket();
                CreateCombinationWeapon(combinationDamage);

            }
            else
            {
                Debug.Log("조합 할 무기가 없습니다.");
            }
        }

        // 조합 완성 소켓 칸 오픈 기능
        private void OnCombiantionSocket()
        {
            combiantionSocket.SetActive(isCombiantion);
            combiantionSocket.GetComponent<XRSocketInteractor>().interactionLayers = InteractionLayerMask.GetMask("Weapon");
        }


        // 새 조합 무기 생성 기능
        private void CreateCombinationWeapon(int _combinationDamage)
        {
            // 첫번째로 들어온 무기를 메인으로 인스턴스
            GameObject combinationWeapon = Instantiate(combinationBox[0], combiantionSocket.transform.position, Quaternion.identity);

            AddEmission(combinationWeapon);

            if (combinationWeapon != null)
            {
                combinationWeapon.GetComponent<Weapon>().stunDamage = _combinationDamage;  // 조합무기의 stunDamage 값을 combinationDamage로 설정

                // 조합 진행 된 무기 제거
                for (int i = 0; i < objsBox.Count; i++)
                {
                    objsBox[i].SetActive(false);
                }
                objsBox.Clear();
            }
            else
            {
                Debug.Log("조합 된 무기가 만들어지지 않았습니다.");
            }

            ingredientSocket.SetActive(!isCombiantion);
        }

        // 새 조합 무기에 발광 추가
        private void AddEmission(GameObject _combinationWeapon)
        {
            // RIgidbody 값 설정
            _combinationWeapon.transform.GetChild(0).GetChild(0).GetComponent<Rigidbody>().angularDrag = 0.05f;
            _combinationWeapon.transform.GetChild(0).GetChild(0).GetComponent<Rigidbody>().useGravity = true;
            _combinationWeapon.transform.GetChild(0).GetChild(0).GetComponent<Rigidbody>().isKinematic = false;

            // 대상의 머티리얼 가져옴
            Material newMat = _combinationWeapon.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material;

            // 머티리얼 변경
            newMat.EnableKeyword("_EMISSION");
            newMat.SetTexture("_EmissionMap", newMat.GetTexture("_BaseMap"));
            newMat.SetColor("_EmissionColor", new Color(190f, 8f, 0f));
            newMat.SetFloat("_EmissionIntensity", 5.5f);

            // 변경된 머티리얼 적용
            _combinationWeapon.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = newMat;
        }
    }
}

