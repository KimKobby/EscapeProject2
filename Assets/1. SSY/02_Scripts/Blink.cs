using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Song
{
    public class Blink : MonoBehaviour
    {
        private Light lampLight;
        private float blinksec = 2f;
        private float time;
        private int randomrange;



        // Start is called before the first frame update
        void Start()
        {
            lampLight = this.gameObject.transform.GetChild(0).GetComponent<Light>();
            time = blinksec;
            randomrange = Random.Range(2, 5);

        }

        // Update is called once per frame
        void Update()
        {
            time += Time.deltaTime;

            if (time <= 0.3)
            {
                lampLight.intensity = 10;
            }
            else if (time > 0.3 && time <= 0.35)
            {
                lampLight.intensity = 1;
            }
            else if (time > 0.35 && time <= 0.4)
            {
                lampLight.intensity = 5;
            }
            else if (time > 0.4 && time <= 0.45)
            {
                lampLight.intensity = 1;
            }
            else if (time > 0.45 && time <= 0.5)
            {
                lampLight.intensity = 5;
            }
            else if (time > 0.5 && time <= randomrange)
            {
                lampLight.intensity = 10;
            }
            else
            {
                randomrange = Random.Range(2, 5);
                time = 0f;
            }
        }
    }

}
