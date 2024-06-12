using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class WeaponManager : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> meleeWeapons = new List<GameObject>();

        [SerializeField]
        private List<GameObject> potionWeapons = new List<GameObject>();

        [SerializeField]
        private GameObject spawnArea;
        private Vector3 spawnPosition;

        private void Start()
        {
            GEN_RandomWeapon();
        }

        private void GEN_Area()
        {
            // 큐브 오브젝트 내부의 랜덤 위치 계산
            spawnPosition = spawnArea.transform.position + new Vector3(
                Random.Range(-spawnArea.transform.localScale.x / 2f, spawnArea.transform.localScale.x / 2f),
                Random.Range(-spawnArea.transform.localScale.y / 2f, spawnArea.transform.localScale.y / 2f),
                Random.Range(-spawnArea.transform.localScale.z / 2f, spawnArea.transform.localScale.z / 2f)
            );
        }

        private void GEN_RandomWeapon()
        {
            foreach (GameObject weaponInList in meleeWeapons)
            {
                GEN_Area();
                weaponInList.SetActive(Random.value > 0f);
                GameObject weapon = Instantiate(weaponInList, spawnPosition, Quaternion.identity);
            }
        }
    }
}