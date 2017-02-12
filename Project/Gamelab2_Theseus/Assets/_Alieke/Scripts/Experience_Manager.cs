//made by Alieke
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience_Manager : MonoBehaviour {
    public PlayerController player;
    private UI_Manager uiManager;
    public Statistics stats;

    public float currentExperience;
    public float neededExperience;

    public int currentLevel;
    public int experiencePoints;


	void Awake () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        uiManager = GetComponent<UI_Manager>();
	}
	

	void Update () {
        if (Input.GetButtonDown("Fire2"))
        {
            currentExperience = currentExperience + 10;
            GotExperience();
        }
	}

    public void GotExperience()
    {
        if(currentExperience > neededExperience)
        {
            GetComponent<UI_Manager>().experienceBarFiller.fillAmount = 0;
            currentLevel++;
            experiencePoints++;
            currentExperience = currentExperience - neededExperience;
            neededExperience = neededExperience * 1.15f;
            neededExperience = Mathf.Round(neededExperience);
            currentExperience =  Mathf.Round(currentExperience);
        }
    }

    public void AddStrength()
    {
        if (experiencePoints > 0)
        {
            stats.strength++;
            experiencePoints--;
            player.attack.attackDamage *= 1.25f;
            player.attack.critDamage +=0.5f;
            uiManager.SetStrengthStats(player.attack.attackDamage, stats.strength);
        }
    }

    public void AddCrit()
    {
        if (experiencePoints > 0)
        {
            stats.critChance++;
            experiencePoints--;
            player.attack.critChance += 0.25f;
            uiManager.SetCritStats(player.attack.critChance, stats.critChance);
        }
    }

    public void AddStamina()
    {
        if (experiencePoints > 0)
        {
            stats.stamina++;
            experiencePoints--;
            player.stamina.currentStamina = (player.stamina.maxStamina * 1.12f) - player.stamina.maxStamina + player.stamina.currentStamina;
            player.stamina.maxStamina *= 1.12f;          
            uiManager.SetStaminaStats(player.stamina.maxStamina, stats.stamina);         
        }
    }

    public void AddHealth()
    {
        if (experiencePoints > 0)
        {
            stats.health++;
            experiencePoints--;
            player.health.currentHealth = (player.health.maxHealth * 1.12f) - player.health.maxHealth + player.health.currentHealth;
            player.health.maxHealth *= 1.05f;
            uiManager.SetHealthStats(player.health.maxHealth, stats.health);
        }
    }

    public void AddDefense()
    {
        if (experiencePoints > 0)
        {
            stats.defense++;
            experiencePoints--;
            player.defense.defenseAmount += 0.15f;
            player.defense.blockChance += 0.1f;
            uiManager.SetDefenseStats(player.defense.defenseAmount, stats.defense);
        }
    }
}

[System.Serializable]
public class Statistics
{
    public float strength, stamina, health, defense, critChance;
}
