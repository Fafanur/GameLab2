using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick_Ups : MonoBehaviour
{
    public int defensePoints;
    public int healthPoints;
    public string itemName;

    public int thisItemID;
    private InventoryManager inventory;
    private ThisFixesGame fix;

    void Start()
    {
        fix = GameObject.Find("GameManager").GetComponent<ThisFixesGame>();
        inventory = GameObject.Find("GameManager").GetComponent<InventoryManager>();
    }

    void OnMouseDown()
    {
        if (gameObject.tag == "Sword")
        {
            fix.pickedupWep++;
            Destroy(gameObject);
        }

        if (gameObject.tag == "Flower")
        {
            CraftManager.craftManager.pickupFlower++;
            Destroy(gameObject);
        }
        else if (gameObject.tag == "Seaweed")
        {
            CraftManager.craftManager.pickupSeaWeed++;
            inventory.seaWeedAmount++;
            Destroy(gameObject);
        }

        if(gameObject.tag == "Armor")
        {
            inventory.armorAmount++;
        }


      
        
    }
}
