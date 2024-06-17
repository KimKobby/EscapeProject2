using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.XR.CoreUtils;
using UnityEngine;


namespace Item
{
    public class ItemOutline : MonoBehaviour
    {
        public Material outlineMaterial;
        public float outlineScale = 1.1f;
        public Color outlineColor = Color.white;


        private void Start()
        {
            //outlineMaterial = Resources.Load<Material>("ItemOutline");
        }

        public void AddOutlineShader(GameObject obj)
        {
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer != null)
            {
                // 초기화 해야하는데 초기화가 안됨
                //obj.GetComponent<MeshRenderer>().sharedMaterials = obj.GetComponent<MeshRenderer>().materials;
                Debug.Log(obj.name + "sharedMaterials 배열 길이 =" + obj.GetComponent<MeshRenderer>().sharedMaterials.Length);
                // 기존 머터리얼 가져오기
                Material[] originMaterials = obj.GetComponent<MeshRenderer>().sharedMaterials;
                Debug.Log(obj.name + "오리진 배열 길이 =" + originMaterials.Length);
                // 1칸 늘어난 머터리얼 배열 만들기
                Material[] newMaterials = new Material[originMaterials.Length + 1];
                Debug.Log(obj.name + "새 배열 길이 =" + newMaterials.Length);
                // 새 배열에 기존 배열 추가하기
                originMaterials.CopyTo(newMaterials, 0);
                // 1칸 늘어난 배열에 아웃라인머터리얼 추가하기
                newMaterials[newMaterials.Length - 1] = outlineMaterial;
                // 공유 머터리얼에 새로 추가한 머터리얼 배열 동기화
                obj.GetComponent<MeshRenderer>().sharedMaterials = newMaterials;
            }
            else
            {
               // obj.AddComponent<Renderer>().material = outlineMaterial;

                Debug.Log(obj.name + "아이템에 적용된 렌더러가 없어 추가했습니다");
            }
        }
    }
}
