using UnityEngine;
using UnityEngine.XR;

public class CanvasFollowHMD : MonoBehaviour
{
    public Transform hmdTransform; // HMD�� Ʈ������
    public Vector3 offset; // HMD�κ����� ������

    void Update()
    {
        // HMD ��ġ�� ���� �̵�
        transform.position = hmdTransform.position + hmdTransform.forward * offset.z + hmdTransform.up * offset.y + hmdTransform.right * offset.x;
        transform.rotation = Quaternion.LookRotation(transform.position - hmdTransform.position);
    }
}
