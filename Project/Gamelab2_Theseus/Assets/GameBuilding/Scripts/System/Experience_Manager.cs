//made by Alieke
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("StatTexts")]
    public Text strengthText;
    public Text staminaText;
    public Text healthText;
    public Text defenseText;
    public Text critText;
    public Text levelText;

    [Header("StatPoints")]
    public Text strengthPoints;
    public Text staminaPoints;
    public Text healthPoints;
    public Text defensePoints;
    public Text critPoints;

    public GameObject statPanel;
    private UIManager uiManager;
    private bool openStats = false;

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

    void Start ()
    {
        uiManager = GameObject.Find("GameManager").GetComponent<UIManager>();
        strengthText.text = PlayerController.playerController.attackDamage.ToString();
        critText.text = PlayerController.playerController.critChance.ToString();
        staminaText.text = PlayerController.playerController.maxStamina.ToString();
        healthText.text = PlayerController.playerController.maxHealth.ToString();
        defenseText.text = PlayerController.playerController.defenseAmount.ToString();
    }

	/*void Update () {
        if (Input.GetButtonDown("Fire2"))
        {
            currentExperience = currentExperience + 25;
            GotExperience();
        }
	}*/

    public void OpenCloseStats()
    {
        if (openStats)
        {
            Camera.main.GetComponent<CameraController>().maymoveMouse = true;
            PlayerController.playerController.mayMove = true;
            statPanel.SetActive(false);
            openStats = false;
        }
        else
        {
            Camera.main.GetComponent<CameraController>().maymoveMouse = false;
            PlayerController.playerController.mayMove = false;
            statPanel.SetActive(true);
            openStats = true;
        }
    }

    public void GotExperience()
    {
        if(currentExperience > neededExperience)
        {
            uiManager.experienceBarFiller.fillAmount = 0;
            currentLevel++;
            experiencePoints++;
            levelText.text = currentLevel.ToString();
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
            strengthText.text = PlayerController.playerController.attackDamage.ToString();
            strengthPoints.text = strength.ToString();
        }
    }

    public void AddCrit()
    {
        if (experiencePoints > 0)
        {
            critChance++;
            experiencePoints--;
            PlayerController.playerController.critChance += critScale;
            critText.text = PlayerController.playerController.critChance.ToString();
            critPoints.text = critChance.ToString();
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
            staminaText.text = PlayerController.playerController.maxStamina.ToString();
            staminaPoints.text = stamina.ToString();
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
            healthText.text = PlayerController.playerController.maxHealth.ToString();
            healthPoints.text = health.ToString();
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
            defenseText.text = PlayerController.playerController.defenseAmount.ToString();
            defensePoints.text = defense.ToString();
        }
    }
}
