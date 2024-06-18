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


        [SerializeField] private bool b_LockBtn;
        private string[] answers = new string[4];

        public string test;

        public Animator animator;

        public bool b_Open;



        public void ClickLockBtn()
        {
            // Debug.Log("ClickLockBtn ");
            b_LockBtn = true;
        }

        private void Awake()
        {
            paper = GameObject.FindObjectOfType<Paper>();

        }


        private void Update()
        {
            //  Debug.Log("answer = " + paper.GetWord());


            //버튼 클릭했을때
            if (b_LockBtn)
            {

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
                }
                else
                {
                    b_LockBtn = false;
                    s = "";

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





    }

}
