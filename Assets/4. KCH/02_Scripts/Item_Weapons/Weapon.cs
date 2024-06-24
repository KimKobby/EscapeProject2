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
        public int stunDamage;  // ���� ���� ������
        public bool isGrab = false;  // �÷��̾ ��Ҵ��� Ȯ��
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
            Debug.Log(this.gameObject.name + "�� ��ҽ��ϴ�. ���´�" + isGrab);
        }

        
        public void Throw()
        {
            isGrab  = false;    
            //Debug.Log(this.gameObject.name + "�� ���ҽ��ϴ�. ���´�" + isGrab);
            StartCoroutine("IsGrabReset");
        }

        private IEnumerator IsGrabReset()  // ���� ���� ����
        {

            yield return new WaitForSeconds(2f); //  2���� ���� ���� ��
            isAttack = false;
            Debug.Log(this.gameObject.name + "���� �� ������ ���´�" + isGrab);


        }


        //public void OnCollisionEnter(Collision collision)  // �浹��
        //{
        //    if (isGrab && collision.gameObject.GetComponent<EnemyStunTime>() != null)
        //    {
        //        enemy.GetComponent<EnemyStunTime>().stunTime += stunDamage; // �� ����Ÿ�ӿ� ���� ��������ŭ �߰�
        //        this.gameObject.SetActive(false);  // ���� �ı�
        //        isGrab = false;
        //    }
        //    else
        //    {
        //        Debug.Log("���� ����� �����ڰ� �ƴմϴ�.");
        //    }
        //}
/*
        public void Swing(int dmg) // ����(�ֵθ���) ���
        {
            dmg = stunDamage;  // ���� ������
            isGrab = true;
            //StartCoroutine(IsAttackReset());
        }

        public void Throw(int dmg) // ����(������) ���
        {
            dmg = stunDamage;  // ���� ������
            isGrab = true;
        }
*/
      
    }

}
