using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenClose : MonoBehaviour
{
     [SerializeField] public GameObject GrabInteractor;
    [SerializeField] public float default_rot;

    [SerializeField] public float change_rot;

    public Quaternion startRotation;
    public Quaternion endRotation;
    [SerializeField] private bool isRot;

    private bool isOpen;


    [SerializeField] private float elapsedTime = 0.0f;
    private float durationTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
      
        startRotation = this.transform.rotation;
        Vector3 eulerRotation = startRotation.eulerAngles;

        endRotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y + change_rot, eulerRotation.z);
      
    }

    // Update is called once per frame
    void Update()
    {
        if(isRot)
        {

            if(!isOpen)
            {
                //this.transform.GetComponent<Collider>().enabled = false;  
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / durationTime);
                transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);

                if (t >= 1f)
                {
                    isOpen = true;
                    isRot = false;
                    elapsedTime = 0f;
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
        if(other.CompareTag("Hand"))
        {
            
            Debug.Log("Player Ãæµ¹");
            isRot = true;
          
        }

    }
}
