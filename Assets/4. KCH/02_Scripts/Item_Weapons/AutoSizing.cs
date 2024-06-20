using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AutoSizing : MonoBehaviour
{
    private BoxCollider socketCollider;  //  소켓 BoxCollider
    private GameObject holdObject;  // 리사이징 할 소켓에 들어간 오브젝트
    private float holdObjectScale;  // 소켓에 들어간 오브젝트 원래 스케일값
    private float restoreScale;  // 스케일 1로 계산 하기 위한 값
    private float scaleMultiplier = 0.8f; // 오브젝트 크기 조절 비율

    private void Start()
    {
        socketCollider = this.GetComponent<BoxCollider>();
    }

    public void ReSizing(SelectEnterEventArgs args)
    {
        // 소켓에 들어간 오브젝트 지정
        holdObject = args.interactableObject.transform.GetChild(0).GetChild(0).gameObject;

        // 소켓에 들어간 오브젝트 원래 스케일값 지정
        holdObjectScale = args.interactableObject.transform.GetChild(0).gameObject.transform.localScale.x;

        // 소켓에 들어간 오브젝트를 스케일 1로 계산 하기 위한 값
        restoreScale = 1/holdObjectScale;

        // 소켓에 맞게 계산
        Vector3 newScale = Vector3.one * restoreScale * socketCollider.size.x * scaleMultiplier;

        holdObject.transform.localScale = newScale;
    }

    public void restoreSizing(SelectExitEventArgs args)
    {
        holdObject.transform.localScale = new Vector3(1, 1, 1);
    }


}
