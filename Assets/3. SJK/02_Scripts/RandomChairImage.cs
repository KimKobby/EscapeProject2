using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class RandomChairImage : MonoBehaviour
{
    public GameObject[] chairs; // 의자 객체의 배열
    public Image imageToShow; // 표시할 이미지
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
        StartCoroutine(ShowImage());
    }

    IEnumerator ShowImage()
    {
        // 이미지를 표시
        imageToShow.gameObject.SetActive(true);

        // 2초 대기
        yield return new WaitForSeconds(2);

        // 이미지를 숨김
        imageToShow.gameObject.SetActive(false);
    }
}
