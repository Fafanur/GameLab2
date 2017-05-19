using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestPanel : MonoBehaviour
{
    public int panelID;
    public Text questTitle;
    public Text questDescription;
    public GameObject checker;

    CanvasGroup cg;

    QuestManager questManager;
    UIManager ui;
    RectTransform descRect;


    void Awake()
    {
        questManager = GameObject.Find("GameManager").GetComponent<QuestManager>();
        ui = GameObject.Find("Canvas").GetComponent<UIManager>();
        descRect = GetComponent<RectTransform>();
    }

    //Fill the panel after instantiating
    public void FillPanel(int id)
    {
        questTitle.text = questManager.activeQuests[id].questName;
        questDescription.text = questManager.activeQuests[id].questDescription;
        if (questManager.activeQuests[id].questState == QuestClass.QuestState.TaskCompleted)
        {
            checker.SetActive(true);
        }
        else
        {
            checker.SetActive(false);
        }
    }
}

