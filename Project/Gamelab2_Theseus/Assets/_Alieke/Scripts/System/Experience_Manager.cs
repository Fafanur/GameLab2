//made by Alieke
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience_Manager : MonoBehaviour
{
    [Header("Components")]
    public PlayerController player;
    private UI_Manager uiManager;

    [Header("Level management")]
    public float currentExperience;
    public float neededExperience;
    public int currentLevel;
    public int experiencePoints;

    [Header("Statistics")]
    public float strength;
    public float stamina;
    public float health;
    public float defense;
    public float critChance;


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
            GetComponent<UI_Manager>().experienceBarFiller.value = 0;
            currentLevel++;
            experiencePoints++;
            uiManager.SetTotalPoints(experiencePoints);
            GetComponent<UI_Manager>().levelText.text = "Level : " + currentLevel.ToString();
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
            strength++;
            experiencePoints--;
            player.attackDamage *= 1.25f;
            player.critDamage +=0.5f;
            uiManager.SetStrengthStats(player.attackDamage, strength);
            uiManager.SetTotalPoints(experiencePoints);
        }
    }

    public void AddCrit()
    {
        if (experiencePoints > 0)
        {
            critChance++;
            experiencePoints--;
            player.critChance += 0.25f;
            uiManager.SetCritStats(player.critChance, critChance);
            uiManager.SetTotalPoints(experiencePoints);
        }
    }

    public void AddStamina()
    {
        if (experiencePoints > 0)
        {
            stamina++;
            experiencePoints--;
            player.currentStamina = (player.maxStamina * 1.12f) - player.maxStamina + player.currentStamina;
            player.maxStamina *= 1.12f;          
            uiManager.SetStaminaStats(player.maxStamina, stamina);
            uiManager.SetTotalPoints(experiencePoints);
        }
    }

    public void AddHealth()
    {
        if (experiencePoints > 0)
        {
            health++;
            experiencePoints--;
            player.currentHealth = (player.maxHealth * 1.12f) - player.maxHealth + player.currentHealth;
            player.maxHealth *= 1.05f;
            uiManager.SetHealthStats(player.maxHealth, health);
            uiManager.SetTotalPoints(experiencePoints);
        }
    }

    public void AddDefense()
    {
        if (experiencePoints > 0)
        {
            defense++;
            experiencePoints--;
            player.defenseAmount += 0.15f;
            player.blockChance += 0.1f;
            uiManager.SetDefenseStats(player.defenseAmount, defense);
            uiManager.SetTotalPoints(experiencePoints);
        }
    }
}
