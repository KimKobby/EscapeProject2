using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Inventory : MonoBehaviour
{
    public void AddInventory(SelectEnterEventArgs args)
    {
        // 'args'에서 선택된 인터랙터블 오브젝트를 가져옴
        GameObject inItem = args.interactableObject.transform.gameObject;

        // 현재 오브젝트(this)의 자식으로 'inItem'을 설정 
        inItem.transform.SetParent(this.transform);


    }
}