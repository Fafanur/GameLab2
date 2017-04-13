using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick_Ups : MonoBehaviour {
    public int thisItemNumber;
    public int defensePoints;
    public int healthPoints;
    public string itemName;

    void OnMouseUpAsButton()
    {
        UI_Manager.uiManager.GetItem(thisItemNumber, itemName, healthPoints, defensePoints);
       Inventory_Manager.invManager.SetItemStats(thisItemNumber, defensePoints, healthPoints);
        Destroy(gameObject);
    }
}
