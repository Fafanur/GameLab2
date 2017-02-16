using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Inventory_Manager : MonoBehaviour {
    PlayerController plyrController;
    UI_Manager uiManager;
    Experience_Manager xpManager;

    void Awake () {
        plyrController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        uiManager = GetComponent<UI_Manager>();
        xpManager = GetComponent<Experience_Manager>();

    }
	
	public int SetItemStats (int number, float defPoints, float healthPoints)
    {
        plyrController.currentHealth = (plyrController.maxHealth + healthPoints) - plyrController.maxHealth + plyrController.currentHealth;
        plyrController.maxHealth += defPoints;
        plyrController.defenseAmount += healthPoints;
        uiManager.SetHealthStats(plyrController.maxHealth, xpManager.health);
        uiManager.SetDefenseStats(plyrController.defenseAmount, xpManager.defense);
        return number;
	}
}
