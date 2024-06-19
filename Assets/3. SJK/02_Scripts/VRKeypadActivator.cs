using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace NavKeypad
{
    public class VRKeypadActivator : XRGrabInteractable
    {
        public VRKeypad vrKeypad; // VRKeypad 스크립트를 참조할 변수

        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            base.OnSelectEntered(args);
            // 키패드를 선택했을 때 VRKeypad의 ToggleKeypad 메서드를 호출합니다.
            vrKeypad.ToggleKeypad();
        }
    }
}
