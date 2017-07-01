using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satyr : MonoBehaviour
{
    public enum states { follow, idle, attack }

    public states satyrState;

    public Transform targetPlayer;
    private SatyrAttack satyrAttackScript;

    private Animator anim;

    public PlayerController playerController;
    public Vector3 lookAtVector;
    public bool hit;


    [Header("HealthComponents")]
    public float curHealth;
    public float maxHealth;
    //public float regenAmount;
    public bool dead;

    [Header("Aggro")]
    private float distance;
    public float aggroDistance;
    public float attackDistance;
    public bool inRange;
    public float moveSpeed;

    [Header("Attack")]
    public float attackDamage;

    void Start()
    {
        curHealth = maxHealth;
        targetPlayer = GameObject.Find("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();


    }

    void Update()
    {
        lookAtVector = new Vector3(targetPlayer.position.x, transform.position.y, targetPlayer.position.z);
        distance = Vector3.Distance(transform.position, targetPlayer.position);
        switch (satyrState)
        {
            case states.follow:
                Follow();
                break;
            case states.idle:
                Idle();
                break;
            case states.attack:
                AttackControl();
                anim.SetBool("Stab", true);
                break;

        }

        if(curHealth < 0)
        {
            Death();
        }
    }

    void Idle()
    {
        //anim.SetBool("Idle", true);
        if (distance < aggroDistance)
        {
            satyrState = states.follow;
        }
        anim.SetBool("Running", false);
        anim.SetBool("Stab", false);

    }

    void Follow()
    {
        print("Im follwing");
        anim.SetBool("Idle", false);
        anim.SetBool("Running", true);
        anim.SetBool("Stab", false);

        Vector3 localPosition = targetPlayer.position - transform.position;
        localPosition = localPosition.normalized; // The normalized direction in LOCAL space
        transform.Translate(localPosition.x * Time.deltaTime * moveSpeed, localPosition.y * Time.deltaTime * moveSpeed, localPosition.z * Time.deltaTime * moveSpeed);
        transform.LookAt(lookAtVector);

        if (distance < attackDistance)
        {
            satyrState = states.attack;

        }

        if (distance > aggroDistance)
        {
            satyrState = states.idle;
        }
    }

   
    public void Attack()
    {
        print("Im attack u");
        if (targetPlayer != null)
        {
            if (distance < attackDistance)
            {
                transform.LookAt(lookAtVector);
                playerController.currentHealth -= attackDamage;
            }
        }        

    }


    void AttackControl()
    {
        if (playerController.currentHealth < 0)
        {
            satyrState = states.idle;
        }

        if(distance > attackDistance)
        {
            satyrState = states.follow;
        }
    }

    public void Death()
    {
        Destroy(gameObject);


    }

}

