using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

namespace Song
{
    public class Paper : MonoBehaviour
    {
        private TMP_Text[] tmp_txts = new TMP_Text[4];
        
        private string[] words = { "FACE", "ADDB", "CABE", "DEBA", "ABEF" };
        public TMP_Text[] hidden_txts = new TMP_Text[4];

        private int wordidx;

        public string GetWord()
        {
          return words[wordidx];
        }
        

        void Awake()
        {
    
        }

        // Start is called before the first frame update
        void Start()
        {
            SetPaperNum();

            SetHiddenNum();

            
        }

        private void SetPaperNum()
        {
            TMP_Text[] tmpTexts = GetComponentsInChildren<TMP_Text>();

            HashSet<string> usedRooms = new HashSet<string>();

            for (int i = 0; i < 4; ++i)
            {
                int randomFloorIdx = UnityEngine.Random.Range(1, 3);
                int randomRoomIdx = UnityEngine.Random.Range(1, 4);
                

                ////3������
                //string roomKey = randomFloorIdx.ToString() + "," + randomRoomIdx.ToString();

                //������ ����
                string roomKey =  randomRoomIdx.ToString();

                // �ߺ��� ���� ȣ���� ��� �ٽ� �������� ����
                while (usedRooms.Contains(roomKey))
                {
                    //randomFloorIdx = UnityEngine.Random.Range(1, 3);
                    randomRoomIdx = UnityEngine.Random.Range(1, 4);
                   // roomKey = randomFloorIdx.ToString() + "," + randomRoomIdx.ToString();
                    roomKey =  randomRoomIdx.ToString();
                }

                // ���� ���� ȣ���� ǥ���ϰ� HashSet�� �߰�
                tmpTexts[i].text = roomKey;
                usedRooms.Add(roomKey);
            }

        }

        private void SetHiddenNum()
        {
          
            wordidx = UnityEngine.Random.Range(0, 5);

            for (int i = 0;i < 4; ++i)
            {
                hidden_txts[i].text = words[wordidx][i].ToString();
                    
            }


        }


        // Update is called once per frame
        void Update()
        {

        }
    }


}