using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftManager : MonoBehaviour {

    public int pickupFlower;
    public int pickupSeaWeed;
    public int healthyHerb; // weet niet zeker waar de healthpotions komen?

    public int checkCounter;
    public bool mayCraft;

    private bool panelActive;

	void Start ()
    {
        mayCraft = false;
	}

	void Update ()
    {
        if (pickupFlower >= checkCounter && pickupSeaWeed >= checkCounter)
        {
            mayCraft = true;
        }
        else
        {
            mayCraft = false;
        }

        if (Input.GetButtonDown("Q"))
        {
            if (panelActive)
            {
                UI_Manager.uiManager.craftPanel.SetActive(true);
                panelActive = false;
            }
            else
            {
                UI_Manager.uiManager.craftPanel.SetActive(false);
                panelActive = true;
            }         
        }      
    }

    public void Craft()
    {
        if (mayCraft) { 
            pickupFlower -= checkCounter;
            pickupSeaWeed -= checkCounter;
            healthyHerb++;
            UI_Manager.uiManager.UpdateCraftables(pickupFlower, pickupSeaWeed, healthyHerb);
        }
    }
}
