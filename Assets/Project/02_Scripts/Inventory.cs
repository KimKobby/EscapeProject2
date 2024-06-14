using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Inventory : MonoBehaviour
{
    public void AddInventory(SelectEnterEventArgs args)
    {
        // 'args'���� ���õ� ���ͷ��ͺ� ������Ʈ�� ������
        GameObject inItem = args.interactableObject.transform.gameObject;

        // ���� ������Ʈ(this)�� �ڽ����� 'inItem'�� ���� 
        inItem.transform.SetParent(this.transform);


    }
}