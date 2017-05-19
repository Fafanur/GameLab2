using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Inventory_Manager : MonoBehaviour
{
    public static Inventory_Manager invManager;

    void Awake()
    {
        if (invManager == null){
            invManager = this;
            DontDestroyOnLoad(this);
        }
        else if(invManager != this)
        {
            Destroy(this);
        }
    }

	public void SetItemStats (int number, float defPoints, float healthPoints)
    {
        PlayerController.playerController.currentHealth = (PlayerController.playerController.maxHealth + healthPoints) - PlayerController.playerController.maxHealth + PlayerController.playerController.currentHealth;
        PlayerController.playerController.maxHealth += defPoints;
        PlayerController.playerController.defenseAmount += healthPoints;
        UI_Manager.uiManager.SetHealthStats(PlayerController.playerController.maxHealth, healthPoints);
        UI_Manager.uiManager.SetDefenseStats(PlayerController.playerController.defenseAmount, Experience_Manager.xpManager.defense);
	}
}
