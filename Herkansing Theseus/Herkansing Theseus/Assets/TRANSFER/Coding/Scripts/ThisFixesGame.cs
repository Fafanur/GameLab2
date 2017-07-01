using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThisFixesGame : MonoBehaviour
{
    public GameObject startWeapon;
    public QuestManager questmanager;
    public int pickedupWep;

    // Use this for initialization

    void Awake()
    {

        startWeapon.SetActive(false);
        questmanager = GetComponent<QuestManager>();
    }

    void Start()
    {
        questmanager.enabled = false;
    }

	
	// Update is called once per frame
	void Update ()
    {
		if(pickedupWep == 1f)
        {
            startWeapon.SetActive(true);
            questmanager.enabled = true;
        }
	}

}
