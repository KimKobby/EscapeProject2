using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class RandomChairImage : MonoBehaviour
{
    public GameObject[] chairs;       // 의자 객체의 배열
    public Image imageToShow;         // 표시할 이미지
    public AudioClip soundToPlay;     // 재생할 효과음
    private AudioSource audioSource;  // 오디오 소스
    private GameObject selectedChair; // 선택된 의자

    void Start()
    {
        // AudioSource 컴포넌트 추가 또는 가져오기
        audioSource = gameObject.AddComponent<AudioSource>();

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
        Debug.Log("이미지가 나왔습니다");

        // 효과음 재생
        if (soundToPlay != null && audioSource != null)
        {
            audioSource.PlayOneShot(soundToPlay);
        }

        // 2초 대기
        yield return new WaitForSeconds(2);

        // 이미지를 숨김
        imageToShow.gameObject.SetActive(false);
    }
}
