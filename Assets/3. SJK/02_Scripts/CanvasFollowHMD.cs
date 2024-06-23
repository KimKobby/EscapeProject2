using UnityEngine;
using UnityEngine.XR;

public class CanvasFollowHMD : MonoBehaviour
{
    public Transform hmdTransform; // HMD의 트랜스폼
    public Vector3 offset; // HMD로부터의 오프셋

    void Update()
    {
        // HMD 위치를 따라 이동
        transform.position = hmdTransform.position + hmdTransform.forward * offset.z + hmdTransform.up * offset.y + hmdTransform.right * offset.x;
        transform.rotation = Quaternion.LookRotation(transform.position - hmdTransform.position);
    }
}
