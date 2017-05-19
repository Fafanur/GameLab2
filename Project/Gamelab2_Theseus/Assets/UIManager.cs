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
    public int activeQuest;
    public List<GameObject> questPanels = new List<GameObject>();
    public float questPanelxPos;
    bool unfolded;



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
        questPanelPlace.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(questPanelPlace.GetComponent<RectTransform>().anchoredPosition, new Vector3(
            questPanelxPos,
            questPanelPlace.GetComponent<RectTransform>().anchoredPosition.y),
            10 * Time.deltaTime);
        ;
    }

    public void ShowQuests()
    {
        for(int i = 0;  i < questManager.activeQuests.Count; i++)
        {
            spawnedQuestPanel = (GameObject)Instantiate(questPanel);
            spawnedQuestPanel.transform.SetParent(questPanelPlace);
            spawnedQuestPanel.GetComponent<RectTransform>().localScale = Vector3.one;

            spawnedQuestPanel.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, i * -50, 0);
            spawnedQuestPanel.GetComponent<QuestPanel>().FillPanel(i);
            spawnedQuestPanel.GetComponent<QuestPanel>().panelID = i;
            questPanels.Add(spawnedQuestPanel);
        }
    }

    public void Unfold()
    {
        if(unfolded)
        {
            questPanelxPos = 0;
        }
        else
        {
            questPanelxPos = 10;
        }
    }
}
