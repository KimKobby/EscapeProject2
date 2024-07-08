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
        private CapsuleCollider boxCollider;

        private void Start()
        {
            boxCollider = this.gameObject.GetComponent<CapsuleCollider>();
        }

        public void OnTriggerEnter(Collider other)  // ���⿡ �´� ���
        {
            Weapon weapon = other.transform.root.GetComponent<Weapon>();
            //Debug.Log(other.gameObject.name + "�� �¾ҽ��ϴ�.");
            if (weapon != null && weapon.onAttack)
            {
                //Debug.Log(other.gameObject.name + "�� �¾ҽ��ϴ�.");
                stunTime = weapon.stunDamage; // �� ����Ÿ�ӿ� ���� ��������ŭ �߰�
                //Debug.Log("���� Ÿ��" + stunTime);
                enemy_Animation.SetTrigger("Hit");
                StartCoroutine("SetIsStun");
                weapon.gameObject.SetActive(false);  // ���� ��
            }
            else
            {
                //Debug.Log("������� �ƴմϴ�.");
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
                boxCollider.enabled = false;
                isStun = true;
                stunTimeUI.SetActive(isStun);
                stunTime -= Time.deltaTime;
                string text = ((int)stunTime).ToString() + "��";
                timeText.text = text;
            }
            else if (stunTime <= 0)
            {
                boxCollider.enabled = true;
                isStun = false;
                enemy_Animation.SetBool("isStun", false);
                stunTimeUI.SetActive(isStun);
            }
        }

    }
}
