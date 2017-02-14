using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRespawnPosition : MonoBehaviour {
    private RespawnPlayer_Manager respawnManager;
    public string nameSpawnPoint;

    void Awake()
    {
        respawnManager = GameObject.FindGameObjectWithTag("Gamemanager").GetComponent<RespawnPlayer_Manager>();
    }


    void OnTriggerEnter(Collider trigger)
    {
        if(trigger.transform.tag == "Player")
        {
            respawnManager.SetSpawnPoint(nameSpawnPoint);
        }
    }
}
