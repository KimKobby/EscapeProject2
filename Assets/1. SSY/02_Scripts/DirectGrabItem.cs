using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DirectGrabItem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReturnItemSize(SelectEnterEventArgs args)
    {
        if (args.interactableObject.transform.gameObject.CompareTag("Item"))
        {
            Debug.Log(args.interactableObject.transform.GetChild(0).GetChild(0));
            args.interactableObject.transform.GetChild(0).GetChild(0).transform.localScale = new Vector3(1, 1, 1);
        }
    }
    
}
