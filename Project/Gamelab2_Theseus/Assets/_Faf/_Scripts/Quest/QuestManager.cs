using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<QuestClass> quests = new List<QuestClass>();
    public List<QuestClass> activeQuests = new List<QuestClass>();


    UIManager uiManager;
    InventoryManager inventoryManager;


    void Awake()
    {
        inventoryManager = GetComponent<InventoryManager>();
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    void Start()
    {
        CheckQuestProgression();
        
    }

    public void CheckQuestProgression()
    {
        activeQuests.Clear();
        for (int i = 0; i <quests.Count; i++)
        {
            if (quests[i].questState == QuestClass.QuestState.Active || quests[i].questState == QuestClass.QuestState.TaskCompleted)
            {
                ActivateQuest(quests[i].questID);
                
            }
        }
                uiManager.ShowQuests();
    }


    public void ActivateQuest(int id)
    {
        for(int i = 0; i < quests.Count; i++)
        {
            if(quests[i].questID == id)
            {
                activeQuests.Add(quests[id]);

            }
        }
    }

    public void CompleteTask(int id)
    {
        for (int i = 0; i < activeQuests.Count; i++)
        {
            if (activeQuests[i].questID == id)
            {
                activeQuests[i].questState = QuestClass.QuestState.TaskCompleted;
            }
        }
    }

    public void CompleteQuest(int id)
    {
        for (int i = 0; i < activeQuests.Count; i++)
        {
            if (quests[i].questID == id)
            {
                inventoryManager.inventory.Add(GetItem(id));
            }
        }
    }

    public ItemClass GetItem(int id)
    {
        for(int i = 0; i < inventoryManager.items.Count; i++)
        {
            if(inventoryManager.items[i].itemID == id)
            {
                return inventoryManager.items[i];
            }
        }
        return null;
    }
}
