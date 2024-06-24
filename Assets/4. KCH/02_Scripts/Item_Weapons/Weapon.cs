using NPC;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Weapons
{

    public class Weapon : MonoBehaviour
    {
        public int stunDamage;  // 무기 스턴 데미지
        public bool isGrab = false;  // 플레이어가 잡았는지 확인
        public bool isAttack = false;
       // private XRGrabInteractable grabInteractable;

        public GameObject enemy;



        private void Start()
        {
            //XRGrabInteractable grabInteractable = this.GetComponent<XRGrabInteractable>();
        }

        public void OnGrab()
        {

            StopCoroutine("IsGrabReset");
            isGrab = true;
            Debug.Log(this.gameObject.name + "을 잡았습니다. 상태는" + isGrab);
        }

        
        public void Throw()
        {
            isGrab  = false;    
            //Debug.Log(this.gameObject.name + "을 놓았습니다. 상태는" + isGrab);
            StartCoroutine("IsGrabReset");
        }

        private IEnumerator IsGrabReset()  // 잡은 상태 끄기
        {

            yield return new WaitForSeconds(2f); //  2초후 잡은 상태 끔
            isAttack = false;
            Debug.Log(this.gameObject.name + "놓은 후 무기의 상태는" + isGrab);


        }


        //public void OnCollisionEnter(Collision collision)  // 충돌시
        //{
        //    if (isGrab && collision.gameObject.GetComponent<EnemyStunTime>() != null)
        //    {
        //        enemy.GetComponent<EnemyStunTime>().stunTime += stunDamage; // 적 스턴타임에 스턴 데미지만큼 추가
        //        this.gameObject.SetActive(false);  // 무기 파괴
        //        isGrab = false;
        //    }
        //    else
        //    {
        //        Debug.Log("맞은 대상이 싸이코가 아닙니다.");
        //    }
        //}
/*
        public void Swing(int dmg) // 공격(휘두르기) 기능
        {
            dmg = stunDamage;  // 스턴 데미지
            isGrab = true;
            //StartCoroutine(IsAttackReset());
        }

        public void Throw(int dmg) // 공격(던지기) 기능
        {
            dmg = stunDamage;  // 스턴 데미지
            isGrab = true;
        }
*/
      
    }

}
