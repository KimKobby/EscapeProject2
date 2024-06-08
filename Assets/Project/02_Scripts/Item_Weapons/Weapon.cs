using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{

    public class Weapon : MonoBehaviour
    {
        public int stunDamage;  // ���� ���� ������
        private bool isAttack = false;  // ���������� Ȯ��

        public NPC.EnemyStunTime enemy;  // �� ��ũ��Ʈ

        public void Swing(int dmg) // ����(�ֵθ���) ���
        {
            dmg = stunDamage;  // ���� ������
            isAttack = true;
            //StartCoroutine(IsAttackReset());
        }

        public void Throw(int dmg) // ����(������) ���
        {
            dmg = stunDamage;  // ���� ������
            isAttack = true;
        }


        private IEnumerator IsAttackReset()  // �������� ���� ����
        {
            yield return new WaitForSeconds(1f);
            isAttack = false;
        }


        public void OnCollisionEnter(Collision collision)  // �浹��
        {
            if (isAttack && collision.gameObject.CompareTag("Enemy") && collision.gameObject.layer == LayerMask.NameToLayer("NPC"))  // ������ && ���±� && NPC���̾��� ���
            {
                enemy.stunTime += stunDamage; // �� ����Ÿ�ӿ� ���� ��������ŭ �߰�
                this.gameObject.SetActive(false);  // ���� �ı�
                isAttack = false;
            }
            else
            {
                isAttack = false ;
            }
        }

        /*
                public void OnTriggerEnter(Collider other)  // �浹��
                {
                    if (other.gameObject.CompareTag("Enemy") && other.gameObject.layer == LayerMask.NameToLayer("NPC"))
                    {
                        enemy.stunTime += stunDamage; // �� ����Ÿ�ӿ� ���� ��������ŭ �߰�
                        this.gameObject.SetActive(false);  // �����ı�
                    }
                }
        */
    }

}
