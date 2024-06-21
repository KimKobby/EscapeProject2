using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AutoSizing : MonoBehaviour
{
    private BoxCollider socketCollider;  //  ���� BoxCollider
    private GameObject holdObject;  // ������¡ �� ���Ͽ� �� ������Ʈ
    private float holdObjectScale;  // ���Ͽ� �� ������Ʈ ���� �����ϰ�
    private float restoreScale;  // ������ 1�� ��� �ϱ� ���� ��
    private float scaleMultiplier = 0.8f; // ������Ʈ ũ�� ���� ����


    private void Start()
    {
        socketCollider = this.GetComponent<BoxCollider>();
        this.GetComponent<CustomInventorySocketInteractor>().selectEntered.AddListener(ReSizing);
       // this.GetComponent<CustomInventorySocketInteractor>().selectExited.AddListener(restoreSizing);
    }

    public void ReSizing(SelectEnterEventArgs args)
    {
        // ���Ͽ� �� ������Ʈ ����
        holdObject = args.interactableObject.transform.GetChild(0).GetChild(0).gameObject;

        // ���Ͽ� �� ������Ʈ ���� �����ϰ� ����
        holdObjectScale = args.interactableObject.transform.GetChild(0).gameObject.transform.localScale.x;

        // ���Ͽ� �� ������Ʈ�� ������ 1�� ��� �ϱ� ���� ��
        restoreScale = 1/holdObjectScale;

        Vector3 newScale = new Vector3();
        if (restoreScale >= 1)
        {
            newScale = Vector3.one * socketCollider.size.x * scaleMultiplier * restoreScale;
        }
        else if (restoreScale < 1)
        {
            //if (socketCollider.size.x > restoreScale
            newScale = Vector3.one * socketCollider.size.x * scaleMultiplier * holdObjectScale;
        }

        holdObject.transform.localScale = newScale;
    }

    public void restoreSizing(SelectExitEventArgs args)
    {

       // holdObject.transform.localScale = new Vector3(1, 1, 1);
    }


}
