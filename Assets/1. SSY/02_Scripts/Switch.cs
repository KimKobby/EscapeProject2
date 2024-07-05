using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;


public interface ISwitchable
{
    public bool IsActive { get;}

    public void Activate();
    public void Deactivate();
}




public class Switch : MonoBehaviour
{
    public ISwitchable ct;

    public void Toggle()
    {
        if (ct.IsActive)
        {
            ct.Deactivate();
        }
        else
        {
            ct.Activate();
        }
    }

}