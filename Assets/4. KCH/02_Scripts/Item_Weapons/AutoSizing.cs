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
    }

    public void ReSizing(SelectEnterEventArgs args)
    {
        // ���Ͽ� �� ������Ʈ ����
        holdObject = args.interactableObject.transform.GetChild(0).GetChild(0).gameObject;

        // ���Ͽ� �� ������Ʈ ���� �����ϰ� ����
        holdObjectScale = args.interactableObject.transform.GetChild(0).gameObject.transform.localScale.x;

        // ���Ͽ� �� ������Ʈ�� ������ 1�� ��� �ϱ� ���� ��
        restoreScale = 1/holdObjectScale;

        // ���Ͽ� �°� ���
        Vector3 newScale = Vector3.one * restoreScale * socketCollider.size.x * scaleMultiplier;

        holdObject.transform.localScale = newScale;
    }

    public void restoreSizing(SelectExitEventArgs args)
    {
        holdObject.transform.localScale = new Vector3(1, 1, 1);
    }


}
