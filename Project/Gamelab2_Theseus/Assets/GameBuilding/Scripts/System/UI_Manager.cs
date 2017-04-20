//made by Alieke
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager uiManager;
    [Header("UI Bars")]
    public Image healthBarFiller;
    public Slider experienceBarFiller;
    public Image staminaBarFiller;

    [Header("Statistic text")]
    public Text strengthText;
    public Text critText;
    public Text staminaText;
    public Text healthText;
    public Text defenseText;
    public Text levelText;

    [Header("Statistic Points")]
    public Text strengthPoints;
    public Text critPoints;
    public Text staminaPoints;
    public Text healthPoints;
    public Text defensePoints;
    public Text totalPoints;

    [Header("Game Over")]
    public Image gameOverPanel;
    private bool playerDead;

    public GameObject xp_Particle;
    public ItemUI itemUI;

    [Header("Crafting")]
    public GameObject craftPanel;
    public Text seaweedText;
    public Text flowerText;
    public Text healthyHerbsText;

    void Awake () {
        if(uiManager == null)
        {
            uiManager = this;
            DontDestroyOnLoad(this);
        }
        else if(uiManager != this)
        {
            Destroy(this);
        }
	}

    void Start()
    {
        strengthText.text = "Strength : " + PlayerController.playerController.attackDamage;
        critText.text = "Crit Chance : " + PlayerController.playerController.critChance;
        staminaText.text = "Stamina : " + PlayerController.playerController.maxStamina;
        healthText.text = "Health : " + PlayerController.playerController.maxHealth;
        defenseText.text = "Defense : " + PlayerController.playerController.defenseAmount;
    }
	
	void Update () {
        HealthBar();
        ExperienceBar();
        StaminaBar();

        if (playerDead)
        {
            float col = gameOverPanel.color.a;
           col += 2.5f * Time.deltaTime;
        }
	}

    public void UpdateCraftables(int flowerAmount, int seaweedAmount, int healthHerbsAmount)
    {
        flowerText.text = "Flower : " + flowerAmount.ToString();
        seaweedText.text = "Seaweed : " + seaweedAmount.ToString();
        healthyHerbsText.text = "Healthy Herb : " + healthHerbsAmount.ToString();
    }

    public void GetItem(int slotNumber, string itemName, int healthPoints, int defensePoints)
    {
        itemUI.inventorySlots[slotNumber].color = Color.yellow;
        string text  = itemName + "@Health + " + healthPoints.ToString() + "@Defense + " + defensePoints.ToString();
        text = text.Replace("@",  System.Environment.NewLine);
        itemUI.itemText[slotNumber].text = text;
    }

    public void GameOverScreen()
    {       
        gameOverPanel.gameObject.SetActive(true);
        playerDead = true;
    }

    public void SetStrengthStats(float stat, float points)
    {
        strengthText.text = "Strength : " + Mathf.Round(stat);
        strengthPoints.text = points.ToString();
    }

    public void SetCritStats(float stat, float points)
    {
        critText.text = "Crit chance : " + Mathf.Round(stat);
        critPoints.text = points.ToString();
    }

    public void SetStaminaStats(float stat, float points)
    {
        staminaText.text = "Stamina : " + Mathf.Round(stat);
        staminaPoints.text = points.ToString();
    }

    public void SetHealthStats(float stat, float points)
    {
        healthText.text = "Health : " + Mathf.Round(stat);
        healthPoints.text = points.ToString();
    }

    public void SetDefenseStats(float stat, float points)
    {
        defenseText.text = "Defense : " + Mathf.Round(stat);
        defensePoints.text = points.ToString();
    }

    public void SetTotalPoints(int points)
    {
        totalPoints.text = "Points : " + points.ToString();
    }

    public void HealthBar()
    {
        float fillAmount = PlayerController.playerController.currentHealth / PlayerController.playerController.maxHealth;
        healthBarFiller.fillAmount = Mathf.Lerp(healthBarFiller.fillAmount, fillAmount, Time.deltaTime * 5);
    }

    public void StaminaBar()
    {
        float fillAmount = PlayerController.playerController.currentStamina / PlayerController.playerController.maxStamina;
        staminaBarFiller.fillAmount = Mathf.Lerp(staminaBarFiller.fillAmount, fillAmount, Time.deltaTime * 5);
    }

    public void ExperienceBar(){
        float fillAmount = Experience_Manager.xpManager.currentExperience / Experience_Manager.xpManager.neededExperience;
        experienceBarFiller.value = Mathf.Lerp(experienceBarFiller.value, fillAmount, Time.deltaTime * 5);
        float totalAmount = experienceBarFiller.value + fillAmount;
        if(experienceBarFiller.value != fillAmount)
        {
            xp_Particle.SetActive(true);
        }
    }
}

[System.Serializable]
public class ItemUI
{
    public List<Image> inventorySlots = new List<Image>();
    public List<Text> itemText = new List<Text>();
}

