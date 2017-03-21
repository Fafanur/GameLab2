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

    [Header("ScaleFactor")]
    public float experienceScale;
    public float strenghtScale;
    public float critDmgScale;
    public float critScale;
    public float stamineScale;
    public float healthScale;
    public float defenseScale;
    public float blockScale;

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
            uiManager.experienceBarFiller.value = 0;
            currentLevel++;
            experiencePoints++;
            uiManager.SetTotalPoints(experiencePoints);
            uiManager.levelText.text = "Level : " + currentLevel.ToString();
            currentExperience = currentExperience - neededExperience;
            neededExperience = neededExperience * experienceScale;
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
            player.attackDamage *= strenghtScale;
            player.critDamage += critDmgScale;
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
            player.critChance += critScale;
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
            player.maxStamina *= stamineScale;
            player.currentStamina -= player.maxStamina + player.currentStamina;    
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
            player.maxHealth *= healthScale;
            player.currentHealth -=player.maxHealth + player.currentHealth;
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
            player.defenseAmount += defenseScale;
            player.blockChance += blockScale;
            uiManager.SetDefenseStats(player.defenseAmount, defense);
            uiManager.SetTotalPoints(experiencePoints);
        }
    }
}
