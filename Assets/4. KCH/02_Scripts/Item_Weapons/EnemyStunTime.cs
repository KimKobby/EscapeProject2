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
            Debug.Log(other.gameObject.name + "트리거가 감지되었습니다.");
            Debug.Log(other.transform.root.GetComponent<Weapon>());
            if (weapon != null && weapon.isGrab)
            {
                Debug.Log(other.gameObject.name + "에 맞았습니다.");
                stunTime += weapon.stunDamage; // 적 스턴타임에 스턴 데미지만큼 추가
                Debug.Log("스턴 타임" +  stunTime);
                other.gameObject.SetActive(false);  // 무기파괴
            }
        }

    }
}
