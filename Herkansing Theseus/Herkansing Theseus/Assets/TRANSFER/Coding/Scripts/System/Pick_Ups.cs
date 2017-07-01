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
	public UIManager uimanager;

    void Start()
    {
        fix = GameObject.Find("GameManager").GetComponent<ThisFixesGame>();
        inventory = GameObject.Find("GameManager").GetComponent<InventoryManager>();
		uimanager = GameObject.Find ("Canvas").GetComponent<UIManager> ();

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
            //CraftManager.craftManager.pickupFlower++;
			uimanager.herbs ++;
			uimanager.PickuphealingItems ();
            Destroy(gameObject);
        }
        else if (gameObject.tag == "Seaweed")
        {
            //CraftManager.craftManager.pickupSeaWeed++;
            //inventory.seaWeedAmount++;

			uimanager.seaweed ++;
			uimanager.PickuphealingItems ();
            Destroy(gameObject);
        }

        if(gameObject.tag == "Helmet")
        {
            inventory.hasHelmet = true;
            Destroy(gameObject);
        }
        if (gameObject.tag == "Chest")
        {
            inventory.hasChest = true;
            Destroy(gameObject);
        }
        if (gameObject.tag == "Wrists")
        {
            inventory.hasWrists = true;
            Destroy(gameObject);
        }
        if (gameObject.tag == "Boots")
        {
            inventory.hasBoots = true;
            Destroy(gameObject);
        }
    }
}
