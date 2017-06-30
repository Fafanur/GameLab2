using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryManager : MonoBehaviour
{
    public List<ItemClass> items = new List<ItemClass>();
    public List<ItemClass> inventory = new List<ItemClass>();

    UIManager uiManager;

    public int potionAmount;
    public int seaWeedAmount;
    public int flowerAmount;

    public bool hasHelmet;
    public bool hasChest;
    public bool hasWrists;
    public bool hasBoots;

    void Awake()
    {
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    public void AddItem(int ID)
    {
        inventory.Add(items[ID]);
    }

    void Update()
    {
        if(hasHelmet)
        {
            uiManager.LightUpPiece(uiManager.helmet);
        }
        if(hasChest)
        {
            uiManager.LightUpPiece(uiManager.chest);
        }
        if(hasWrists)
        {
            uiManager.LightUpPiece(uiManager.wristguards);
        }
        if(hasBoots)
        {
            uiManager.LightUpPiece(uiManager.boots);
        }
    }


    /*
    public int CountArmorPieces()
    {
        armorAmount = 0;
        for(int i = 0; i < inventory.Count; i++)
        {
            if(inventory[i].itemType == ItemClass.ItemType.ArmorPiece)
            {
                armorAmount++; 
            }
        }
        return armorAmount;
    }


    public int CountSeaweed()
    {
        for(int i = 0; i< inventory.Count; i++)
        {
            if(inventory[i].itemID == 5)
            {
                seaWeedAmount++;
            }
        }
        return seaWeedAmount;
    }

    public int CountPotionItems()
    {
        potionAmount = 0;
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].itemType == ItemClass.ItemType.Consumable)
            {
                potionAmount++;
            }
        }
        return potionAmount;
    }
    */
}
