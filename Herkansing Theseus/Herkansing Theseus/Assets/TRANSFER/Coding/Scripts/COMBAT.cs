using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COMBAT : MonoBehaviour {
    RaycastHit hit;
    public float raydis;
    public int attackdamage;
    private Satyr satScript;

    public Transform camPos;
    public bool mayAttack;

    public void Attack () {

        if(mayAttack == true)
        {
            if (Physics.Raycast(camPos.position, camPos.forward, out hit, raydis))
            {
                if (hit.transform.tag == "Enemy")
                {
                    print("Player is Attacking");
                    hit.transform.GetComponent<Satyr>().curHealth -= attackdamage;
					mayAttack = false;
                }
            }
        }
        
    }
}
