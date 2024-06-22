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
            Weapon weapon = other.GetComponent<Weapon>();
            Debug.Log(other.gameObject.name + "Ʈ���Ű� �����Ǿ����ϴ�.");
            Debug.Log(other.gameObject.GetComponent<Weapon>());
            if (other.gameObject.GetComponent<Weapon>() != null && other.gameObject.GetComponent<Weapon>().isGrab)
            {
                Debug.Log(other.gameObject.name + "�� �¾ҽ��ϴ�.");
                stunTime += other.GetComponent<Weapon>().stunDamage; // �� ����Ÿ�ӿ� ���� ��������ŭ �߰�
                Debug.Log("���� Ÿ��" +  stunTime);
                other.gameObject.SetActive(false);  // �����ı�
            }
        }

    }
}
