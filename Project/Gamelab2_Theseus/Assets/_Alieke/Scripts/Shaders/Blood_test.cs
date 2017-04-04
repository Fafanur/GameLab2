using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood_test : MonoBehaviour {

	void Start () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        RaycastHit hit = new RaycastHit();
        Ray ray = new Ray(collision.contacts[0].point - collision.contacts[0].normal, collision.contacts[0].normal);
        if (Physics.Raycast(ray, out hit))
        {
            print(hit.textureCoord);
        }
    }
}
