//made by Alieke
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    public GameObject gameManager;
    private Rigidbody _rb;
    public Animator headBobAnimator;
    public GameObject statPanel;
    public GameObject statPointsPanel;
    private Animator ownAnimator;

    enum MovingStates { Idle, Walking}
    MovingStates movingStates;

    enum CombatStates { Idle, AttackLeft, AttackRight}
    CombatStates combatStates;
    

    [Header("Movement")]
    public float moveSpeed;
    public float accelerationSpeed;
    public float maxSpeed;
    public float normalSpeed;
    public float heightRayDis;
    public float jumpForce;
    private bool mayJump = true;
    private bool mayAttack = true;
    private float xInput;
    private float yInput;

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
    public bool mayMove;

    void Awake()
    {
        currentHealth = maxHealth;
        currentStamina = maxStamina;
        normalSpeed = moveSpeed;
        _rb = GetComponent<Rigidbody>();
        headBobAnimator = headBobAnimator.GetComponent<Animator>();
        ownAnimator = GetComponent<Animator>();
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
            GetHit(10);
        }
    }

    void FixedUpdate()
    {
        if (mayMove)
        {
            Movement();
            Jump();
        }

        switch (movingStates)
        {
            case MovingStates.Idle:
                headBobAnimator.SetBool("Walking", false);
                break;
            case MovingStates.Walking:
                headBobAnimator.SetBool("Walking", true);
                break;
        }

        switch (combatStates)
        {
            case CombatStates.Idle:
                ownAnimator.SetBool("Idle", true);
                ownAnimator.SetBool("AttackLeft", false);
                ownAnimator.SetBool("AttackRight", false);
                break;
            case CombatStates.AttackLeft:
                ownAnimator.SetBool("AttackLeft", true);
                ownAnimator.SetBool("Idle", false);
                ownAnimator.SetBool("AttackRight", false);
                break;
            case CombatStates.AttackRight:
                ownAnimator.SetBool("AttackRight", true);
                ownAnimator.SetBool("Idle", false);
                ownAnimator.SetBool("AttackLeft", false);
                break;
        }
        if (mayAttack)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                Attack();
            }
        }
    }

    void Movement()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
        _rb.MovePosition(transform.position + transform.forward * Time.deltaTime * moveSpeed * yInput + transform.right * Time.deltaTime * moveSpeed * xInput);
        if (yInput > 0)
        {
            movingStates = MovingStates.Walking;
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
            movingStates = MovingStates.Idle;
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
        if (CritStrike()) //Calculate total damage
        {
            totalDamage = Mathf.Round(attackDamage * critDamage);
        }

        if (xInput < 0) //Attack van rechts naar links
        {
            combatStates = CombatStates.AttackLeft;
        }
        else if(xInput > 0) // Attack van links naar rechts
        {
            combatStates = CombatStates.AttackRight;
        }
        else if(xInput == 0) // random attack
        {
            int number = Random.Range(0, 1);
            if(number == 0)
            {
                combatStates = CombatStates.AttackLeft;
            }
            else
            {
                combatStates = CombatStates.AttackRight;
            }
        }
    }

   public void EndAttackAnimation(){
        if (!Input.GetButton("Fire2"))
        {
            combatStates = CombatStates.Idle;
        }
    }

    private  bool CritStrike() //Calculates if attack crits
    {
        float number = Random.Range(0, 100);
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
                Camera.main.GetComponent<CameraController>().maymoveMouse = false;
                mayMove = false;
                gameManager.GetComponent<UI_Manager>().GameOverScreen();
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