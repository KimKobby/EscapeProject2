using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AutoSizing : MonoBehaviour
{
    private BoxCollider socketCollider;
    private GameObject holdObject;
    private float scaleMultiplier = 0.9f; // 오브젝트 크기 조절 비율

    private void Start()
    {
        socketCollider = this.GetComponent<BoxCollider>();
    }

    public void ReSizing(SelectEnterEventArgs args)
    {
        Debug.Log("tor : " + args.interactorObject.transform.name);
        Debug.Log("table : " + args.interactableObject.transform.name);
        //holdObject = args.interactableObject.transform.gameObject;
        //Debug.Log(holdObject.gameObject.name);
        //Debug.Log(holdObject.transform.localScale.x);
        //Debug.Log(holdObject.transform.localScale.y);
        //Debug.Log(holdObject.transform.localScale.z);

        //holdObject.transform.localScale = new Vector3(
        //    (socketCollider.size.x * scaleMultiplier),
        //    (socketCollider.size.y * scaleMultiplier),
        //    (socketCollider.size.z * scaleMultiplier)
        //    );

        Vector3 newScale = Vector3.one * socketCollider.size.x * scaleMultiplier;
        args.interactableObject.transform.GetChild(0).GetChild(0).localScale = newScale;

        //args.interactableObject.transform.gameObject.transform.localScale = holdObject.transform.localScale;

        //Debug.Log(holdObject.transform.localScale.x);
        //Debug.Log(holdObject.transform.localScale.y);
        //Debug.Log(holdObject.transform.localScale.z);
    }

    public void restoreSizing(SelectExitEventArgs args)
    {
        holdObject.transform.localScale = new Vector3(1, 1, 1);
    }


}
