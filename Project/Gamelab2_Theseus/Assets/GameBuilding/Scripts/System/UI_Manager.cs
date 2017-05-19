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
    public Image experienceBarFiller;
    public Image staminaBarFiller;

    [Header("Statistic text")]
    public Text strengthText;
    public Text critText;
    public Text staminaText;
    public Text healthText;
    public Text defenseText;
    public Text levelText;

    [Header("Statistic Points")]
    public Text strengthPointsText;
    private int strengthPoints = 0;
    public Text critPointsText;
    private int critPoints = 0;
    public Text staminaPointsText;
    private int staminaPoints = 0;
    public Text healthPointsText;
    private int hpPoints = 0;
    public Text defensePointsText;
    private int defensePoints = 0;


    [Header("Game Over")]
    public Image gameOverPanel;
    private bool playerDead;

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
        strengthText.text = PlayerController.playerController.attackDamage.ToString();
        critText.text = PlayerController.playerController.critChance.ToString() ;
        staminaText.text = PlayerController.playerController.maxStamina.ToString();
        healthText.text = PlayerController.playerController.maxHealth.ToString().ToString();
        healthPointsText.text = "(0)";
        defenseText.text = PlayerController.playerController.defenseAmount.ToString();
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
        /*itemUI.inventorySlots[slotNumber].color = Color.yellow;
        string text  = itemName + "@Health + " + healthPoints.ToString() + "@Defense + " + defensePoints.ToString();
        text = text.Replace("@",  System.Environment.NewLine);
        itemUI.itemText[slotNumber].text = text;*/
    }

    public void GameOverScreen()
    {       
        gameOverPanel.gameObject.SetActive(true);
        playerDead = true;
    }

    public void SetStrengthStats(float stat, float points)
    {
        strengthText.text = Mathf.Round(stat).ToString();
        strengthPointsText.text = "(" + points.ToString() + ")";
    }

    public void SetCritStats(float stat, float points)
    {
        critText.text =Mathf.Round(stat).ToString();
        critPointsText.text = "(" + points.ToString() + ")";
    }

    public void SetStaminaStats(float stat, float points)
    {
        staminaText.text = Mathf.Round(stat).ToString();
        staminaPointsText.text = "(" + points.ToString() + ")";
    }

    public void SetHealthStats(float stat, float points)
    {
        healthText.text = Mathf.Round(stat).ToString();
        healthPointsText.text = "(" + points.ToString() + ")";
    }

    public void SetDefenseStats(float stat,float points)
    {
        defenseText.text = Mathf.Round(stat).ToString();
        defensePoints++;
        defensePointsText.text = "(" + points.ToString() + ")";
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
        experienceBarFiller.fillAmount = Mathf.Lerp(experienceBarFiller.fillAmount, fillAmount, Time.deltaTime * 2);
        float totalAmount = experienceBarFiller.fillAmount + fillAmount;
    }
}

