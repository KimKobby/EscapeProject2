using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace NPC
{
    public class EnemyStunTime : MonoBehaviour
    {
        public int stunTime = 0;


        public void OnTriggerEnter(Collider other)  
        {
            Weapon weapon = other.transform.root.GetComponent<Weapon>();
            Debug.Log(other.gameObject.name + "Ʈ���Ű� �����Ǿ����ϴ�.");
            Debug.Log(other.transform.root.GetComponent<Weapon>());
            if (weapon != null && weapon.isGrab)
            {
                Debug.Log(other.gameObject.name + "�� �¾ҽ��ϴ�.");
                stunTime += weapon.stunDamage; // �� ����Ÿ�ӿ� ���� ��������ŭ �߰�
                Debug.Log("���� Ÿ��" +  stunTime);
                other.gameObject.SetActive(false);  // �����ı�
            }
        }

    }
}
