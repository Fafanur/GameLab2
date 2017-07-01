using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlayer_Manager : MonoBehaviour
{
    public static RespawnPlayer_Manager respawnManager;
    [Header("Spawn positions")]
    public Transform labyrinthSpawnPos;
    public Transform startPointSpawnPos;
    public Transform startPos;
    public Vector3 currentSpawnPoint;

    void Awake()
    {
        if(respawnManager == null)
        {
            respawnManager = this;
            DontDestroyOnLoad(this);
        }
        else if(respawnManager != this)
        {
            Destroy(this);
        }
    }

    void Start()
    {
        currentSpawnPoint = startPos.position;
        PlayerController.playerController.transform.position = startPos.position;
    }
	
	public void SetSpawnPoint (string spawnPoint) {
		if(spawnPoint == "Labyrinth")
        {
            currentSpawnPoint = labyrinthSpawnPos.position;
        }
        if(spawnPoint == "Starting Point")
        {
            currentSpawnPoint = startPointSpawnPos.position;
        }
	}

    public void RespawnPlayer()
    {
        PlayerController.playerController.transform.position = currentSpawnPoint;
        PlayerController.playerController.currentHealth = PlayerController.playerController.maxHealth;
        PlayerController.playerController.currentStamina = PlayerController.playerController.maxStamina;
       // GetComponent<UI_Manager>().gameOverPanel.gameObject.SetActive(false);
        PlayerController.playerController.mayMove = true;
        Camera.main.GetComponent<CameraController>().maymoveMouse = true;
    }
}
