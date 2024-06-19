using UnityEngine;

namespace Shim
{
    public class FuseSocket : MonoBehaviour
    {
        public Material greenMaterial; // 녹색 머티리얼
        public Material redMaterial;   // 빨간색 머티리얼
        private bool fuseInserted = false; // 퓨즈 아이템이 소켓에 삽입되었는지 여부

        private void Start()
        {
        }

        // 퓨즈가 연결되었는지 여부를 반환하는 메서드
        public bool IsFuseConnected()
        {
            return fuseInserted;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Fuse"))
            {
                // 퓨즈 아이템이 소켓에 삽입되었을 때
                fuseInserted = true;
                Debug.Log("퓨즈가 연결되었습니다.");

                // 특정 소켓에 퓨즈가 삽입되었을 때 라이트 머티리얼 변경
                ChangeLightMaterial(greenMaterial);
                Debug.Log("Material이 green으로 변경됩니다.");

                // 퓨즈 삽입 소리 재생
                //PlaySound(insertSound);

                // 모든 퓨즈가 연결되었는지 확인
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
                // 퓨즈 아이템이 소켓에서 제거되었을 때
                fuseInserted = false;
                Debug.Log("퓨즈가 제거되었습니다.");

                // 특정 소켓에 퓨즈가 제거되었을 때 라이트 머티리얼 변경
                ChangeLightMaterial(redMaterial);

                // 퓨즈 제거 소리 재생
                //PlaySound(removeSound);
            }
        }

        private void ChangeLightMaterial(Material material)
        {
            // 라이트 오브젝트 가져오기
            GameObject lightObject = transform.Find("Light").gameObject;
            Debug.Log("Light 오브젝트 찾기");

            // 라이트 오브젝트의 렌더러 컴포넌트 가져오기
            Renderer lightRenderer = lightObject.GetComponent<Renderer>();

            // 라이트 머티리얼 변경
            lightRenderer.material = material;
            Debug.Log("Material이 변경되었습니다.");
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
        //    // 모든 FuseSocket 오브젝트를 찾고 모든 소켓에 퓨즈가 연결되었는지 확인
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
