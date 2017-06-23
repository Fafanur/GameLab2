using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryManager : MonoBehaviour
{
    public List<ItemClass> items = new List<ItemClass>();
    public List<ItemClass> inventory = new List<ItemClass>();
    public int armorAmount;
    public int potionAmount;
    public int seaWeedAmount;
    public int flowerAmount;


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
}
