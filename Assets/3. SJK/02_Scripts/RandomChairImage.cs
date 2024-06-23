using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class RandomChairImage : MonoBehaviour
{
    public GameObject[] chairs; // 의자 객체의 배열
    public Canvas canvasToShow; // 표시할 캔버스
    public Transform hmdTransform; // HMD의 트랜스폼
    public Vector3 canvasOffset = new Vector3(0, 0, 1); // HMD로부터의 오프셋
    private GameObject selectedChair; // 선택된 의자

    void Start()
    {
        // 의자 중 하나를 무작위로 선택
        selectedChair = chairs[Random.Range(0, chairs.Length)];
        Debug.Log("Selected chair: " + selectedChair.name);

        // 선택된 의자에 이벤트 리스너 추가
        XRGrabInteractable grabInteractable = selectedChair.GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.onSelectEntered.AddListener(OnChairGrabbed);
        }
    }

    void OnChairGrabbed(XRBaseInteractor interactor)
    {
        StartCoroutine(ShowCanvas());
    }

    IEnumerator ShowCanvas()
    {
        // 캔버스를 HMD 앞에 배치
        PositionCanvasInFrontOfHMD();

        // 캔버스를 표시
        canvasToShow.gameObject.SetActive(true);

        // 2초 대기
        yield return new WaitForSeconds(2);

        // 캔버스를 숨김
        canvasToShow.gameObject.SetActive(false);
    }

    private void PositionCanvasInFrontOfHMD()
    {
        // HMD 앞에 캔버스 위치 설정
        canvasToShow.transform.position = hmdTransform.position + hmdTransform.forward * canvasOffset.z + hmdTransform.up * canvasOffset.y + hmdTransform.right * canvasOffset.x;
        canvasToShow.transform.rotation = Quaternion.LookRotation(canvasToShow.transform.position - hmdTransform.position);
    }
}
