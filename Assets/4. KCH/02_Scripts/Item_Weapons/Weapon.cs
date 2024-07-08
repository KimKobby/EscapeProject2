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
        public bool onAttack = false;  // ���� ���� ����

        public void OnGrab()  // ����� ��
        {
            StopCoroutine("IsGrabReset");
            isGrab = true;
            onAttack = true;
            //Debug.Log(this.gameObject.name + "�� ��ҽ��ϴ�. ���� ���´�" + onAttack);
        }

        public void Throw()  // ���ų� ������
        {
            isGrab  = false;
            //Debug.Log(this.gameObject.name + "�� ���ҽ��ϴ�. ���� ���´�" + onAttack);
            StartCoroutine("IsGrabReset");
        }

        private IEnumerator IsGrabReset()  // 1�� �� ���� ���� ����
        {
            yield return new WaitForSeconds(1f);
            onAttack = false;
            //Debug.Log(this.gameObject.name + "���� �� ������ ���� ���´�" + onAttack);
        }
    }

}
