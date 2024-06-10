using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons.MeleeWeapons
{

    public class MeleeWeapon : Weapon
    {

        
        private void Start()
        {

        }


        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Swing(stunDamage);

            }
        }

    }

}
