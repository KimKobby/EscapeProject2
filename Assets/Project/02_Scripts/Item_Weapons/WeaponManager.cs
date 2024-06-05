using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class WeaponManager : Weapon
    {
        [SerializeField]
        private List<GameObject> meleeWeapons = new List<GameObject>();

        [SerializeField]
        private List<GameObject> potionWeapons = new List<GameObject>();




        void test()
        {
            stunDamage = 10;
            Debug.Log(stunDamage);
            int i = 0;
            int s = stunDamage + i;
        }


        void test12()
        {
            Debug.Log(stunDamage);
        }

        private void Start()
        {
            test();
            test12();
        }

    }

}
