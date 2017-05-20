using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestClass
{
    public int questID;
    public string questName;
    public string questDescription;
    public string questObjective;
    public enum QuestType
    {
        Fetch,
        Explore
    };

    public QuestType questType;
    public Vector3 checkpoint;

    public enum QuestState
    {
        Inactive,
        Active,
        TaskCompleted,
        Completed
    };

    public QuestState questState;

    public int requiredItemID;
    public int rewardItemID;
    public int expReward;
    public bool unfolded;
}
