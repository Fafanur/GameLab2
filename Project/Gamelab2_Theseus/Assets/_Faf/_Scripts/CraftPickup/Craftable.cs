using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Craftable : MonoBehaviour
{
    // maakt gewoon een gameobject met de tag flower of seaweed
    // Ik wou eerst dat je items kon opakken net zoals bij skyrim, maar ik heb dat nog niet helemaal werkend gekregen dus dan maar tijdelijk zo.

    public CraftManager craftManager;
    public GameObject pickup;

    void Start()
    {
        pickup = gameObject;
    }

    void OnTriggerEnter(Collider col)
    {      
        if (pickup.tag == "Flower" && col.tag == "Player")
        {
            craftManager.pickupFlower++;
            UI_Manager.uiManager.UpdateCraftables(craftManager.pickupFlower, craftManager.pickupSeaWeed, craftManager.healthyHerb);

            Destroy(gameObject);
        }

        if (pickup.tag == "Seaweed" && col.tag == "Player")
        {
            craftManager.pickupSeaWeed++;
            UI_Manager.uiManager.UpdateCraftables(craftManager.pickupFlower, craftManager.pickupSeaWeed, craftManager.healthyHerb);

            Destroy(gameObject);
        }
       
    }
}

