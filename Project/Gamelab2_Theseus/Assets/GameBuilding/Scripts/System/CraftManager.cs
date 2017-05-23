using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftManager : MonoBehaviour {

    public static CraftManager craftManager;
    public int pickupFlower;
    public int pickupSeaWeed;
    public int healthyHerb; // weet niet zeker waar de healthpotions komen?

    public int checkCounter;
    public bool mayCraft;

    private bool panelActive;


    void Awake ()
    {
        if(craftManager == null)
        {
            craftManager = this;
            DontDestroyOnLoad(this);
        }
        else if(craftManager != this)
        {
            Destroy(this);
        }
    }
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
                PlayerController.playerController.cursorLocked = false;
                Cursor.lockState = CursorLockMode.Confined;
                Camera.main.GetComponent<CameraController>().maymoveMouse = false;
                panelActive = false;
            }
            else {
                PlayerController.playerController.cursorLocked = true;
                Camera.main.GetComponent<CameraController>().maymoveMouse = true;
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
        }
    }
}
