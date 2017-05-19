using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryManager : MonoBehaviour
{
    public List<ItemClass> items = new List<ItemClass>();
    public List<ItemClass> inventory = new List<ItemClass>();
    public int amount;


    public int CountArmorPieces()
    {
        amount = 0;
        for(int i = 0; i < inventory.Count; i++)
        {
            if(inventory[i].itemType == ItemClass.ItemType.ArmorPiece)
            {
                amount++;
            }
        }
        return amount;
    }
}
