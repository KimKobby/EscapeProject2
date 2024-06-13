using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Song
{
    public class ObjectPool : MonoBehaviour
    {

        #region ObjectPool
        private static ObjectPool instance;
        public static ObjectPool Inst

        {
            get
            {

                if (instance == null)
                {
                    var obj = FindObjectOfType<ObjectPool>();

                    if (obj != null)
                        instance = obj;
                    else
                    {
                        var newObj = new GameObject();
                        newObj.AddComponent<ObjectPool>();
                        instance = newObj.GetComponent<ObjectPool>();
                    }
                }
                return instance;

            }
        }

        #endregion

        private Queue<GameObject> _queues = new Queue<GameObject>();
        public GameObject[] alpha;
        public int amount = 26;
        [SerializeField] public GameObject parentTr;
        public int icecount;


        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        void Start()
        {
            CreatePool();
        }


        public Queue<GameObject> queues
        {
            get => _queues;
            set => _queues = value;
        }

        public void CreatePool()
        {
            Debug.Log("CreatePool");
            for (int i = 0; i < amount; ++i)
            {
                var newObj = Instantiate(alpha[i]);
                OnEnqueue(newObj);

            }
        }

        public void OnEnqueue(GameObject _obj)
        {

            Debug.Log("OnEnqueue");

            queues.Enqueue(_obj);
            Vector3 vec_ranpos = RandomVector3(5);
            Quaternion qua_ranrot = RandomQuaternion();
            _obj.transform.SetParent(parentTr.transform); //생성한 오브젝트의 부모 오브젝트
            _obj.transform.position = vec_ranpos;
            _obj.transform.rotation = qua_ranrot;
            _obj.SetActive(false);
        }

        public void OnDequeue()
        {

            if (queues.Count <= 30)
            {
                CreatePool();

            }


            while (queues.Count > 26)
            {
                Debug.Log(queues.Count);

                GameObject obj = queues.Dequeue();
                obj.SetActive(true);
                //return obj;
            }


            //return null;

        }


        private Vector3 RandomVector3(int _value)
        {
            int ranpos_x = Random.Range(-_value, _value);
            int ranpos_y = Random.Range(-_value, _value);
            int ranpos_z = Random.Range(-_value, _value);

            return new Vector3(ranpos_x, ranpos_y, ranpos_z);
        }

        private Quaternion RandomQuaternion()
        {
            int ranRot = Random.Range(0, 360);

            Quaternion randomRotation = Quaternion.Euler(ranRot, ranRot, ranRot);
            return randomRotation;
        }


    }

}
