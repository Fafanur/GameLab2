using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatyrAttack : MonoBehaviour
{
    private SatyrManager satyrManager;
    public Transform attackTarget;
    private Transform myTransform;
    public float attackRate = 1;
    private float nextAttack;
    public float attackRange = 3.5f;
    public int attackDamage;
    
	
	void Start()
    {
        satyrManager = GetComponent<SatyrManager>();
        myTransform = transform; //Waarom eigen transform zoeken.
        attackTarget = satyrManager.targetPlayer;
        
    }
    // Update is called once per frame
	void Update ()
    {
        TryToAttack();

    }

    void SetAttackTarget(Transform targetTransform) //why tho
    {
        attackTarget = targetTransform;
        

    }

    void TryToAttack()
    {
        if(attackTarget!= null)
        {
            if (Time.time > nextAttack)
            {
                nextAttack = Time.time + attackRate;
                if (Vector3.Distance(myTransform.position, attackTarget.position) < attackRange)
                {
                    Vector3 lookAtVector = new Vector3(attackTarget.position.x, myTransform.position.y, attackTarget.position.z);
                    myTransform.LookAt(lookAtVector);
                    // attacking true
                }
            }
        }
    }

    // animation shizz
    public void AnimOnEnemyAttack()
    {
        if(attackTarget != null)
        {
            if (Vector3.Distance(myTransform.position, attackTarget.position) <= attackRange &&
           attackTarget.GetComponent<SatyrManager>() != null)
            {
                print("ew"); // only should give damage when player is infront of the enemy
            }

        }
    }

}
