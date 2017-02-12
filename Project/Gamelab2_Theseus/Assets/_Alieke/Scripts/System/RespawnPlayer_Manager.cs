using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlayer_Manager : MonoBehaviour
{
    [Header("Spawn positions")]
    public Transform labyrinthSpawnPos;
    public Transform startPointSpawnPos;
    public Vector3 currentSpawnPoint;

    private GameObject player;
    private PlayerController plyrController;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        plyrController = player.GetComponent<PlayerController>();
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
        player.transform.position = currentSpawnPoint;
        plyrController.currentHealth = plyrController.maxHealth;
        plyrController.currentStamina = plyrController.maxStamina;
        GetComponent<UI_Manager>().gameOverPanel.gameObject.SetActive(false);
        plyrController.mayMove = true;
        Camera.main.GetComponent<CameraController>().maymoveMouse = true;
    }
}
