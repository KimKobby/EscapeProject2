using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class RandomChairImage : MonoBehaviour
{
    public GameObject[] chairs; // ���� ��ü�� �迭
    public Image imageToShow; // ��Ÿ�� �̹���
    private GameObject selectedChair; // ���õ� ����

    void Start()
    {
        // ���� �� �ϳ��� �������� ����
        selectedChair = chairs[Random.Range(0, chairs.Length)];
        Debug.Log("Selected chair: " + selectedChair.name);

        // ���õ� ���ڿ� �̺�Ʈ �ڵ鷯 �߰�
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
        // �̹��� ��Ÿ����
        imageToShow.gameObject.SetActive(true);

        // 2�� ��ٸ���
        yield return new WaitForSeconds(2);

        // �̹��� �����
        imageToShow.gameObject.SetActive(false);
    }
}

