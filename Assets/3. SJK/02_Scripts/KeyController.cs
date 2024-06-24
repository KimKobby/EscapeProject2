using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class KeyController : MonoBehaviour
{
    public GameObject[] keys; // 여러 열쇠 오브젝트
    public Image ghostImage; // 귀신 이미지가 포함된 UI 오브젝트
    public AudioClip ghostSound; // 효과음 오디오 클립
    private AudioSource audioSource;

    void Start()
    {
        ghostImage.enabled = false; // 처음에는 귀신 이미지 숨김
        audioSource = GetComponent<AudioSource>();

        // 모든 열쇠 오브젝트에 이벤트 리스너 추가
        foreach (GameObject key in keys)
        {
            XRGrabInteractable grabInteractable = key.GetComponent<XRGrabInteractable>();
            if (grabInteractable != null)
            {
                grabInteractable.selectEntered.AddListener(OnKeyGrabbed);
            }
        }
    }

    void OnDestroy()
    {
        // 모든 열쇠 오브젝트에서 이벤트 리스너 제거
        foreach (GameObject key in keys)
        {
            XRGrabInteractable grabInteractable = key.GetComponent<XRGrabInteractable>();
            if (grabInteractable != null)
            {
                grabInteractable.selectEntered.RemoveListener(OnKeyGrabbed);
            }
        }
    }

    void OnKeyGrabbed(SelectEnterEventArgs args)
    {
        // 랜덤으로 열쇠 선택
        int randomIndex = Random.Range(0, keys.Length);
        GameObject selectedKey = keys[randomIndex];

        if (args.interactableObject.transform.gameObject == selectedKey)
        {
            StartCoroutine(ShowGhost());
        }
    }

    IEnumerator ShowGhost()
    {
        // 귀신 이미지와 효과음 재생
        ghostImage.enabled = true;
        audioSource.PlayOneShot(ghostSound);

        yield return new WaitForSeconds(2);

        ghostImage.enabled = false;
    }
}
