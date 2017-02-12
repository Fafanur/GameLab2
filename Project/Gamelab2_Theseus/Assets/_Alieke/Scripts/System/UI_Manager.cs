//made by Alieke
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
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

    [Header("Statistic Points")]
    public Text strengthPoints;
    public Text critPoints;
    public Text staminaPoints;
    public Text healthPoints;
    public Text defensePoints;

    [Header("Game Over")]
    public Image gameOverPanel;
    private bool playerDead;

    private PlayerController player;
    private Experience_Manager xpManager;

	void Awake () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        strengthText.text = "Strength : " + player.attackDamage;
        critText.text = "Crit Chance : " + player.critChance;
        staminaText.text = "Stamina : " + player.maxStamina;
        healthText.text = "Health : " + player.maxHealth;
        defenseText.text = "Defense : " + player.defenseAmount;
        xpManager = GetComponent<Experience_Manager>();
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

    public void GameOverScreen()
    {       
        gameOverPanel.gameObject.SetActive(true);
        playerDead = true;
    }

    public float SetStrengthStats(float stat, float points)
    {
        strengthText.text = "Strength : " + Mathf.Round(stat);
        strengthPoints.text = points.ToString();
        return stat;
    }

    public float SetCritStats(float stat, float points)
    {
        critText.text = "Crit chance : " + Mathf.Round(stat);
        critPoints.text = points.ToString();
        return stat;
    }

    public float SetStaminaStats(float stat, float points)
    {
        staminaText.text = "Stamina : " + Mathf.Round(stat);
        staminaPoints.text = points.ToString();
        return stat;
    }

    public float SetHealthStats(float stat, float points)
    {
        healthText.text = "Health : " + Mathf.Round(stat);
        healthPoints.text = points.ToString();
        return stat;
    }

    public float SetDefenseStats(float stat, float points)
    {
        defenseText.text = "Defense : " + Mathf.Round(stat);
        defensePoints.text = points.ToString();
        return stat;
    }

    public void HealthBar()
    {
        float fillAmount = player.currentHealth / player.maxHealth;
        healthBarFiller.fillAmount = Mathf.Lerp(healthBarFiller.fillAmount, fillAmount, Time.deltaTime * 5);
    }

    public void StaminaBar()
    {
        float fillAmount = player.currentStamina / player.maxStamina;
        staminaBarFiller.fillAmount = Mathf.Lerp(staminaBarFiller.fillAmount, fillAmount, Time.deltaTime * 5);
    }

    public void ExperienceBar(){
        float fillAmount = xpManager.currentExperience / xpManager.neededExperience;
        experienceBarFiller.fillAmount = Mathf.Lerp(experienceBarFiller.fillAmount, fillAmount, Time.deltaTime * 5);
    }
}
