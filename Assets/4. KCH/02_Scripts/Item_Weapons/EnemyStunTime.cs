using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Weapons;

namespace NPC
{
    public class EnemyStunTime : MonoBehaviour
    {
        public float stunTime = 0f;
        public GameObject stunTimeUI;
        public TMP_Text timeText;
        private bool isStun = false;
        public Animator enemy_Animation;

        public void OnTriggerEnter(Collider other)  
        {
            Weapon weapon = other.transform.root.GetComponent<Weapon>();
            //Debug.Log(other.gameObject.name + "트리거가 감지되었습니다.");
            if (weapon != null && weapon.isGrab)
            {
                //Debug.Log(other.gameObject.name + "에 맞았습니다.");
                stunTime = weapon.stunDamage; // 적 스턴타임에 스턴 데미지만큼 추가
                enemy_Animation.SetTrigger("Hit");
                StartCoroutine("SetIsStun");
                Debug.Log("스턴 타임" +  stunTime);
                weapon.gameObject.SetActive(false);  // 무기파괴
            }
            else
            {
                Debug.Log("무기류가 아닙니다.");
            }
        }

        IEnumerator SetIsStun()
        {
            yield return new WaitForSeconds(1f);
            enemy_Animation.SetBool("isStun", true);
        }

        private void Update()
        {
            if (stunTime > 0)
            {
                isStun = true;
                stunTimeUI.SetActive(isStun);
                stunTime -= Time.deltaTime;
                string text = ((int)stunTime).ToString() + "초";
                timeText.text = text;
            }
            else if (stunTime <= 0)
            {
                isStun = false;
                enemy_Animation.SetBool("isStun", false);
                stunTimeUI.SetActive(isStun);
            }
        }

    }
}
