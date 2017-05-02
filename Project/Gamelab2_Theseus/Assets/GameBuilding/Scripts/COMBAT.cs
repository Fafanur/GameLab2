using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COMBAT : MonoBehaviour {
    RaycastHit hit;
    public float raydis;
    public int attackdamage = 10;

    public Transform camPos;


    void Attack () {
        if (Physics.Raycast(camPos.position, camPos.forward, out hit, raydis))
        {
            if (hit.transform.tag == "Enemy")
            {
                hit.transform.GetComponent<SatyrManager>().curHealth -= attackdamage;
            }
        }
    }
}
