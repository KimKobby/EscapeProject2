using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Park
{
    public class WallMove : MonoBehaviour
    {
        private Vector3 startPos;

        private Vector3 targetPos;

        public float WallSpeed;

        private float times;//10000フレームつかって移動することにする
        // Start is called before the first frame update
        void Start()
        {
            //壁の初期位置を保存
            startPos = this.transform.localPosition;

            //壁の到着する目標とする座標
            targetPos = new Vector3(-5.23f, 0.16f, -4.85f);

            times = 0.0f;
        }

        // Update is called once per frame
        void Update()
        {
            times += 0.001f * WallSpeed;
            //Debug.Log(times);

            if (times <= 1)
            {
                this.transform.localPosition = Vector3.Lerp(startPos, targetPos, times);
            }
        }
    }
}