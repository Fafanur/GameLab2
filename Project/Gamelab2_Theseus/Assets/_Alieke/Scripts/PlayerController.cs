using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public GameObject gameManager;
    enum States {Idle, Walking}
    States movingStates;

    public float moveSpeed;
    public float accelerationSpeed;
    public float maxSpeed;
    private float normalSpeed;

    public float heightRayDis;

    private Rigidbody _rb;
    private Animator _Anim;
    public float jumpForce;
    private bool mayJump = true;


	void Awake ()
    {
        normalSpeed = moveSpeed;
        _rb = GetComponent<Rigidbody>();
        _Anim = gameManager.GetComponent<Animator>();
	}
	
	void FixedUpdate ()
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
        _rb.MovePosition(transform.position + transform.forward * Time.deltaTime * moveSpeed * yInput +  transform.right * Time.deltaTime * moveSpeed * xInput);
        if (yInput > 0)
        {
            movingStates = States.Walking;
            if (Input.GetButton("Shift"))
            {
                moveSpeed += accelerationSpeed * Time.deltaTime;
                Mathf.Clamp(moveSpeed, 0, maxSpeed);
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
        if(Physics.Raycast(transform.position,-transform.up, heightRayDis))
        {
            mayJump = true;
        }
    }
}
