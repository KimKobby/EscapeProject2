using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class KeyController : MonoBehaviour
{
    public GameObject[] keys; // ���� ���� ������Ʈ
    public Image ghostImage; // �ͽ� �̹����� ���Ե� UI ������Ʈ
    public AudioClip ghostSound; // ȿ���� ����� Ŭ��
    private AudioSource audioSource;

    void Start()
    {
        ghostImage.enabled = false; // ó������ �ͽ� �̹��� ����
        audioSource = GetComponent<AudioSource>();

        // ��� ���� ������Ʈ�� �̺�Ʈ ������ �߰�
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
        // ��� ���� ������Ʈ���� �̺�Ʈ ������ ����
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
        // �������� ���� ����
        int randomIndex = Random.Range(0, keys.Length);
        GameObject selectedKey = keys[randomIndex];

        if (args.interactableObject.transform.gameObject == selectedKey)
        {
            StartCoroutine(ShowGhost());
        }
    }

    IEnumerator ShowGhost()
    {
        // �ͽ� �̹����� ȿ���� ���
        ghostImage.enabled = true;
        audioSource.PlayOneShot(ghostSound);

        yield return new WaitForSeconds(2);

        ghostImage.enabled = false;
    }
}
