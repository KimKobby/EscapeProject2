using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons.MeleeWeapons
{

    public class Crowbar : Weapon
    {
        

        private void Start()
        {
          stunDamage= 10;

            
        }
        private void Update()
        {
            if(Input.GetMouseButton(0))
            {
                Swing(stunDamage);

            }
        }

    }

}
