using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestPanel : MonoBehaviour
{
    public int panelID;
    public Text questTitle;
    public Text questDescription;
    public Text questObjective;
    public GameObject checker;
    public GameObject descriptionBox;

    QuestManager questManager;
    public UIManager ui;
    RectTransform descRect;


    void Awake()
    {
        //Assign a few scripts to pluck information from c:
        questManager = GameObject.Find("GameManager").GetComponent<QuestManager>();
        descRect = GetComponent<RectTransform>();
        ui = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    //Fill the panel after instantiating c:
    public void FillPanel(int id)
    {
        //Get the right quest from the id c:
        QuestClass thisQuest = questManager.activeQuests[id];

        //storing the id for further reference c;
        panelID = id;

        //Check if the quest is listed as unfolded c:
        if(thisQuest.unfolded)
        {
            descriptionBox.SetActive(true);
            //Get title and descriptionfrom the right active quest c:
            questDescription.text = thisQuest.questDescription;
        }
        else
        {
            descriptionBox.SetActive(false);
        }
        questTitle.text = thisQuest.questName;

        //If the quest is completed, activate the checker and change the objective text c:
        if (thisQuest.questState == QuestClass.QuestState.TaskCompleted)
        {
            if (thisQuest.unfolded)
            {
                questObjective.text = "Objective Completed!";
            }
            checker.SetActive(true);
        }
        //If it isn't completed show the right objective text c:
        else
        {
            if (thisQuest.unfolded)
            {
                questObjective.text = thisQuest.questObjective;
            } 
            checker.SetActive(false);
        }
    }

    //We can fold and unfold the quest with a button c:
    public void Fold()
    {
        if(questManager.activeQuests[panelID].unfolded)
        {
            questManager.activeQuests[panelID].unfolded = false;
            
            //Reset the ui as we need to reposition them
            ui.ShowQuests();
        }
        else
        {
            questManager.activeQuests[panelID].unfolded = true;

            //Reset the ui as we need to reposition them
            ui.ShowQuests();
        }
    }
}



