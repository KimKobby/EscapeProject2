using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Item;

namespace Weapons
{
    public class WeaponManager : MonoBehaviour
    {
        [SerializeField] // �������� ����Ʈ
        private List<GameObject> meleeWeapons = new List<GameObject>();

        [SerializeField] // ���ǹ��� ����Ʈ
        private List<GameObject> potionWeapons = new List<GameObject>();

        [SerializeField]  // �ʹ� ���� ���� ����
        private GameObject spawnArea;
        private Vector3 spawnPosition;

        private void Start()
        {
            AddOutlineInList();
            GEN_RandomWeapon();
        }

        private void GEN_Area()  // ���� ����
        {
            // ť�� ������Ʈ ������ ���� ��ġ ���
            spawnPosition = spawnArea.transform.position + new Vector3(
                Random.Range(-spawnArea.transform.localScale.x / 2f, spawnArea.transform.localScale.x / 2f),
                Random.Range(-spawnArea.transform.localScale.y / 2f, spawnArea.transform.localScale.y / 2f),
                Random.Range(-spawnArea.transform.localScale.z / 2f, spawnArea.transform.localScale.z / 2f)
            );
        }

        // �������� ���� ���� ���
        private void GEN_RandomWeapon()
        {
            foreach (GameObject weaponInList in meleeWeapons)
            {
                GEN_Area();
                weaponInList.SetActive(Random.value > 0f);
                GameObject weapon = Instantiate(weaponInList, spawnPosition, Quaternion.identity);
            }
        }

        // ���� ����Ʈ�� �ִ� ������Ʈ�� �ƿ����� �߰�
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
                Debug.Log("�������� ����Ʈ�� ������ϴ�. Ȯ���ϼ���");
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
                Debug.Log("���ǹ��� ����Ʈ�� ������ϴ�. Ȯ���ϼ���");
            }
        }
    }
}