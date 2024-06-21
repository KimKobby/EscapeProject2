using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomXRDirectInteractor : XRBaseControllerInteractor
{
    // Start is called before the first frame update
    void Start()
    {
        this.selectEntered.AddListener(Resize);
    }

    void Resize(SelectEnterEventArgs args)
    {
        if(args.interactorObject.transform.gameObject.CompareTag("Item"))
        {
            args.interactableObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
