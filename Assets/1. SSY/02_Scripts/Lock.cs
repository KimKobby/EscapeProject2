using Song;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

namespace Song
{
    public class Lock : MonoBehaviour
    {
        public int wordCount;
        private Paper paper;

        public GameObject door;
        [SerializeField] private bool b_LockBtn =true;
        private string[] answers = new string[4];

        public string test;

        public Animator animator;
        public AnimationClip animClip;

        public bool b_Open;
        public bool isNumLock;

        private bool b_numlock = true;
        private float timeNumLock = 0f;

        public Animator door_l;
        public Animator door_r;

        public void ClickLockBtn()
        {
            // Debug.Log("ClickLockBtn ");
            b_LockBtn = true;
        }

        private void Awake()
        {
            if(!isNumLock)
            {
                paper = GameObject.FindObjectOfType<Paper>();
            }

          
          
        }

        private void Start()
        {
          // this.transform.rotation = Quaternion.identity;

      
        }

        private void Update()
        {

            
            //if (isNumLock)
                //Debug.Log(answers[0] + answers[1] + answers[2] + answers[3]);
            if (!isNumLock)
            {
                    Debug.Log(isNumLock);
                    Debug.Log("테스트");
                    Debug.Log("answer = " + paper.GetWord());
                    //버튼 클릭했을때
                    //if (b_LockBtn)
                    //{

                    string s = "";


                    for (int i = 0; i < 4; ++i)
                    {
                        s += answers[i];
                    }

                    Debug.Log("S : " + s);

                    if (s == paper.GetWord())
                    {
                        b_Open = true;
                    }

                    //풀렸을 때
                    if (b_Open)
                    {

                        animator.SetTrigger("IsOpen");
                        b_Open = false;
                        b_LockBtn = false;
                    isNumLock = true;
                    }
                    else
                    {
                        b_LockBtn = false;
                        s = "";

                    }

                //}

            }
            else
            {
                if(b_numlock)
                {
                    string s = "";


                    for (int i = 0; i < 4; ++i)
                    {
                        s += answers[i];
                    }


                    if (s == "2107")
                    {
                        timeNumLock += Time.deltaTime;

                        if(timeNumLock >= 1)
                        {
                            b_Open = true;

                        }

                    }
                    else
                    {
                        timeNumLock = 0f;
                    }

                    //풀렸을 때
                    if (b_Open)
                    {

                        animator.SetTrigger("IsOpen");
                       
                        b_Open = false;
                        b_LockBtn = false;
                        b_numlock = false;
                    }
                    else
                    {
                        b_LockBtn = false;
                        s = "";

                    }

                }

            }




        }
        private void OnTriggerStay(Collider other)
        {
            if (other.transform.parent.name == "weel_01")
            {
                answers[0] = other.gameObject.name[0].ToString();
            }

            if (other.transform.parent.name == "weel_02")
            {
                answers[1] = other.gameObject.name[0].ToString();

            }

            if (other.transform.parent.name == "weel_03")
            {
                answers[2] = other.gameObject.name[0].ToString();

            }

            if (other.transform.parent.name == "weel_04")
            {
                answers[3] = other.gameObject.name[0].ToString();
            }


        }

        public void EndAnimEvent()
        {
            door.GetComponent<OpenClose>().SetLock(true);
            this.gameObject.SetActive(false);
        }


        public void OpenAnim()
        {
            this.gameObject.SetActive(false);
            door_l.SetTrigger("IsOpen");
            door_r.SetTrigger("IsOpen");
        }

    }

}
