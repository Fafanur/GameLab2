using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRespawnPosition : MonoBehaviour { 
    public string nameSpawnPoint;

    void OnTriggerEnter(Collider trigger)
    {
        if(trigger.transform.tag == "Player")
        {
            RespawnPlayer_Manager.respawnManager.SetSpawnPoint(nameSpawnPoint);
        }
    }
}
