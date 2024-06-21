using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.XR.CoreUtils;
using UnityEngine;

namespace Item
{
    public class ItemHintOverRap : MonoBehaviour
    {
        private GameObject gameObject;

        private Color color = Color.black;
        private Color targetColor = new Color(0f, 200f, 50f);
        private float minValue = 0f;
        private float maxValue = 1f;

        public bool isFade = false;

        private void start()
        {
            GameObject newobj = Instantiate(gameObject, gameObject.transform.position, Quaternion.identity);
            gameObject = newobj;
            AddHint();
        }

        private void AddHint()
        {
            // 대상의 머티리얼 가져옴
            Material newMat = gameObject.transform.GetComponent<MeshRenderer>().material;

            // 머티리얼 변경
            newMat.EnableKeyword("_EMISSION");
            newMat.SetTexture("_EmissionMap", newMat.GetTexture("_BaseMap"));
            newMat.SetColor("_EmissionColor", color);
            newMat.SetFloat("_EmissionIntensity", minValue);

            // 변경된 머티리얼 추가
            gameObject.transform.GetComponent<MeshRenderer>().AddMaterial(newMat);
        }


        private void Update()
        {
            if (isFade == false)
            {
                gameObject.transform.GetComponent<MeshRenderer>().materials[1].SetColor("_EmissionColor", Color.Lerp(color, targetColor, Time.deltaTime));
                gameObject.transform.GetComponent<MeshRenderer>().materials[1].SetFloat("_EmissionIntensity", Mathf.Lerp(minValue, maxValue, Time.deltaTime));
                isFade = true;
            }
            
            else if(isFade == true)
            {
                gameObject.transform.GetComponent<MeshRenderer>().materials[1].SetColor("_EmissionColor", Color.Lerp(targetColor, color, Time.deltaTime));
                gameObject.transform.GetComponent<MeshRenderer>().materials[1].SetFloat("_EmissionIntensity", Mathf.Lerp(minValue, maxValue, Time.deltaTime));
                isFade = false;
            }
        }

    }
}
