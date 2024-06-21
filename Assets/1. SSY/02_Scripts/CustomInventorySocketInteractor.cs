using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomInventorySocketInteractor : XRSocketInteractor
{
    // Start is called before the first frame update
    void Start()
    {
        this.selectEntered.AddListener(InventorySocketInItem);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InventorySocketInItem(SelectEnterEventArgs args)
    {

       
        args.interactableObject.transform.SetParent(this.gameObject.transform.parent.parent.parent, true);

    }



}
