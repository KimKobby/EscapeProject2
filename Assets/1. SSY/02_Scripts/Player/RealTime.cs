using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

namespace Song
{
    public class RealTime : MonoBehaviour
    {
        private GameObject clockObj;

        // Start is called before the first frame update
        void Start()
        {
            //���� �ð��� �����ִ� TextGameObj;

            clockObj = this.transform.GetChild(0).GetChild(0).gameObject;

        }

        // Update is called once per frame
        void Update()
        {

            //�ð� �޾ƿ���(��Ÿ���� ����)
            string now = DateTime.Now.ToString("HH:mm:ss");
            clockObj.GetComponent<TextMeshProUGUI>().text = now;

        }
    }

}

