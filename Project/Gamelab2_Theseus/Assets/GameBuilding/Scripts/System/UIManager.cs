using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//UIManager on the canvas, displays everything from GameManager c:
public class UIManager : MonoBehaviour
{
    public QuestManager questManager;
    public InventoryManager inventoryManager;

    [Header("UIBars")]
    public Image healthBarFiller;
    public Image experienceBarFiller;
    public Image staminaBarFiller;

    [Header("Quests")]

    GameObject spawnedQuestPanel;
    public GameObject questPanel;
    public Transform questPanelPlace;
    public int unfoldedQuests;
    public List<GameObject> questPanels = new List<GameObject>();

    void Awake()
    {
        //Inject scripts from gamemanager when scene loads c:
        questManager = GameObject.Find("GameManager").GetComponent<QuestManager>();
        inventoryManager = GameObject.Find("GameManager").GetComponent<InventoryManager>();
    }

    void Start()
    {
        ShowQuests();
    }

    void Update()
    {
        HealthBar();
        ExperienceBar();
        StaminaBar();
    }

    public void ShowQuests()
    {
        //remove current panels so we don't get duplicates. bai bai c:
        foreach(GameObject questPanel in questPanels )
        {
            Destroy(questPanel);
        }
        questPanels.Clear();


        //reset the counter before the for-loop c:
        unfoldedQuests = 0;

        //now we make the panels c:
        for (int i = 0; i < questManager.activeQuests.Count; i++)
        {
            spawnedQuestPanel = (GameObject)Instantiate(questPanel);
            spawnedQuestPanel.transform.SetParent(questPanelPlace);
            spawnedQuestPanel.GetComponent<RectTransform>().localScale = Vector3.one;

            spawnedQuestPanel.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, (i * -30) + (unfoldedQuests * -55), 0);
            spawnedQuestPanel.GetComponent<QuestPanel>().FillPanel(i);
            spawnedQuestPanel.GetComponent<QuestPanel>().panelID = i;
            spawnedQuestPanel.GetComponent<QuestPanel>().ui = this;

            //we need to know foldstate and keep track of it for the position of the panel c:
            if (questManager.activeQuests[i].unfolded)
            {
                unfoldedQuests++;
            }

            //Add the panels to a list so we can destroy them all at once c:
            questPanels.Add(spawnedQuestPanel);
        }
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

    public void ExperienceBar()
    {
        float fillAmount = Experience_Manager.xpManager.currentExperience / Experience_Manager.xpManager.neededExperience;
        experienceBarFiller.fillAmount = Mathf.Lerp(experienceBarFiller.fillAmount, fillAmount, Time.deltaTime * 2);
    }
}
