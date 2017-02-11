using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour {
    public Image healthBarFiller;
    public Image experienceBarFiller;

    private PlayerController player;
    private Experience_Manager xpManager;

	void Awake () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        xpManager = GetComponent<Experience_Manager>();
	}
	
	void Update () {
        HealthBar();
        ExperienceBar();
	}


    public void HealthBar()
    {
        float fillAmount1 = player.health.currentHealth / player.health.maxHealth;
        healthBarFiller.fillAmount = Mathf.Lerp(healthBarFiller.fillAmount, fillAmount1, Time.deltaTime * 5);
    }

    public void ExperienceBar(){
        float fillAmount2 = xpManager.currentExperience / xpManager.neededExperience;
        print(experienceBarFiller.fillAmount);
        experienceBarFiller.fillAmount = Mathf.Lerp(experienceBarFiller.fillAmount, fillAmount2, Time.deltaTime * 5);
    }
}
