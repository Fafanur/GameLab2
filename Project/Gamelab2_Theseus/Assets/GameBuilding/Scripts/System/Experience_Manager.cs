//made by Alieke
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience_Manager : MonoBehaviour
{
    public static Experience_Manager xpManager;
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

    public GameObject statPanel;

    void Awake()
    {
        if(xpManager == null)
        {
            xpManager = this;
            DontDestroyOnLoad(this);
        }
        else if(xpManager != this)
        {
            Destroy(this);
        }
    }

	void Update () {
        if (Input.GetButtonDown("Fire2"))
        {
            currentExperience = currentExperience + 25;
            GotExperience();
        }
	}

    public void OpenStats()
    {
        PlayerController.playerController.cursorLocked = true;
        Camera.main.GetComponent<CameraController>().maymoveMouse = true;
        PlayerController.playerController.mayMove = true;
        statPanel.SetActive(true);
    }

    public void GotExperience()
    {
        if(currentExperience > neededExperience)
        {
            UI_Manager.uiManager.experienceBarFiller.fillAmount = 0;
            currentLevel++;
            experiencePoints++;
            UI_Manager.uiManager.levelText.text = currentLevel.ToString();
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
            PlayerController.playerController.attackDamage *= strenghtScale;
            PlayerController.playerController.critDamage += critDmgScale;
            UI_Manager.uiManager.SetStrengthStats(PlayerController.playerController.attackDamage, strength);
        }
    }

    public void AddCrit()
    {
        if (experiencePoints > 0)
        {
            critChance++;
            experiencePoints--;
            PlayerController.playerController.critChance += critScale;
            UI_Manager.uiManager.SetCritStats(PlayerController.playerController.critChance, critChance);
        }
    }

    public void AddStamina()
    {
        if (experiencePoints > 0)
        {
            stamina++;
            experiencePoints--;
            PlayerController.playerController.maxStamina *= stamineScale;
            PlayerController.playerController.currentStamina += PlayerController.playerController.maxStamina + PlayerController.playerController.currentStamina;
            UI_Manager.uiManager.SetStaminaStats(PlayerController.playerController.maxStamina, stamina);
        }
    }

    public void AddHealth()
    {
       
        if (experiencePoints > 0)
        {
            health++;
            experiencePoints--;
            PlayerController.playerController.maxHealth *= healthScale;
            PlayerController.playerController.currentHealth += PlayerController.playerController.maxHealth + PlayerController.playerController.currentHealth;
            UI_Manager.uiManager.SetHealthStats(PlayerController.playerController.maxHealth, health);
        }
    }

    public void AddDefense()
    {
        if (experiencePoints > 0)
        {
            defense++;
            experiencePoints--;
            PlayerController.playerController.defenseAmount += defenseScale;
            PlayerController.playerController.blockChance += blockScale;
            UI_Manager.uiManager.SetDefenseStats(PlayerController.playerController.defenseAmount, defense);
        }
    }
}
