using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{

    public class Weapon : MonoBehaviour
    {
        public int stunDamage;  // 무기 스턴 데미지
        private bool isAttack = false;  // 공격중인지 확인

        public NPC.EnemyStunTime enemy;  // 적 스크립트

        public void Swing(int dmg) // 공격(휘두르기) 기능
        {
            dmg = stunDamage;  // 스턴 데미지
            isAttack = true;
            //StartCoroutine(IsAttackReset());
        }

        public void Throw(int dmg) // 공격(던지기) 기능
        {
            dmg = stunDamage;  // 스턴 데미지
            isAttack = true;
        }


        private IEnumerator IsAttackReset()  // 공격중인 상태 끄기
        {
            yield return new WaitForSeconds(1f);
            isAttack = false;
        }


        public void OnCollisionEnter(Collision collision)  // 충돌시
        {
            if (isAttack && collision.gameObject.CompareTag("Enemy") && collision.gameObject.layer == LayerMask.NameToLayer("NPC"))  // 공격중 && 적태그 && NPC레이어일 경우
            {
                enemy.stunTime += stunDamage; // 적 스턴타임에 스턴 데미지만큼 추가
                this.gameObject.SetActive(false);  // 무기 파괴
                isAttack = false;
            }
            else
            {
                isAttack = false ;
            }
        }

        /*
                public void OnTriggerEnter(Collider other)  // 충돌시
                {
                    if (other.gameObject.CompareTag("Enemy") && other.gameObject.layer == LayerMask.NameToLayer("NPC"))
                    {
                        enemy.stunTime += stunDamage; // 적 스턴타임에 스턴 데미지만큼 추가
                        this.gameObject.SetActive(false);  // 무기파괴
                    }
                }
        */
    }

}
