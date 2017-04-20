using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick_Ups : MonoBehaviour {
    public int thisItemNumber;
    public int defensePoints;
    public int healthPoints;
    public string itemName;

    public float minRotateSpeed;
    public float maxRotateSpeed;
    private Vector3 rotation;

    void Awake()
    {
        rotation = new Vector3(Random.Range(minRotateSpeed, maxRotateSpeed), Random.Range(minRotateSpeed, maxRotateSpeed), Random.Range(minRotateSpeed, maxRotateSpeed));
    }

    void Update()
    {
        RotateItem();
    }

    void RotateItem()
    {
        transform.Rotate(rotation * Time.deltaTime);
    }

    void OnMouseUpAsButton()
    {
        UI_Manager.uiManager.GetItem(thisItemNumber, itemName, healthPoints, defensePoints);
       Inventory_Manager.invManager.SetItemStats(thisItemNumber, defensePoints, healthPoints);
        Destroy(gameObject);
    }
}
