using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enemy stopt niet met chasen als player te verweg is
//Enemy duwt de speler weg

//De target is altijd hetzelfde dus waarom die niet static maken inplaats van in beide script aanroepen idee? maybe idk

public class SatyrManager : MonoBehaviour {
    public enum Enemystates { IdlePatrol, Aggroed, Fleeing, Idle };
    public enum AttackStates { Attacking, Stop };

    public Enemystates moveState;
    public AttackStates attackState;

    public Transform targetPlayer;
    private SatyrAttack satyrAttack;

    [Header("NavMeshComponents")]
    public Transform[] waypoints;
    private int curPoint = 0;
    UnityEngine.AI.NavMeshAgent agent;

    [Header("HealthComponents")]
    public float curHealth;
    public float maxHealth;
    public float regenAmount;
    public bool dead; //Waarvoor een bool alsof ze dood zijn. Wanneer ze dood zijn worden ze toch destroyed.

    [Header("MovementComponents")]
    public float speed;

    [Header("Aggro")]
    public float aggrodis;
    private float distance;
    public float attackdis;
    public bool inRange;


    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        satyrAttack = GetComponent<SatyrAttack>();

        agent.autoBraking = false;
        curHealth = maxHealth;
        dead = false;

        
    }
    void Update()
    {
        distance = Vector3.Distance(transform.position, targetPlayer.position);
        if (distance < aggrodis)
        {
            inRange = true;

        }
        switch (moveState)
        {
            case Enemystates.IdlePatrol:
                IdlePatrol();
                break;
            case Enemystates.Aggroed:
                Aggro();
                break;
            case Enemystates.Fleeing:
                Fleeing();
                break;
        }

        switch (attackState)
        {
            case AttackStates.Attacking:
                Attacking();
                break;
            case AttackStates.Stop:
                break;
        }

        if (curHealth < 0)
        {
            attackState = AttackStates.Stop;
            print("enemy is dead");
            Destroy(gameObject);
            // dood
        }

    }

    private void GoToNextPoint()
    {
        agent.destination = waypoints[curPoint].position;
        curPoint = (curPoint + 1) % waypoints.Length;
        // place waypoints yoself

    }

    public void IdlePatrol()
    {
        if (agent.remainingDistance < 1f)
        {
            GoToNextPoint();
        }
        if (inRange == true)
        {
            moveState = Enemystates.Aggroed;
        }
    }

    void Aggro()
    {
        agent.destination = targetPlayer.position;
        if (distance < attackdis)
        {
            attackState = AttackStates.Attacking;
        }
    }

    void Fleeing()
    {
        agent.destination = waypoints[Random.Range(0, waypoints.Length)].position;
        if (distance > aggrodis)
        {
            moveState = Enemystates.Idle;
        }
    }

    void Attacking()
    {
        // AttackScript but no animations yet.
        if (curHealth < maxHealth / 5)
        {
            inRange = false;
            moveState = Enemystates.Fleeing;
            attackState = AttackStates.Stop;
        }
    }
}
