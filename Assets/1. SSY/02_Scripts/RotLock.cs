using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RotLock : MonoBehaviour
{
    public AnimationClip cp;
    public GameObject door;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerStay(Collider other)
    {
        //키가 들어왔을경우
        if(other.gameObject.layer == 20 && other.gameObject.transform.rotation.eulerAngles.z >= 90)
        {
            
            //열쇠를 돌렸을때
            if (other.gameObject.transform.rotation.eulerAngles.z >= 90)
            {
                Debug.Log("돌렸을때");
                this.GetComponent<Animator>().SetTrigger("IsOpen");
                other.gameObject.SetActive(false);
                //사운드 추가

            }

        }
    }

    private void EndLockEvent()
    {
        door.GetComponent<OpenClose>().SetLock(true);
        this.gameObject.SetActive(false);

    }
}
