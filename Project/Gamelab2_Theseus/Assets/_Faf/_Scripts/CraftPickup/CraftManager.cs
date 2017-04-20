using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftManager : MonoBehaviour {

    public float pickupFlower;
    public float pickupSeaWeed;
    public float healthPotion; // weet niet zeker waar de healthpotions komen?

    public float checkCounter;
    public bool mayCraft;

	void Start ()
    {
        mayCraft = false;
	}
	void Update ()
    {
        if (pickupFlower > checkCounter && pickupSeaWeed > checkCounter)
        {
            mayCraft = true;
        }

        if (Input.GetButtonDown("ALIEKE MAG KIEZEN"))
        {

            // hier moet menutje open gaan en dan een leuk knopje met craft 
            if(mayCraft == true)
            {
                Craft();
            }            
        }      
    }

    void Craft()
    {
        pickupFlower -= checkCounter;
        pickupSeaWeed -= checkCounter;
        healthPotion += 1f;

    }
}
