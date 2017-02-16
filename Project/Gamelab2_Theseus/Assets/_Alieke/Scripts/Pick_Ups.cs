using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick_Ups : MonoBehaviour {
    public GameObject gameManager;
    public int thisItemNumber;
    public int defensePoints;
    public int healthPoints;
    public string itemName;

    void OnMouseUpAsButton()
    {
        gameManager.GetComponent<UI_Manager>().GetItem(thisItemNumber, itemName, healthPoints, defensePoints);
        gameManager.GetComponent<Inventory_Manager>().SetItemStats(thisItemNumber, defensePoints, healthPoints);
        Destroy(gameObject);
    }
}
