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
        public List<GameObject> combinationBox = new List<GameObject>(); // 조합 박스 
        public List<GameObject> objsBox = new List<GameObject>(); // 조합 박스 대체용


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

        /*
        // 조합 완성 소켓 아이템 제거
        public void TakeOutWeapon(SelectExitEventArgs args)  
        {
            args.interactableObject.transform.gameObject.GetComponent<Rigidbody>().angularDrag = 0.05f;
            args.interactableObject.transform.gameObject.GetComponent<Rigidbody>().useGravity = true;
            args.interactableObject.transform.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
        */


        // 무기 조합 기능
        public void Combination()  
        {
            OnSocket();

            int combinationDamage = 0;  // 조합데미지 0 으로 초기화

            if (combinationBox != null && isCombiantion)  // 조합 박스가 빈박스가 아니고 조합 유무가 true 일때 실행
            {
                isCombiantion = false;

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

                CreateCombinationWeapon(combinationDamage);

            }
            else
            {
                Debug.Log("조합 할 무기가 없습니다.");
            }
        }

        // 조합 완성 소켓 칸 오픈 기능
        private void OnSocket()
        {
            Debug.Log(this.transform.GetChild(1).GetChild(4).gameObject.name);
            
           
            this.transform.GetChild(1).GetChild(4).gameObject.GetComponent<XRSocketInteractor>().interactionLayers = InteractionLayerMask.GetMask("Weapon");
        }


        // 새 조합 무기 생성 기능
        private void CreateCombinationWeapon(int _combinationDamage)  
        {
                // 첫번째로 들어온 무기를 메인으로 인스턴스
                GameObject combinationWeapon = Instantiate(combinationBox[0], transform.GetChild(1).GetChild(4).gameObject.transform.position, Quaternion.identity);

                // RIgidbody 값 설정
                combinationWeapon.GetComponent<Rigidbody>().angularDrag = 0.05f;
                combinationWeapon.GetComponent<Rigidbody>().useGravity = true;
                combinationWeapon.GetComponent<Rigidbody>().isKinematic = false;

                // 대상의 머티리얼 가져옴
                Material newMat = combinationWeapon.GetComponent<MeshRenderer>().material;

                // 머티리얼 변경
                newMat.EnableKeyword("_EMISSION");
                newMat.SetTexture("_EmissionMap", newMat.GetTexture("_BaseMap"));
                newMat.SetColor("_EmissionColor", new Color(190f, 8f, 0f));
                newMat.SetFloat("_EmissionIntensity", 5.5f);

                // 변경된 머티리얼 적용
                combinationWeapon.GetComponent<MeshRenderer>().material = newMat;


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
        }
    }
}

