using UnityEngine;

namespace Shim
{
    public class FuseSocket : MonoBehaviour
    {
        public Material greenMaterial; // ��� ��Ƽ����
        public Material redMaterial;   // ������ ��Ƽ����
        private bool fuseInserted = false; // ǻ�� �������� ���Ͽ� ���ԵǾ����� ����

        private void Start()
        {
        }

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

                // ǻ�� ���� �Ҹ� ���
                //PlaySound(insertSound);

                // ��� ǻ� ����Ǿ����� Ȯ��
                //if (AreAllFusesConnected())
                //{
                //    PlaySound(allFusesConnectedSound);
                //}
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Fuse"))
            {
                // ǻ�� �������� ���Ͽ��� ���ŵǾ��� ��
                fuseInserted = false;
                Debug.Log("ǻ� ���ŵǾ����ϴ�.");

                // Ư�� ���Ͽ� ǻ� ���ŵǾ��� �� ����Ʈ ��Ƽ���� ����
                ChangeLightMaterial(redMaterial);

                // ǻ�� ���� �Ҹ� ���
                //PlaySound(removeSound);
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

        //private void PlaySound(AudioClip clip)
        //{
        //    if (audioSource != null && clip != null)
        //    {
        //        audioSource.PlayOneShot(clip);
        //    }
        //}

        //private bool AreAllFusesConnected()
        //{
        //    // ��� FuseSocket ������Ʈ�� ã�� ��� ���Ͽ� ǻ� ����Ǿ����� Ȯ��
        //    FuseSocket[] allFuseSockets = FindObjectsOfType<FuseSocket>();
        //    foreach (FuseSocket socket in allFuseSockets)
        //    {
        //        if (!socket.IsFuseConnected())
        //        {
        //            return false;
        //        }
        //    }
        //    return true;
        //}
    }
}
