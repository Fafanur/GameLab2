using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Inventory_Manager : MonoBehaviour {
    PlayerController plyrController;

    void Awake () {
        plyrController = player.GetComponent<PlayerController>();
	}
	
	public int SetItemStats (int number, float defPoints, float healthPoints)
    {
        plyrController.maxHealth += defPoints;
        plyrController.defenseAmount += healthPoints;
        return number;
	}
}
