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
        public bool onAttack = false;  // 무기 공격 상태

        public void OnGrab()  // 잡았을 때
        {
            StopCoroutine("IsGrabReset");
            isGrab = true;
            onAttack = true;
            //Debug.Log(this.gameObject.name + "을 잡았습니다. 공격 상태는" + onAttack);
        }

        public void Throw()  // 놓거나 던질때
        {
            isGrab  = false;
            //Debug.Log(this.gameObject.name + "을 놓았습니다. 공격 상태는" + onAttack);
            StartCoroutine("IsGrabReset");
        }

        private IEnumerator IsGrabReset()  // 1초 후 공격 상태 끄기
        {
            yield return new WaitForSeconds(1f);
            onAttack = false;
            //Debug.Log(this.gameObject.name + "놓은 후 무기의 공격 상태는" + onAttack);
        }
    }

}
