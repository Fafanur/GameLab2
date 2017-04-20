using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatyrAttack : MonoBehaviour
{
    public SatyrManager satyrManager;
    public Transform attackTarget;
    public Transform myTransform;
    public float attackRate = 1;
    private float nextAttack;
    public float attackRange = 3.5f;
    public int attackDamage;

	
	void Start()
    {
        satyrManager = GetComponent<SatyrManager>();
        myTransform = transform;
        attackTarget = satyrManager.targetPlayer;
        
    }
    // Update is called once per frame
	void Update ()
    {
        TryToAttack();

    }

    void SetAttackTarget(Transform targetTransform)
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

    // animation shizz, maar nog geen animaties.
    public void AnimOnEnemyAttack()
    {

    }

}
