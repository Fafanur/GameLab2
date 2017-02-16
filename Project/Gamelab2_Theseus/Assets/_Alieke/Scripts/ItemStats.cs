using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStats : MonoBehaviour {
    public GameObject itemPanel;

    public void OnMouseEnter()
    {
        itemPanel.SetActive(true);
    }

    public void OnMouseExit()
    {
        itemPanel.SetActive(false);
    }
}
