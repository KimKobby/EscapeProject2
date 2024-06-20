using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenClose1 : MonoBehaviour
{
    [SerializeField] public GameObject GrabInteractor;
    [SerializeField] public float default_pos;
    [SerializeField] public float default_rot;

    [SerializeField] public float change_rot_x;
    [SerializeField] public float change_rot_y;
    [SerializeField] public float change_rot_z;
    [SerializeField] public float change_pos_x;
    [SerializeField] public float change_pos_y;
    [SerializeField] public float change_pos_z;

    public Quaternion startRotation;
    public Quaternion endRotation;

    public Vector3 startPosition;
    public Vector3 endPosition;

    [SerializeField] private bool isRot;
    [SerializeField] private bool isPos;

    private bool isOpen;

    public bool isSolution = false;

    private bool firstTimeOpened = false;

    public PsychoPathNPC psychoPathNPC;

    [SerializeField] private float elapsedTime = 0.0f;
    private float durationTime = 1f;

    void Start()
    {
        startPosition = this.transform.position;
        startRotation = this.transform.rotation;
        Vector3 eulerRotation = startRotation.eulerAngles;

        endRotation = Quaternion.Euler(eulerRotation.x + change_rot_x, eulerRotation.y + change_rot_y, eulerRotation.z + change_rot_z);
        endPosition = new Vector3(startPosition.x + change_pos_x, startPosition.y + change_pos_y, startPosition.z + change_pos_z);
    }

    void Update()
    {
        if (isRot)
        {
            if (!isOpen)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / durationTime);
                transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
                transform.position = Vector3.Lerp(startPosition, endPosition, t);
                if (t >= 1f)
                {
                    isOpen = true;
                    isRot = false;
                    elapsedTime = 0f;
                    if (!firstTimeOpened)
                    {
                        firstTimeOpened = true;
                        psychoPathNPC.OnFirstDoorOpened();
                    }
                }
            }
            else
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / durationTime);
                transform.rotation = Quaternion.Lerp(endRotation, startRotation, t);

                if (t >= 1f)
                {
                    isOpen = false;
                    isRot = false;
                    elapsedTime = 0f;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isSolution)
        {
            if (other.CompareTag("Hand"))
            {
                Debug.Log("Player 충돌");
                isRot = true;
            }
        }
        else
        {
            // 잠긴 사운드
        }
    }

    public void SetLock(bool _lock)
    {
        isSolution = _lock;
        Debug.Log("LOCK 풀림");
    }
}
