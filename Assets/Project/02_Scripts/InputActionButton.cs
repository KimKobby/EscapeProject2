using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputActionButton : MonoBehaviour
{
    public InputActionAsset asset;

    void Update()
    {
        var a = asset.actionMaps[8].actions[0].ReadValue<float>();
        var b = asset.actionMaps[8].actions[1].ReadValue<float>();
        var x = asset.actionMaps[8].actions[2].ReadValue<float>();
        var y = asset.actionMaps[8].actions[3].ReadValue<float>();

        Debug.Log(string.Format("{0} / {1} / {2} / {3}", a, b, x, y));
    }

}