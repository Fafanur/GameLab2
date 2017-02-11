//made by Alieke
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    //Components
    public GameObject gameManager;
    public Animator headBob;
    private Rigidbody _rb;
    private Animator _Anim;
    public GameObject statPanel;

    enum States { Idle, Walking }
    States movingStates;


    public Movement movement;
    public Stamina stamina;
    public Health health;
    public Attack attack;
    public Defense defense;

    private bool mayJump = true;
    private bool mayAttack = true;

    private bool statPanelOpen;

    public float fillAmount;


    void Awake()
    {
        stamina.currentStamina = stamina.maxStamina;
        movement.normalSpeed = movement.moveSpeed;
        _rb = GetComponent<Rigidbody>();
        _Anim = headBob.GetComponent<Animator>();
    }

    void Update() // Opens the statistics panel
    {
        if (Input.GetButtonDown("Open Stats"))
        {
            if (statPanelOpen)
            {
                statPanel.SetActive(false);
                statPanelOpen = false;
                mayAttack = true;
            }
            else
            {
                statPanel.SetActive(true);
                mayAttack = false;
                statPanelOpen = true;
            }
        }

        if (Input.GetButtonDown("o"))
        {
            GetHit(5);
        }
    }

    void FixedUpdate()
    {
        Movement();
        Jump();

        switch (movingStates)
        {
            case States.Idle:
                _Anim.SetBool("Walking", false);
                break;
            case States.Walking:
                _Anim.SetBool("Walking", true);
                break;
        }
    }

    void Movement()
    {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");
        _rb.MovePosition(transform.position + transform.forward * Time.deltaTime * movement.moveSpeed * yInput + transform.right * Time.deltaTime * movement.moveSpeed * xInput);
        if (yInput > 0)
        {
            movingStates = States.Walking;
            if (Input.GetButton("Shift"))
            {
                if (stamina.currentStamina > 0)
                {
                    movement.moveSpeed += movement.accelerationSpeed * Time.deltaTime;
                    Mathf.Clamp(movement.moveSpeed, 0, movement.maxSpeed);
                    stamina.currentStamina -= stamina.staminaDrain * Time.deltaTime;
                }
            }
        }
        else if (movement.moveSpeed > movement.normalSpeed)
        {
            movement.moveSpeed -= movement.accelerationSpeed * Time.deltaTime;
        }
        else
        {
            movingStates = States.Idle;
        }

        if (stamina.currentStamina < stamina.maxStamina && !Input.GetButtonDown("Shift"))
        {
            stamina.currentStamina += stamina.staminaRecover * Time.deltaTime;
        }

        if (Physics.Raycast(transform.position, -transform.up, movement.heightRayDis))
        {
            _rb.useGravity = false;
        }
        else
        {
            _rb.useGravity = true;
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (mayJump)
            {
                _rb.AddForce(Vector3.up * movement.jumpForce);
                mayJump = false;
            }
        }
        if (Physics.Raycast(transform.position, -transform.up, movement.heightRayDis))
        {
            mayJump = true;
        }
    }

    void Attack()
    {
        print(CritStrike());
        if (CritStrike())
        {
            attack.totalDamage = Mathf.Round(attack.attackDamage * attack.critDamage);
            //attack animation
        }
    }

    private  bool CritStrike() //Calculates if attack crits
    {
        float number = Random.Range(1, 101);
        if (attack.critChance >= number)
        {
            return true;
         }
        else
        {
            return false;
        }
    }

    float GetHit (float damage)
    {
        if (!BlockChance())
        {
            health.currentHealth -= (damage - defense.defenseAmount);
            if (health.currentHealth <= 0)
            {
                //death
            }
        }
        return health.currentHealth;
    }

    bool BlockChance() // calculate if the player blocks
    {
        float number = Random.Range(1, 101);
        if(number <= defense.blockChance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    float GetHealth(float healAmount)
    {
        if ((health.currentHealth + healAmount) > health.maxHealth){
            health.currentHealth = health.maxHealth;         
        }
        else
        {
            health.currentHealth += healAmount;
        }
        return health.currentHealth;
    }
}

[System.Serializable]
public class Attack
{
    public float attackDamage, critChance,critDamage, totalDamage;
}

[System.Serializable]
public class Health
{
    public float currentHealth, maxHealth;
}

[System.Serializable]
public class Stamina
{
    public float currentStamina, maxStamina, staminaDrain, staminaRecover;
}

[System.Serializable]
public class Defense
{
    public float blockChance, defenseAmount;
}

[System.Serializable]
public class Movement
{
    public float moveSpeed, accelerationSpeed, maxSpeed, normalSpeed, heightRayDis, jumpForce;

}