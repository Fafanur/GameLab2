//made by Alieke
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController playerController;
    [Header("Components")]
    private Rigidbody _rb;
    public Animator headBobAnimator;
    public Animator combatAnimator;

    enum MovingStates { Idle, Walking}
    MovingStates movingStates;

    public enum CombatStates { Idle, AttackLeft, AttackRight}
    public CombatStates combatStates;
    

    [Header("Movement")]
    public float moveSpeed;
    public float accelerationSpeed;
    public float maxSpeed;
    public float normalSpeed;
    public float heightRayDis;
    public float jumpForce;
    public bool mayJump = true;
    private bool mayAttack = true;
    private float xInput;
    private float yInput;
    public RaycastHit jumpHit;

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
    private bool mayRun = true;

    [Header("Health")]
    public float currentHealth;
    public float maxHealth;

    [Header("Defense")]
    public float blockChance;
    public float defenseAmount;

    public bool mayMove;

    RaycastHit hit;
    public float raydis;

    public bool cursorLocked;

    void Awake()
    {
        if(playerController == null)
        {
            playerController = this;
            DontDestroyOnLoad(this);
        }
        else if(playerController != this)
        {
            Destroy(this);
        }
    }

    void Start()
    {
        CheckCursorState();
        currentHealth = maxHealth;
        currentStamina = maxStamina;
        normalSpeed = moveSpeed;
        _rb = GetComponent<Rigidbody>();
        headBobAnimator = headBobAnimator.GetComponent<Animator>();
    }

    void Update() 
    {
        if (Input.GetButtonDown("Alt"))
        {
            if (cursorLocked)
            {
                cursorLocked = false;
            }
            else if(!cursorLocked)
            {
                cursorLocked = true;
            }
            
        }
        CheckCursorState();
    }

    void FixedUpdate()
    {
        if (mayMove)
        {
            Movement();
            Jump();
        }
        else
        {
            movingStates = MovingStates.Idle;
            combatStates = CombatStates.Idle;
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
                combatAnimator.SetBool("Bobbing", true);
                combatAnimator.SetBool("AttackLeft", false);
                combatAnimator.SetBool("AttackRight", false);
                break;
            case CombatStates.AttackLeft:
                combatAnimator.SetBool("AttackLeft", true);
                combatAnimator.SetBool("Bobbing", false);
                combatAnimator.SetBool("AttackRight", false);
                break;
            case CombatStates.AttackRight:
                combatAnimator.SetBool("AttackRight", true);
                combatAnimator.SetBool("Bobbing", false);
                combatAnimator.SetBool("AttackLeft", false);
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
            combatAnimator.SetFloat("BobbingMultiplier", 0.7f);
            movingStates = MovingStates.Walking;    
            if (Input.GetButton("Shift"))
            {
                if (currentStamina > 0 && mayRun)
                {
                    moveSpeed += accelerationSpeed * Time.deltaTime;
                    moveSpeed = Mathf.Clamp(moveSpeed, 0, maxSpeed);
                    currentStamina -= staminaDrain * Time.deltaTime;
                }
                else
                {
                    mayRun = false;
                    moveSpeed -= accelerationSpeed * Time.deltaTime;
                    moveSpeed = Mathf.Clamp(moveSpeed, normalSpeed, maxSpeed);
                }
            }
            else
            {
                moveSpeed -= accelerationSpeed * Time.deltaTime;
                moveSpeed = Mathf.Clamp(moveSpeed, normalSpeed, maxSpeed);
                currentStamina += staminaRecover * Time.deltaTime;
                currentStamina = Mathf.Clamp(currentStamina, 0, 100);
            }
        }
        else if (moveSpeed > normalSpeed)
        {
            moveSpeed -= accelerationSpeed * Time.deltaTime;
        }
        else
        {
            combatAnimator.SetFloat("BobbingMultiplier", 0.2f);
            movingStates = MovingStates.Idle;
        }

        if(currentStamina > 15)
        {
            mayRun = true;
        }

        if (currentStamina < maxStamina && !Input.GetButtonDown("Shift"))
        {
            currentStamina += staminaRecover * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0, 100);
        }

        /* Waarom Gravity uit? 
        if (Physics.Raycast(transform.position, -transform.up, heightRayDis))
        {
            _rb.useGravity = false;
        }
        else
        {
            _rb.useGravity = true;
        }
        */
    }

    void Jump()
    {
        //Debug.DrawRay(transform.position, -Vector3.up, Color.red); 
        // Jump aangepast
        if (Input.GetButtonDown("Jump"))
        {
            if (Physics.Raycast(transform.position, -Vector3.up, out jumpHit, heightRayDis))
            {
                if (jumpHit.transform.tag == "Ground")
                {
                    print("Im Jumping");
                    _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                }
            }
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

        if(Physics.Raycast(transform.position,Vector3.forward, out hit, raydis))
        {
            if(hit.transform.tag == "Enemy")
            {
                hit.transform.GetComponent<SatyrManager>().curHealth -= attackDamage;
            }
        }
    }

    public void CheckCursorState()
    {
        if (cursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
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
               // UIManager.uiManager.GameOverScreen();
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