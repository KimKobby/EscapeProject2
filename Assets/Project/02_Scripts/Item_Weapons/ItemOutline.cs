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
                // �ʱ�ȭ �ؾ��ϴµ� �ʱ�ȭ�� �ȵ�
                //obj.GetComponent<MeshRenderer>().sharedMaterials = obj.GetComponent<MeshRenderer>().materials;
                Debug.Log(obj.name + "sharedMaterials �迭 ���� =" + obj.GetComponent<MeshRenderer>().sharedMaterials.Length);
                // ���� ���͸��� ��������
                Material[] originMaterials = obj.GetComponent<MeshRenderer>().sharedMaterials;
                Debug.Log(obj.name + "������ �迭 ���� =" + originMaterials.Length);
                // 1ĭ �þ ���͸��� �迭 �����
                Material[] newMaterials = new Material[originMaterials.Length + 1];
                Debug.Log(obj.name + "�� �迭 ���� =" + newMaterials.Length);
                // �� �迭�� ���� �迭 �߰��ϱ�
                originMaterials.CopyTo(newMaterials, 0);
                // 1ĭ �þ �迭�� �ƿ����θ��͸��� �߰��ϱ�
                newMaterials[newMaterials.Length - 1] = outlineMaterial;
                // ���� ���͸��� ���� �߰��� ���͸��� �迭 ����ȭ
                obj.GetComponent<MeshRenderer>().sharedMaterials = newMaterials;
            }
            else
            {
               // obj.AddComponent<Renderer>().material = outlineMaterial;

                Debug.Log(obj.name + "�����ۿ� ����� �������� ���� �߰��߽��ϴ�");
            }
        }
    }
}
