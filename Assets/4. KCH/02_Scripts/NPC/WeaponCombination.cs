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
        private List<GameObject> combinationBox = new List<GameObject>(); // 조합 박스 리스트
        private List<GameObject> objsBox = new List<GameObject>(); // 조합 박스 대체용

        [SerializeField] private GameObject ingredientSocket;  // 조합 칸 소켓모음
        [SerializeField] private GameObject combiantionSocket;  // 조합 된 소켓



        // 조합 소켓 아이템 리스트에 추가
        public void WeaponAddList(SelectEnterEventArgs args)
        {
            if (isCombiantion == false)
            {
                combinationBox.Add(args.interactableObject.transform.gameObject);
                //Debug.Log(args.interactableObject + "_ADD");
            }
        }


        // 조합 소켓 아이템 리스트에 제거
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
                ingredientSocket.SetActive(!isCombiantion);  // 조합 소켓칸들 끄기

            }
            else
            {
                Debug.Log("조합 할 무기가 없거나 이미 조합을 했습니다.");
            }
        }

        // 조합 완성 소켓 칸 오픈 기능
        private void OnCombiantionSocket()
        {

            combiantionSocket.SetActive(isCombiantion);  // 조합 된 무기 넣을 소켓 켜기
            combiantionSocket.GetComponent<XRSocketInteractor>().interactionLayers = InteractionLayerMask.GetMask("Weapon");
        }

        // 새 조합 무기 생성 기능
        private void CreateCombinationWeapon(int _combinationDamage)
        {
            // 첫번째로 들어온 무기를 메인으로 인스턴스
            GameObject combinationWeapon = Instantiate(combinationBox[Random.Range(0, combinationBox.Count)], combiantionSocket.transform.position, Quaternion.identity);

            AddEmission(combinationWeapon);

            if (combinationWeapon != null)
            {
                // 조합무기의 stunDamage 값을 combinationDamage로 설정
                combinationWeapon.transform.gameObject.GetComponent<Weapon>().stunDamage = _combinationDamage;
            }
            else
            {
                Debug.Log("조합 된 무기가 만들어지지 않았습니다.");
            }
            RemoveObjBox();


        }

        // 새 조합 무기에 발광 추가
        private void AddEmission(GameObject _combinationWeapon)
        {
            // RIgidbody 값 설정
            _combinationWeapon.transform.GetComponent<Rigidbody>().angularDrag = 0.05f;
            _combinationWeapon.transform.GetComponent<Rigidbody>().useGravity = true;
            _combinationWeapon.transform.GetComponent<Rigidbody>().isKinematic = false;

            // 대상의 머티리얼 가져옴
            Material newMat = _combinationWeapon.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material;

            // 머티리얼 변경
            newMat.EnableKeyword("_EMISSION");
            //newMat.SetTexture("_EmissionMap", newMat.GetTexture("_BaseMap"));
            //newMat.SetColor("_EmissionColor", new Color(190f, 80f, 50f));
            //newMat.SetFloat("_EmissionIntensity", 1f);



            // 변경된 머티리얼 적용
            _combinationWeapon.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = newMat;
        }

        // 조합 진행 된 무기 제거
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

