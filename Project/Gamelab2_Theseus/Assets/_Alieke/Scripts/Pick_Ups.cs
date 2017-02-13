using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick_Ups : MonoBehaviour {
    public GameObject gameManager;
    public int thisItemNumber;
    public float defensePoints;
    public float healthPoints;

    void OnMouseUpAsButton()
    {
        gameManager.GetComponent<UI_Manager>().GetItem(thisItemNumber);
        gameManager.GetComponent<Inventory_Manager>().SetItemStats(thisItemNumber, defensePoints, healthPoints);
        Destroy(gameObject);
    }
}
