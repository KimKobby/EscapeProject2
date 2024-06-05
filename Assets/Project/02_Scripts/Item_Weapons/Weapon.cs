using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{

    public class Weapon : MonoBehaviour
    {
        public int stunDamage;
        public bool isUse = true;

        public GameObject enemy;

        public void attack(int _test) 
        {
            _test = stunDamage;
           enemy.GetComponent<enemy>().hp -= stunDamage;
           isUse = false;
        }

    }

}
