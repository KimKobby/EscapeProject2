using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Item;

namespace Weapons
{
    public class WeaponManager : MonoBehaviour
    {
        [SerializeField] // 근접무기 리스트
        private List<GameObject> meleeWeapons = new List<GameObject>();

        [SerializeField] // 포션무기 리스트
        private List<GameObject> potionWeapons = new List<GameObject>();

        [SerializeField]  // 초반 무기 생성 영역
        private GameObject spawnArea;
        private Vector3 spawnPosition;

        private void Start()
        {
            AddOutlineInList();
            GEN_RandomWeapon();
        }

        private void GEN_Area()  // 생성 영역
        {
            // 큐브 오브젝트 내부의 랜덤 위치 계산
            spawnPosition = spawnArea.transform.position + new Vector3(
                Random.Range(-spawnArea.transform.localScale.x / 2f, spawnArea.transform.localScale.x / 2f),
                Random.Range(-spawnArea.transform.localScale.y / 2f, spawnArea.transform.localScale.y / 2f),
                Random.Range(-spawnArea.transform.localScale.z / 2f, spawnArea.transform.localScale.z / 2f)
            );
        }

        // 랜덤으로 무기 생성 기능
        private void GEN_RandomWeapon()
        {
            foreach (GameObject weaponInList in meleeWeapons)
            {
                GEN_Area();
                weaponInList.SetActive(Random.value > 0f);
                GameObject weapon = Instantiate(weaponInList, spawnPosition, Quaternion.identity);
            }
        }

        // 무기 리스트에 있는 오브젝트에 아웃라인 추가
        private void AddOutlineInList()
        {
            ItemOutline itemOutline = new ItemOutline();

            if (meleeWeapons.Count > 0)
            {
                for (int i = 0; i < meleeWeapons.Count; i++)
                {
                    if (meleeWeapons[i] != null)
                    {
                        itemOutline.AddOutlineShader(meleeWeapons[i]);
                    }
                }
            }
            else
            {
                Debug.Log("근접무기 리스트가 비었습니다. 확인하세요");
            }

            if (potionWeapons.Count > 0)
            {
                for (int i = 0; i < potionWeapons.Count; i++)
                {
                    if (potionWeapons[i] != null)
                    {
                        itemOutline.AddOutlineShader(potionWeapons[i]);
                    }
                }
            }
            else
            {
                Debug.Log("포션무기 리스트가 비었습니다. 확인하세요");
            }
        }
    }
}