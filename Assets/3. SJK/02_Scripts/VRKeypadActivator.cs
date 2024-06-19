using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace NavKeypad
{
    public class VRKeypadActivator : XRGrabInteractable
    {
        public VRKeypad vrKeypad; // VRKeypad ��ũ��Ʈ�� ������ ����

        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            base.OnSelectEntered(args);
            // Ű�е带 �������� �� VRKeypad�� ToggleKeypad �޼��带 ȣ���մϴ�.
            vrKeypad.ToggleKeypad();
        }
    }
}
