using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemClass
{
    public int itemID;
    public string itemName;
    public string itemDescription;
    public enum ItemType
    {
        Consumable,
        QuestItem,
        ArmorPiece,
        Trash
    };

    public ItemType itemType;
    public Sprite icon;
}
