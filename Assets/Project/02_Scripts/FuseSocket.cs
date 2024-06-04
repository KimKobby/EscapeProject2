using UnityEngine;

namespace Shim
{
    public class FuseSocket : MonoBehaviour
    {
        public Material greenMaterial; // ��� ��Ƽ����
        public Material redMaterial;   // ������ ��Ƽ����
        private bool fuseInserted = false; // ǻ�� �������� ���Ͽ� ���ԵǾ����� ����

        // ǻ� ����Ǿ����� ���θ� ��ȯ�ϴ� �޼���
        public bool IsFuseConnected()
        {
            return fuseInserted;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Fuse"))
            {
                // ǻ�� �������� ���Ͽ� ���ԵǾ��� ��
                fuseInserted = true;
                Debug.Log("ǻ� ����Ǿ����ϴ�.");

                // Ư�� ���Ͽ� ǻ� ���ԵǾ��� �� ����Ʈ ��Ƽ���� ����
                ChangeLightMaterial(greenMaterial);
                Debug.Log("Material�� green���� ����˴ϴ�.");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Fuse"))
            {
                // ǻ�� �������� ���Ͽ��� ���ŵǾ��� ��
                fuseInserted = false;
                Debug.Log("Material�� red�� ����˴ϴ�.");

                // Ư�� ���Ͽ� ǻ� ���ŵǾ��� �� ����Ʈ ��Ƽ���� ����
                ChangeLightMaterial(redMaterial);
            }
        }

        private void ChangeLightMaterial(Material material)
        {
            // ����Ʈ ������Ʈ ��������
            GameObject lightObject = transform.Find("Light").gameObject;
            Debug.Log("Light ������Ʈ ã��");

            // ����Ʈ ������Ʈ�� ������ ������Ʈ ��������
            Renderer lightRenderer = lightObject.GetComponent<Renderer>();

            // ����Ʈ ��Ƽ���� ����
            lightRenderer.material = material;
            Debug.Log("Material�� ����Ǿ����ϴ�.");
        }
    }
}
