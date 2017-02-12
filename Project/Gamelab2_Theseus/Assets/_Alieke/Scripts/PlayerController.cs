//made by Alieke
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    public GameObject gameManager;
    public Animator headBob;
    private Rigidbody _rb;
    private Animator _Anim;
    public GameObject statPanel;
    public GameObject statPointsPanel;

    enum States { Idle, Walking }
    States movingStates;

    [Header("Movement")]
    public float moveSpeed;
    public float accelerationSpeed;
    public float maxSpeed;
    public float normalSpeed;
    public float heightRayDis;
    public float jumpForce;
    private bool mayJump = true;
    private bool mayAttack = true;

    [Header("Attack")]
    public float attackDamage;
    public float critChance;
    public float critDamage;
    public float totalDamage;

    [Header("Stamina")]
    public float currentStamina;
    public float maxStamina;
    public float staminaDrain;
    public float staminaRecover;

    [Header("Health")]
    public float currentHealth;
    public float maxHealth;

    [Header("Defense")]
    public float blockChance;
    public float defenseAmount;

    private bool statPanelOpen;

    void Awake()
    {
        currentHealth = maxHealth;
        currentStamina = maxStamina;
        normalSpeed = moveSpeed;
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
                statPointsPanel.SetActive(false);
                statPanelOpen = false;
                mayAttack = true;
                Camera.main.GetComponent<CameraController>().maymoveMouse = true;
            }
            else
            {
                Camera.main.GetComponent<CameraController>().maymoveMouse = false;
                statPanel.SetActive(true);
                statPointsPanel.SetActive(true);
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
        _rb.MovePosition(transform.position + transform.forward * Time.deltaTime * moveSpeed * yInput + transform.right * Time.deltaTime * moveSpeed * xInput);
        if (yInput > 0)
        {
            movingStates = States.Walking;
            if (Input.GetButton("Shift"))
            {
                if (currentStamina > 0)
                {
                    moveSpeed += accelerationSpeed * Time.deltaTime;
                    Mathf.Clamp(moveSpeed, 0, maxSpeed);
                    currentStamina -= staminaDrain * Time.deltaTime;
                }
            }
        }
        else if (moveSpeed > normalSpeed)
        {
            moveSpeed -= accelerationSpeed * Time.deltaTime;
        }
        else
        {
            movingStates = States.Idle;
        }

        if (currentStamina < maxStamina && !Input.GetButtonDown("Shift"))
        {
            currentStamina += staminaRecover * Time.deltaTime;
        }

        if (Physics.Raycast(transform.position, -transform.up, heightRayDis))
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
                _rb.AddForce(Vector3.up * jumpForce);
                mayJump = false;
            }
        }
        if (Physics.Raycast(transform.position, -transform.up, heightRayDis))
        {
            mayJump = true;
        }
    }

    void Attack()
    {
        print(CritStrike());
        if (CritStrike())
        {
            totalDamage = Mathf.Round(attackDamage * critDamage);
            //attack animation
        }
    }

    private  bool CritStrike() //Calculates if attack crits
    {
        float number = Random.Range(1, 101);
        if (critChance >= number)
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
            currentHealth -= (damage - defenseAmount);
            if (currentHealth <= 0)
            {
                //death
            }
        }
        return currentHealth;
    }

    bool BlockChance() // calculate if the player blocks
    {
        float number = Random.Range(1, 101);
        if(number <= blockChance)
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
        if ((currentHealth + healAmount) > maxHealth){
            currentHealth = maxHealth;         
        }
        else
        {
            currentHealth += healAmount;
        }
        return currentHealth;
    }
}